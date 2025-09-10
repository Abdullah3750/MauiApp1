namespace MauiApp1.MVVM.Models
{
    public class WorkoutItem
    {
        public string Day { get; set; } = string.Empty;
        public string Plan { get; set; } = string.Empty;
        public int Sets { get; set; }
        public int Reps { get; set; }
    }
}