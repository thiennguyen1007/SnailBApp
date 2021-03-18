using SnailBApp.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels.NhanVienVM
{
    class ListNhanVienPageViewModel: BaseViewModel
    {
        private IPageService _pageService;
        public ICommand AddCommand { get; private set; }
        public ListNhanVienPageViewModel(IPageService pageService)
        {
            _pageService = pageService;
            AddCommand = new Command(OnAddClicked);
        }
        private void OnAddClicked()
        {
            _pageService.PushAsync(new Views.NhanVienPage.FillNhanVienPage());
        }
    }
}
