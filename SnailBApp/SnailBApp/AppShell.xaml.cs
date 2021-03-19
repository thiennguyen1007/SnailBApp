using SnailBApp.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnailBApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            Task.Delay(1000);
            Task.Run(async()=> 
            {
                await Shell.Current.Navigation.PopToRootAsync();
                Application.Current.MainPage = new StartPage();
            });           
        }
    }
}
