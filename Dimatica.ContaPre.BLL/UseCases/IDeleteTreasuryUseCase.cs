namespace Dimatica.ContaPre.BLL.UseCases
{
    public interface IDeleteTreasuryUseCase
    {
        int DeleteTreasury(int treasuryId, int userId, bool connectToOracle);
    }
}
