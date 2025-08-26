using System.Windows.Controls;

namespace EE_RetainWallLibrary
{
    public partial class PressureResultsControl : UserControl
    {
        public PressureResultsControl()
        {
            InitializeComponent();
        }

        public PressureResultsModel Model
        {
            get => DataContext as PressureResultsModel;
            set => DataContext = value;
        }
    }
}
