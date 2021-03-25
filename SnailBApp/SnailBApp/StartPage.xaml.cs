using SnailBApp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        private StartViewModel ViewModel;
        public StartPage()
        {
            ViewModel = new StartViewModel();
            InitializeComponent();
            Task.Run(SpinImg);
            this.BindingContext = ViewModel;
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");
                if (result)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        }
        private async void SpinImg()
        {
            double displacement = img_banner.Width;
            while (true) {
                await img_banner.FadeTo(0, 800, Easing.Linear);
                await img_banner.TranslateTo(-displacement, img_banner.Y, 800, Easing.CubicInOut);
                await img_banner.TranslateTo(displacement, 0, 0);
                await Task.WhenAll(
                    img_banner.FadeTo(1, 800, Easing.Linear),
                    img_banner.TranslateTo(0, img_banner.Y, 800, Easing.CubicInOut));
            }                           
            //while (true)
            //{
            //    await img_banner.RelRotateTo(360, 5000, Easing.BounceIn);
            //}

        }
    }
}