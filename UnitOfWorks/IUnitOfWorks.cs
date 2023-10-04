namespace StockManagementSystem.UnitOfWorks
{
    public interface IUnitOfWorks : IDisposable
    {
        void Save();

    }
}
