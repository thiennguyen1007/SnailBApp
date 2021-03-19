
using SnailBApp.Data;
using SnailBApp.Data.NhanVienData;
using SnailBApp.Services;
using SnailBApp.ViewModels.NhanVienVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views.NhanVienPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailNhanVien : ContentPage
    {
        private DetailNhanVienViewModel ViewModel
        {
            get {return BindingContext as DetailNhanVienViewModel; }
            set { BindingContext = value;}
        }
        public DetailNhanVien(NhanVienViewModel x)
        {
            var dataAccess = new SQLiteNhanVienStore(DependencyService.Get<ISQLite>());
            var pageService = new PageService();
            ViewModel = new DetailNhanVienViewModel(x,dataAccess,pageService);
            this.BindingContext = ViewModel;
            InitializeComponent();
        }
    }
}