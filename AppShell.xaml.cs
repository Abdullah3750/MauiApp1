namespace MauiApp1;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(WorkoutPage), typeof(WorkoutPage));
        Routing.RegisterRoute(nameof(NutritionPage), typeof(NutritionPage));
        Routing.RegisterRoute(nameof(ProgressPage), typeof(ProgressPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}