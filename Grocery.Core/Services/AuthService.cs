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
            var client = _clientService.Get(email);
            if (client != null)
            {
                var passwordFromClient = client.GetType().GetProperty("_password", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.GetValue(client) as string;
                if (passwordFromClient != null && PasswordHelper.VerifyPassword(password, passwordFromClient))
                {
                    return client;
                }
            }
            //Vraag de klantgegevens [Client] op die je zoekt met het opgegeven emailadres
            //Als je een klant gevonden hebt controleer dan of het password matcht --> PasswordHelper.VerifyPassword(password, passwordFromClient)
            //Als alles klopt dan klantgegveens teruggeven, anders null
            return null;
        }
    }
}
