namespace Dimatica.ContaPre.BLL.UseCases.Treasury
{
    using System.Collections.Generic;

    using Dimatica.ContaPre.OL.Business;
    using Dimatica.ContaPre.OL.Models;

    public interface ISaveTonnageSheetUseCase
    {
        Response SaveTonnageSheet(PRE_HOJA_ARQUEO tonnageSheet, IList<PRE_DETALLE_HOJA_ARQUEO> details, int userId);
    }
}
