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
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SpinImg();
            ViewModel = new StartViewModel();
            this.BindingContext = ViewModel;
            ViewModel.LoadDataCommand.Execute(null);
            NextNew();
        }
        private async void NextNew()
        {         
            do
            {
                await Task.Delay(2000);
                ViewModel.OnNextClicked();
            } while (true);
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
            do
            {
                await img_banner.RotateTo(360, 5000, Easing.BounceIn);
                await img_banner.RotateTo(180, 2500, Easing.BounceIn);
                await img_banner.RotateTo(0, 2500, Easing.BounceIn);
            } while (true);
        }
    }
}