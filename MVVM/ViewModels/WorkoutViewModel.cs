using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MauiApp1.MVVM.Models;
using MauiApp1.Services;
using Microsoft.Maui.Storage;

namespace MauiApp1.MVVM.ViewModels
{
    public class WorkoutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // ✅ Weekly Plan Dictionary
        public Dictionary<string, string> WeeklyPlan { get; set; } = new();

        // ✅ Custom Workouts
        public ObservableCollection<WorkoutModel> UserWorkouts { get; set; } = new();

        private string _newWorkoutTitle;
        public string NewWorkoutTitle
        {
            get => _newWorkoutTitle;
            set
            {
                _newWorkoutTitle = value;
                OnPropertyChanged(nameof(NewWorkoutTitle));
            }
        }

        private string _newWorkoutDescription;
        public string NewWorkoutDescription
        {
            get => _newWorkoutDescription;
            set
            {
                _newWorkoutDescription = value;
                OnPropertyChanged(nameof(NewWorkoutDescription));
            }
        }

        public ICommand SaveWorkoutCommand { get; }
        public ICommand DeleteWorkoutCommand { get; }

        public WorkoutViewModel()
        {
            SaveWorkoutCommand = new Command(async () => await SaveWorkout());
            DeleteWorkoutCommand = new Command<int>(async (id) => await DeleteWorkout(id));

            LoadWeeklyPlan();   
            LoadWorkouts();     
        }

       
        private void LoadWeeklyPlan()
        {
            WeeklyPlan.Clear();

            WeeklyPlan["Monday"] = "Chest & Triceps";
            WeeklyPlan["Tuesday"] = "Back & Biceps";
            WeeklyPlan["Wednesday"] = "Leg Day";
            WeeklyPlan["Thursday"] = "Cardio + Abs";
            WeeklyPlan["Friday"] = "Shoulders";
            WeeklyPlan["Saturday"] = "Full Body HIIT";
            WeeklyPlan["Sunday"] = "Rest or Yoga";

            OnPropertyChanged(nameof(WeeklyPlan));
        }

        
        private async Task SaveWorkout()
        {
            var username = Preferences.Get("Username", string.Empty);

            if (string.IsNullOrWhiteSpace(NewWorkoutTitle) ||
                string.IsNullOrWhiteSpace(NewWorkoutDescription) ||
                string.IsNullOrWhiteSpace(username))
                return;

            var workout = new WorkoutModel
            {
                Title = NewWorkoutTitle,
                Description = NewWorkoutDescription,
                CreatedBy = username
            };

            await DatabaseService.AddWorkout(workout);

            NewWorkoutTitle = string.Empty;
            NewWorkoutDescription = string.Empty;

            await LoadWorkouts();
        }

        
        private async Task LoadWorkouts()
        {
            UserWorkouts.Clear();

            var username = Preferences.Get("Username", string.Empty);

            var workouts = await DatabaseService.GetWorkoutsForUser(username);

            foreach (var workout in workouts)
            {
                UserWorkouts.Add(workout);
            }
        }

        
        private async Task DeleteWorkout(int id)
        {
            await DatabaseService.DeleteWorkout(id);
            await LoadWorkouts();
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
