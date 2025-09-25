using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }
        public Client? Login(string email, string password)
        {
            Client? client = _clientService.Get(email);
            if (client == null) return null;
            if (PasswordHelper.VerifyPassword(password, client.Password)) return client;
            return null;
        }
        
        public Client? Register(string firstName, string lastName, string email, string password, string verifyPassword)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return null;
            
            string name = string.IsNullOrWhiteSpace(lastName) ? firstName : $"{firstName} {lastName}";

            if (!EmailHelper.IsValidEmail(email))
                return null;
            if (!PasswordHelper.CheckPasswords(password, verifyPassword))
                return null;
            Client newClient = new Client(
                0,
                name,
                email,
                PasswordHelper.HashPassword(password)
            );
            return _clientService.Create(newClient);
        }
    }
}
