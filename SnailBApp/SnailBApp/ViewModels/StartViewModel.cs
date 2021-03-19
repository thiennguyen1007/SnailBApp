﻿using SnailBApp.Services;
using SnailBApp.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels
{
    public class StartViewModel
    {
        private IPageService _pageService;
        public ICommand OrderCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public StartViewModel()
        {
            _pageService = new PageService();
            LoginCommand = new Command(async () => await _pageService.PushAsync(new LoginPage()));
            OrderCommand = new Command( () => Application.Current.MainPage = new NavigationPage(new OrderPage()));
        }
    }
}
