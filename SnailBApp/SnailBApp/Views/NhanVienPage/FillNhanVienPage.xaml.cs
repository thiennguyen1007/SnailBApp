using SnailBApp.Data;
using SnailBApp.Data.NhanVienData;
using SnailBApp.Services;
using SnailBApp.ViewModels.NhanVienVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views.NhanVienPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FillNhanVienPage : ContentPage
    {
        public FillNhanVienPage()
        {
            var pageService = new PageService();
            var dataAccess = new SQLiteNhanVienStore(DependencyService.Get<ISQLite>());
            ViewModel = new FillNhanVienPageViewModel(dataAccess, pageService);
            this.BindingContext = ViewModel;
            InitializeComponent();
        }
        private FillNhanVienPageViewModel ViewModel
        {
            get { return BindingContext as FillNhanVienPageViewModel; }
            set { BindingContext = value; }
        }
    }
}