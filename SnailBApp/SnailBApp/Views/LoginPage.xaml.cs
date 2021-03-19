using SnailBApp.Services;
using SnailBApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var pageService = new PageService();
            InitializeComponent();
            this.BindingContext = new LoginViewModel(pageService);
        }
    }
}