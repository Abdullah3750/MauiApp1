using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MauiApp1.MVVM.Models;

namespace MauiApp1.MVVM.ViewModels
{
    public class ProgressViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ProgressItem> Progress { get; set; }

        private string _message = "Track your daily progress below:";
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
            }
        }

        public ProgressViewModel()
        {
            var today = DateTime.Today.ToString("yyyy-MM-dd");

            Progress = new ObservableCollection<ProgressItem>
            {
                new ProgressItem { Date = "2025-03-24", Detail = "Morning jog - 15 mins", IsToday = today == "2025-03-24" },
                new ProgressItem { Date = "2025-03-25", Detail = "Healthy smoothie", IsToday = today == "2025-03-25" },
                new ProgressItem { Date = today, Detail = "Yoga session - 30 mins", IsToday = true }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}