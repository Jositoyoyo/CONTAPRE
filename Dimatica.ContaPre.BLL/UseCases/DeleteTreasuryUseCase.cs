namespace Dimatica.ContaPre.BLL.UseCases
{
    using System;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;

    public class DeleteTreasuryUseCase : IDeleteTreasuryUseCase
    {
        private readonly ITreasuriesService treasuriesService;

        private readonly IAccountingDocumentsService accountingDocumentsService;

        public DeleteTreasuryUseCase(ITreasuriesService treasuriesService, IAccountingDocumentsService accountingDocumentsService)
        {
            if (treasuriesService == null)
            {
                throw new ArgumentNullException(nameof(treasuriesService));
            }

            if (accountingDocumentsService == null)
            {
                throw new ArgumentNullException(nameof(accountingDocumentsService));
            }

            this.treasuriesService = treasuriesService;
            this.accountingDocumentsService = accountingDocumentsService;
        }

        public int DeleteTreasury(int treasuryId, int userId, bool connectToOracle)
        {
            if (treasuryId == 0)
            {
                return 2;
            }

            var foundInOracle = false;
            var recognizedRights = string.Empty;

            if (connectToOracle)
            {
                foundInOracle = this.treasuriesService.FoundTreasuryOracle(treasuryId);

                if (foundInOracle)
                {
                    recognizedRights = this.treasuriesService.GetRecognizedRightsOracle(treasuryId);
                }
            }

            if (!string.IsNullOrWhiteSpace(recognizedRights))
            {
                return 3;
            }

            var documents = this.treasuriesService.GetDocuments(null, null, treasuryId);

            foreach (var document in documents)
            {
                if (document.DOC_CODIGO != null)
                {
                    var bills = this.accountingDocumentsService.GetPurchases((int)document.DOC_CODIGO);

                    foreach (var bill in bills)
                    {
                        this.accountingDocumentsService.UpdatePayBankOracle((DateTime?)null, (int)bill.CODFACTURAGEI);
                        this.accountingDocumentsService.UpdateHistoryOracle(bill.NCERTIFICADO, "Se ha eliminado el apunte de tesorería vinculado al pago de la factura.");
                    }
                }
            }

            if (foundInOracle)
            {
                var deleteTreasuryOracle = this.treasuriesService.DeleteTreasuryOracle(treasuryId);

                if (deleteTreasuryOracle.ResponseCode != ResponseCode.Ok)
                {
                    return 4;
                }
            }

            var delete = this.treasuriesService.DeleteTreasury(treasuryId, userId);

            switch (delete.ResponseCode)
            {
                case ResponseCode.Invalid:
                    return 0;
                case ResponseCode.Ok:
                    return 1;
                case ResponseCode.NotFound:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
