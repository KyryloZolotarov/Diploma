namespace Order.Hosts.Services.Interfaces
{
    public interface IOrderUserService
    {
        Task<int?> Add(int id, string name, string givenName, string familyName, string email, string address);
        Task<int?> Update(int id, string name, string givenName, string familyName, string email, string address);
        Task<int?> Delete(int id);
    }
}
