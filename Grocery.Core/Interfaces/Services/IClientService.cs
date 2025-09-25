using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IClientService
    {
        Client? Get(string email);
        Client? Get(int id);
        List<Client> GetAll();
        Client Create(Client newClient);
    }
}
