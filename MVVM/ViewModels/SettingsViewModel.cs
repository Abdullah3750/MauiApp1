using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.ApplicationModel;

namespace MauiApp1.MVVM.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isDarkMode;
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                if (_isDarkMode != value)
                {
                    _isDarkMode = value;
                    OnPropertyChanged();
                    ApplyTheme(value);
                }
            }
        }

        public SettingsViewModel()
        {
            // Set initial mode based on app's current theme
            IsDarkMode = Application.Current.UserAppTheme == AppTheme.Dark;
        }

        private void ApplyTheme(bool isDark)
        {
            Application.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;
        }

        void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}