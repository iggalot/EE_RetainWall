using System.Windows.Controls;

namespace EE_RetainWallLibrary
{
    /// <summary>
    /// Interaction logic for PressureBlockDisplayControl.xaml
    /// </summary>
    public partial class PressureBlockDisplayControl : UserControl
    {
        public PressureBlockDisplayControl()
        {
            InitializeComponent();
        }

        public PressureBlockDataModel Model
        {
            get => DataContext as PressureBlockDataModel;
            set => DataContext = value;
        }
    }
}
