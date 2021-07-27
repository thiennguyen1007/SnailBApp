using Android.App;
using Android.OS;

namespace SnailBApp.Droid
{
    [Activity(Label = "TX Flowers", Theme="@style/SplashTheme", MainLauncher =true,NoHistory =true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            System.Threading.Thread.Sleep(1500);
            StartActivity(typeof(MainActivity));
        }
    }
}