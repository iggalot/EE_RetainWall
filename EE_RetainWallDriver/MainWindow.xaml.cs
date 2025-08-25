using EE_RetainWallLibrary;
using System.Windows;

namespace EE_RetainWallDriver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RetWallModel _model;
        private SiteDataModel _site_data;
        private SoilParametersModel _soil_parameters;

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
            SiteDataModel site_data = new SiteDataModel(10, 0);
            SoilParametersModel soil_parameters = new SoilParametersModel();

            RetWallModel model = RetainWallCalculator.Create(10.0, site_data, soil_parameters);


            _site_data = site_data;
            _soil_parameters = soil_parameters;
            _model = model;

            DisplayWallStem(model);
            DisplaySoilProperties();
        }

        private void DisplayWallStem(RetWallModel model)
        {
            tbStemResults.Text = model.DisplayInfo();
        }

        private void DisplaySoilProperties()
        {
            tbGamma.Text = _soil_parameters.Density.ToString();
            tbKa.Text = _soil_parameters.Ka.ToString();
            tbKp.Text = _soil_parameters.Kp.ToString();


        }
    }
}