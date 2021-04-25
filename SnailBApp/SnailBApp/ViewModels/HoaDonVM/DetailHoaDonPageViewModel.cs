using SnailBApp.Data.HoaDonData;
using SnailBApp.Services;

namespace SnailBApp.ViewModels.HoaDonVM
{
    public class DetailHoaDonPageViewModel: BaseViewModel
    {
        private IHoaDonStore _hoaDonStore;
        private IPageService _pageService;
        //
        private HoaDonViewModel _hoaDon;

        public HoaDonViewModel HoaDon { get => _hoaDon; set => _hoaDon = value; }
        public DetailHoaDonPageViewModel(IHoaDonStore hoaDonStore, HoaDonViewModel hd, IPageService pageService)
        {
            _pageService = pageService;
            _hoaDonStore = hoaDonStore;
            //
            HoaDon = new HoaDonViewModel(hd);
        }
    }
}
