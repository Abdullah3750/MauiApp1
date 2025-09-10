using System.ComponentModel;
using System.Windows.Input;
using MauiApp1.MVVM.Models;
using MauiApp1.Services;
using Microsoft.Maui.Controls;

namespace MauiApp1.MVVM.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await RegisterUser());
        }

        private async Task RegisterUser()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                StatusMessage = "Please fill all fields.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                StatusMessage = "Passwords do not match.";
                return;
            }

            var existingUser = await DatabaseService.GetUser(Username);
            if (existingUser != null)
            {
                StatusMessage = "Username already exists.";
                return;
            }

            var newUser = new UserModel
            {
                Username = Username,
                Password = Password
            };

            await DatabaseService.AddUser(newUser);

            StatusMessage = "Registration successful ✅";
            Username = Password = ConfirmPassword = string.Empty;
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
