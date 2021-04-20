namespace SnailBApp.ViewModels.HoaDonVM
{
    public class HoaDonViewModel : BaseViewModel
    {
        public int ID { get; set; }
        private string _email;
        private string _phoneNumber;
        private float _price;
        private string _foods;
        private string _date;
        public string Email
        {
            get => _email;
            set { SetProperty(ref _email, value); }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set { SetProperty(ref _phoneNumber, value); }
        }
        public string Foods
        {
            get => _foods;
            set { SetProperty(ref _foods, value); }
        }
        public string Date
        {
            get => _date;
            set { SetProperty(ref _date, value); }
        }
        public float Price
        {
            get => _price;
            set { SetProperty(ref _price, value); }
        }
        public HoaDonViewModel(Models.HoaDon hd)
        {
            ID = hd.ID;
            Email = hd.Email;
            PhoneNumber = hd.PhoneNumber;
            Date = hd.Date;
            Foods = hd.Foods;
            Price = hd.Price;
        }
        public HoaDonViewModel(HoaDonViewModel hd)
        {
            ID = hd.ID;
            Email = hd.Email;
            PhoneNumber = hd.PhoneNumber;
            Date = hd.Date;
            Foods = hd.Foods;
            Price = hd.Price;
        }
        public HoaDonViewModel() { }
    }
}
