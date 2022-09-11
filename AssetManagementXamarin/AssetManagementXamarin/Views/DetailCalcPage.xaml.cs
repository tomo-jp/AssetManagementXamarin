using AssetManagementXamarin.ViewModels;
using Xamarin.Forms;

namespace AssetManagementXamarin.Views
{
    public partial class DetailCalcPage : ContentPage
    {
        public DetailCalcPage()
        {
            InitializeComponent();

            BindingContext = new DetailCalcViewModel();
        }
    }
}