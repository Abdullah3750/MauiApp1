using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using MauiApp1.Services;
using MauiApp1.MVVM.Models;

namespace MauiApp1.MVVM.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private bool _rememberMe;
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                if (_rememberMe != value)
                {
                    _rememberMe = value;
                    OnPropertyChanged(nameof(RememberMe));
                }
            }
        }

        private string _loginStatus;
        public string LoginStatus
        {
            get => _loginStatus;
            set
            {
                _loginStatus = value;
                OnPropertyChanged(nameof(LoginStatus));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await OnLogin());
        }

        private async Task OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                LoginStatus = "Please enter username and password.";
                return;
            }

            var user = await DatabaseService.GetUser(Username);

            if (user == null)
            {
                LoginStatus = "User not found.";
                return;
            }

            if (user.Password != Password)
            {
                LoginStatus = "Incorrect password.";
                return;
            }

            // Login successful 
            LoginStatus = "Login successful ✅";

            if (RememberMe)
            {
                Preferences.Set("IsLoggedIn", true);
                Preferences.Set("Username", Username);
            }

            // Navigate to main app
            Application.Current.MainPage = new AppShell();
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
