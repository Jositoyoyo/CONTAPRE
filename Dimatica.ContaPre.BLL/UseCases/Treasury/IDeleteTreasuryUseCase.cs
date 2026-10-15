namespace Dimatica.ContaPre.BLL.UseCases.Treasury
{
    public interface IDeleteTreasuryUseCase
    {
        DeleteTreasuryResult DeleteTreasury(int treasuryId, int userId, bool connectToOracle);
    }
}
