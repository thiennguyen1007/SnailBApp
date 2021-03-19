using SnailBApp.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; }
        private IPageService _pageService;
        private string _email;
        private string _pass;
        public string Email
        {
            get => _email;
            set { SetProperty(ref _email, value); }
        }
        public string Pass
        {
            get => _pass;
            set { SetProperty(ref _pass, value); }
        }

        public LoginViewModel(IPageService pageService)
        {
            //
            _pageService = pageService;
            //Command
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Pass))
            {
                await _pageService.DisplayAlert("Failed!", "Please enter Email and Password", "OK");
            }
            else
            {
                if (Email == "admin" && Pass == "1234")
                {
                    await _pageService.DisplayAlert("Login Success", "", "Ok");
                    await Task.Delay(1000);
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                    Application.Current.MainPage = new AppShell();                                      
                }
                else
                    await _pageService.DisplayAlert("Login Failed!", "Please enter correct Email and Password", "OK");
            }
        }
    }
}
