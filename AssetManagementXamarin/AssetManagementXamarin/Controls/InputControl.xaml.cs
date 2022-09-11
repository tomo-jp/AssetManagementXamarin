using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AssetManagementXamarin.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputControl : BaseControl
    {
        private string _itemText;
        public string ItemText
        {
            get { return _itemText; }
            set { SetProperty(ref _itemText, value); }
        }

        private string _unitText;
        public string UnitText
        {
            get { return _unitText; }
            set { SetProperty(ref _unitText, value); }
        }

        public bool HasError
        {
            get { return (bool)GetValue(HasErrorProperty); }
            set { SetValue(HasErrorProperty, value); }
        }

        public static readonly BindableProperty HasErrorProperty =
            BindableProperty.Create("HasError", typeof(bool), typeof(InputControl), false);

        public int InputValue
        {
            get { return (int)GetValue(InputValueProperty); }
            set { SetValue(InputValueProperty, value); }
        }

        public static readonly BindableProperty InputValueProperty =
            BindableProperty.Create("InputValue", typeof(int), typeof(InputControl), 0);

        public InputControl()
        {
            InitializeComponent();
        }
    }
}