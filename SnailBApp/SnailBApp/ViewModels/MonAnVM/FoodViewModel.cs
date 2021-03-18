namespace SnailBApp.ViewModels.MonAnVM
{
    public class FoodViewModel : BaseViewModel
    {
        private int _id;
        private string _name;
        private string _img;
        private string _desc;
        private int _sl;
        private float _price;
        public int ID
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string IMG
        {
            get { return _img; }
            set { SetProperty(ref _img, value); }
        }
        public string Desc
        {
            get { return _desc; }
            set { SetProperty(ref _desc, value); }
        }
        public float Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        public int SL
        {
            get { return _sl; }
            set { SetProperty(ref _sl, value); }
        }
    }
}
