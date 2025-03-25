namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnWorkoutClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(WorkoutPage));
    }

    private async void OnNutritionClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(NutritionPage));
    }

    private async void OnProgressClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProgressPage));
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }
}