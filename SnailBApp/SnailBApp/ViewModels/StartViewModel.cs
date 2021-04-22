using SnailBApp.Models;
using SnailBApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace SnailBApp.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        private New _news;
        int index=0;
        public ICommand OrderCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public ICommand LoadDataCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        //
        public ObservableCollection<New> LstNews { get; set;  } = new ObservableCollection<New>();
        public New News { get => _news; set => SetProperty(ref _news, value); }

        public StartViewModel()
        {
            LoginCommand = new Command(()=>Application.Current.MainPage = new NavigationPage(new LoginPage()));
            OrderCommand = new Command(() => Application.Current.MainPage = new NavigationPage(new OrderPage()));
            //
            LoadDataCommand = new Command(LoadData);
            NextCommand = new Command(OnNextClicked);
        }
        private void LoadData()
        {
            News = new New(KhoiTao()[0]);
        }
        public void OnNextClicked()
        {
            index++;
            if(index== KhoiTao().Count)
            {
                index = 0;
            }
            News = KhoiTao()[index];
        }
        private ObservableCollection<New> KhoiTao()
        {
            ObservableCollection<New> x = new ObservableCollection<New>();
            New n = new New {
                Title = "Offer -50 %",
                Detail = "Cho đơn hàng đầu tiên, ưu đãi với khách hàng mới",
                IMG = "https://bytuong.com/wp-content/uploads/2019/04/sale-2.jpg",
            };
            New n2 = new New
            {
                Title = "Couple BBQ",
                Detail = "Các cặp đôi offer có bàn riêng, love love...",
                IMG = "https://magedalqerbi.cachefly.net/p/60024/2.webp",
            };
            New n1 = new New {
                Title = "Drink sales",
                Detail = "Thức uống đang được free với xuất đặc biệt 169$",
                IMG = "https://images.vexels.com/media/users/3/145673/isolated/preview/45cabd43ddfce4f502244cd475ed7138-free-drink-ticket-by-vexels.png"
            };
            x.Add(n);
            x.Add(n2);
            x.Add(n1);
            return x;
        }
    }
}
