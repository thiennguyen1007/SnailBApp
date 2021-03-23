using SnailBApp.Services;
using SnailBApp.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels
{
    public class StartViewModel
    {
        public ICommand OrderCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public StartViewModel()
        {
            LoginCommand = new Command(()=>Application.Current.MainPage = new NavigationPage(new LoginPage()));
            OrderCommand = new Command( () => Application.Current.MainPage = new NavigationPage(new OrderPage()));
        }
    }
}
