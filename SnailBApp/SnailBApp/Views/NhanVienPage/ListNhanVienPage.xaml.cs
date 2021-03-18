
using SnailBApp.Services;
using SnailBApp.ViewModels.NhanVienVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views.NhanVienPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListNhanVienPage : ContentPage
    {
        //private ListNhanVienPageViewModel ViewModel;
        public ListNhanVienPage()
        {
            var pageService = new PageService();
            this.BindingContext = new ListNhanVienPageViewModel(pageService);
            InitializeComponent();
        }
    }
}