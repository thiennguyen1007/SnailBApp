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
            while (true)
            {
                await img_banner.RelRotateTo(360, 10000, Easing.BounceOut);
            }
        }
    }
}