using EE_RetainWallLibrary;
using System.Windows;

namespace EE_RetainWallDriver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += AppLoaded;
        }

        private void AppLoaded(object sender, RoutedEventArgs e)
        {
            LoadTestScenarioData();
        }

        private void LoadTestScenarioData()
        {

            SiteDataModel site_data = new SiteDataModel();
            SoilParametersModel soil_parameters = new SoilParametersModel();

            RetWallModel model = RetainWallCalculator.Create(10.0, site_data, soil_parameters);
        }
    }
}