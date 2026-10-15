namespace Dimatica.ContaPre.BLL.UseCases.Treasury
{
    using System;
    using System.Collections.Generic;

    using Dimatica.ContaPre.BLL.Interfaces;
    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    public class SaveTonnageSheetUseCase : ISaveTonnageSheetUseCase
    {
        private readonly ITonnageSheetsService tonnageSheetsService;

        public SaveTonnageSheetUseCase(ITonnageSheetsService tonnageSheetsService)
        {
            if (tonnageSheetsService == null)
            {
                throw new ArgumentNullException(nameof(tonnageSheetsService));
            }

            this.tonnageSheetsService = tonnageSheetsService;
        }

        public Response SaveTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet, IList<PRE_DETALLE_HOJA_ARQUEO> details, int userId)
        {
            if (tonnageSheet == null)
            {
                throw new ArgumentNullException(nameof(tonnageSheet));
            }

            if (details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }

            tonnageSheet.USU_CODIGO = userId;

            if (tonnageSheet.HOJ_CODIGO != 0)
            {
                var update = this.tonnageSheetsService.UpdateTonnageSheet(tonnageSheet);

                if (update.ResponseCode != ResponseCode.Ok)
                {
                    return update;
                }

                foreach (var detail in details)
                {
                    var existingDetail = this.tonnageSheetsService.GetDetailByFilters(
                        tonnageSheet.HOJ_CODIGO,
                        detail.DET_NUMERO_EXPEDIENTE,
                        detail.LIN_NUMERO);

                    if (existingDetail != null)
                    {
                        this.tonnageSheetsService.UpdateTonnageSheetDetail(existingDetail.DET_CODIGO, detail.MARCADO == true, userId);
                    }
                    else
                    {
                        detail.HOJ_CODIGO = tonnageSheet.HOJ_CODIGO;
                        detail.USU_CODIGO = userId;
                        this.tonnageSheetsService.InsertTonnageSheetDetail(detail);
                    }
                }

                return update;
            }

            var insert = this.tonnageSheetsService.InsertTonnageSheet(tonnageSheet);

            if (insert.ResponseCode != ResponseCode.Ok)
            {
                return insert;
            }

            var sheetId = (int)insert.ResponseMethod;

            foreach (var detail in details)
            {
                detail.HOJ_CODIGO = sheetId;
                detail.USU_CODIGO = userId;
                this.tonnageSheetsService.InsertTonnageSheetDetail(detail);
            }

            return insert;
        }
    }
}
