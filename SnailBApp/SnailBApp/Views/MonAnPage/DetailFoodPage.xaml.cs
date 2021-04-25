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
    public partial class DetailFoodPage : ContentPage
    {
        public DetailFoodPage(FoodViewModel f)
        {
            var pageService = new PageService();
            var dataAccess = new SQLiteFoodStore(DependencyService.Get<ISQLite>());
            ViewModel = new DetailFoodPageViewModel(pageService, dataAccess,f);
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            
        }
        private DetailFoodPageViewModel ViewModel
        {
            get => BindingContext as DetailFoodPageViewModel;
            set { BindingContext = value; }
        }
    }
}