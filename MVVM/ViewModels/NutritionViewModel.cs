using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.MVVM.ViewModels
{
    public class NutritionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title = "🍎 Nutrition Tips";
        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        private List<string> _tips = new()
        {
            "• Drink plenty of water.",
            "• Include protein in every meal.",
            "• Eat fruits and vegetables daily.",
            "• Avoid processed foods.",
            "• Don't skip breakfast."
        };

        public List<string> Tips => _tips;

        void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}