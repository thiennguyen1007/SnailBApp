
using SnailBApp.Data;
using SnailBApp.Data.NhanVienData;
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
            var dataAccess = new SQLiteNhanVienStore(DependencyService.Get<ISQLite>());
            this.BindingContext = new ListNhanVienPageViewModel(dataAccess,pageService);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
        }
        public ListNhanVienPageViewModel ViewModel
        {
            get { return BindingContext as ListNhanVienPageViewModel; }
            set { BindingContext = value; }
        }
        private async void lstNV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           await ViewModel.ItemSelectedAsync(e.SelectedItem as NhanVienViewModel);
        }
    }
}