namespace SnailBApp.ViewModels.NhanVienVM
{
    public class NhanVienViewModel : BaseViewModel
    {
        public int ID { get; set; }
        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }
        public string Date
        {
            get => _date;
            set { SetProperty(ref _date, value); }
        }
        public string GioiTinh
        {
            get => _gioiTinh;
            set { SetProperty(ref _gioiTinh, value); }
        }
        public string Address { get => _address; set { SetProperty(ref _address, value); } }
        public string Desc { get => _desc; set { SetProperty(ref _desc, value); } }
        public string IMG { get => _img; set { SetProperty(ref _img, value); } }
        public string PhoneNumber { get => _phoneNumber; set { SetProperty(ref _phoneNumber, value); } }

        private string _name;
        private string _date;
        private string _gioiTinh;
        private string _address;
        private string _desc;
        private string _img;
        private string _phoneNumber;
        public NhanVienViewModel(Models.NhanVien x)
        {
            ID = x.ID;
            Name = x.Name;
            Desc = x.Desc;
            Date = x.Date;
            PhoneNumber = x.PhoneNumber;
            IMG = x.IMG;
            GioiTinh = x.GioiTinh;
            Address = x.Address;
        }

        public NhanVienViewModel(int id, string name, string date, string gioiTinh, string address, string desc, string img, string phoneNumber)
        {
            ID = id;
            _name = name;
            _date = date;
            _gioiTinh = gioiTinh;
            _address = address;
            _desc = desc;
            _img = img;
            _phoneNumber = phoneNumber;
        }
        public NhanVienViewModel()
        { }
    }
}
