using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SnailBApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
            Task.Run(SpinImg);
        }
        private async void SpinImg()
        {
            while (true)
            {
                await img_banner.RelRotateTo(360, 10000, Easing.BounceOut);
            }
        }
    }
}