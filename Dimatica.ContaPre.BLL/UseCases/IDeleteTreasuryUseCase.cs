namespace Dimatica.ContaPre.BLL.UseCases
{
    public interface IDeleteTreasuryUseCase
    {
        DeleteTreasuryResult DeleteTreasury(int treasuryId, int userId, bool connectToOracle);
    }
}
