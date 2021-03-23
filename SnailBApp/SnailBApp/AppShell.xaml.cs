using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnailBApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            if (await this.DisplayAlert("Are you sure?", "Log out now!", "OK", "Cancel"))
            {
                await Task.Delay(1000);
                //await Navigation.PopToRootAsync();
                //await Shell.Current.Navigation.PopToRootAsync();
                Application.Current.MainPage = new StartPage();
            }
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");

                if (result)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow(); // Or anything else
                    
                }
            });
            return true;
        }
    }
}
