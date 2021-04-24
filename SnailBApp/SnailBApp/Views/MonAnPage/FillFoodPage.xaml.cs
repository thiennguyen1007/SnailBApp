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
    public partial class FillFoodPage : ContentPage
    {
        public FillFoodPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            var pageService = new PageService();
            var dataAccess = new SQLiteFoodStore(DependencyService.Get<ISQLite>());
            ViewModel = new FillFoodPageViewModel(pageService, dataAccess);
        }
        private FillFoodPageViewModel ViewModel
        {
            get => BindingContext as FillFoodPageViewModel;
            set { BindingContext = value; }
        }
        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            ViewModel.NumberUnfocus();
        }
    }
}