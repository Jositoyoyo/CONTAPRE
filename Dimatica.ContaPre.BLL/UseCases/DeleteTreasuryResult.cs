namespace Dimatica.ContaPre.BLL.UseCases
{
    public enum DeleteTreasuryResult
    {
        Invalid = 0,
        Ok = 1,
        NotFound = 2,
        RecognizedRights = 3,
        OracleDeleteFailed = 4
    }
}
