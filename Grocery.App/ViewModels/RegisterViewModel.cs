
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.App.ViewModels 
{
    public partial class RegisterViewModel : BaseViewModel 
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        [ObservableProperty]
        private string firstName = "";

        [ObservableProperty]
        private string lastName = "";

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string verifyPassword = "";

        [ObservableProperty]
        private string registerMessage = "";

        public RegisterViewModel(IAuthService authService, GlobalViewModel global) 
        {
            _authService = authService;
            _global = global;
        }

        [RelayCommand]
        public async void NavigateToLogin() {
            var loginViewModel = App.Current.Handler.MauiContext.Services.GetService<LoginViewModel>();
            var loginView = new Views.LoginView(loginViewModel);
            await Application.Current.MainPage.Navigation.PushModalAsync(loginView);
        }

        [RelayCommand]
        private void Register()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                RegisterMessage = "je hebt niks ingevuld voor de voornaam";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(VerifyPassword) || password != verifyPassword)
            {
                RegisterMessage = "een van de wachtwoord velden is leeg of de wachtwoorden komen niet overeen";
                return;
            }
            //TODO: verder testen register

            Client? authenticatedClient = _authService.Register(FirstName, LastName, Email, Password, VerifyPassword);
            if (authenticatedClient != null) 
            {
                RegisterMessage = $"Welkom {authenticatedClient.name}!";
                _global.Client = authenticatedClient;
                Application.Current!.MainPage = new AppShell();
            } 
            else 
            {
                RegisterMessage = "Ongeldige gegevens.";
            }
        }
    }
}
