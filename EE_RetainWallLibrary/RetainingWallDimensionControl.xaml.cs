using System.Windows.Controls;

namespace EE_RetainWallLibrary
{
    public partial class RetainingWallDimensionsControl : UserControl
    {
        public RetWallModel WallData { get; set; }

        public RetainingWallDimensionsControl()
        {
            InitializeComponent();
            WallData = new RetWallModel();
            this.DataContext = WallData;
        }

        public RetainingWallDimensionsControl(RetWallModel wall)
        {
            InitializeComponent();
            WallData = wall;
            this.DataContext = WallData;
        }
    }
}
