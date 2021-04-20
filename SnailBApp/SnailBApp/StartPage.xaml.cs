using SnailBApp.ViewModels;
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
            SpinImg();
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
                await img_banner.RotateTo(360,2000, Easing.BounceIn);
            }                           
        }
    }
}