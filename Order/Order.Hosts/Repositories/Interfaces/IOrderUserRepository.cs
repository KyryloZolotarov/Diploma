namespace Order.Hosts.Repositories.Interfaces
{
    public interface IOrderUserRepository
    {
        Task<int?> Add(int id, string name, string givenName, string familyName, string email, string address);
        Task<int?> Update(int id, string name, string givenName, string familyName, string email, string address);
        Task<int?> Delete(int id);
    }
}
