namespace Order.Hosts.Repositories.Interfaces;

public interface IOrderUserRepository
{
    Task<string> Add(string id, string name, string givenName, string familyName, string email, string address);
    Task<string> Update(string id, string name, string givenName, string familyName, string email, string address);
    Task<string> Delete(string id);
}