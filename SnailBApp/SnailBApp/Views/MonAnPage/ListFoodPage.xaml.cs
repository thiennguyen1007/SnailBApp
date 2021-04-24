using SnailBApp.Data;
using SnailBApp.Data.FoodData;
using SnailBApp.Services;
using SnailBApp.ViewModels.MonAnVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp.Views.MonAnPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListFoodPage : ContentPage
    {
        public ListFoodPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            var pageService = new PageService();
            var dataAccess = new SQLiteFoodStore(DependencyService.Get<ISQLite>());
            ViewModel = new ListFoodPageViewModel(dataAccess,pageService);
            base.OnAppearing();
            ViewModel.LoadDataCommand.Execute(null);
        }
        public ListFoodPageViewModel ViewModel
        {
            get { return BindingContext as ListFoodPageViewModel; }
            set { BindingContext = value; }
        }
    }
}