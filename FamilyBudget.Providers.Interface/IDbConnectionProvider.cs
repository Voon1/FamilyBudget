using System.Data;


namespace FamilyBudget.Providers.Interface
{
    public interface IDbConnectionProvider : IProvider
    {

        IDbConnection ProvideNewConnection(DbConnectionType connectionType = DbConnectionType.DefaultConnection);
    }
}
