using Android.App;
using Android.Content.PM;
using Android.OS;

namespace BrainChallenger;

[Activity(Theme = "@style/DarkTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density,
    ScreenOrientation = ScreenOrientation.Portrait)]
public class MainActivity : MauiAppCompatActivity
{
    public static MainActivity Main;

    public static Action BackClicked;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        Main = this;

        Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentNavigation);
        Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
        Window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);

    }

    //Recolor status bar background
    public static void SetStatusBackGround(Android.Graphics.Color color)
    {
        Main.Window.SetStatusBarColor(color);
    }

    //Recolor status bar background
    public static void SetNavigationBackGround(Android.Graphics.Color color)
    {
        Main.Window.SetNavigationBarColor(color);
    }

    public override void OnBackPressed()
    {
        BackClicked();
    }

}
