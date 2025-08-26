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

        public PressureResultsModel Results { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += AppLoaded;
        }

        private void AppLoaded(object sender, RoutedEventArgs e)
        {
            LoadTestScenarioData();

            retainingWallDimensionsControl.Content = new RetainingWallDimensionsControl(_model);
        }

        private void LoadTestScenarioData()
        {
            SiteDataModel site_data = new SiteDataModel(5,0);
            SoilParametersModel soil_parameters = new SoilParametersModel();

            RetWallModel model = RetWallModel.Create(10.0, site_data, soil_parameters);


            _site_data = site_data;
            _soil_parameters = soil_parameters;
            _model = model;

            DisplayWallStem(model);
            DisplaySoilProperties();

            Results = new PressureResultsModel
            {
                WallStemActivePressure = _model.wallStemActivePressure,
                WallStemPassivePressure = _model.wallStemPassivePressure,
                WallFootingActivePressure = _model.wallFootingActivePressure,
                WallFootingPassivePressure = _model.wallFootingPassivePressure,
                WallKeyActivePressure = _model.wallKeyActivePressure,
                WallKeyPassivePressure = _model.wallKeyPassivePressure,
                WallHeelPressure = _model.wallHeelPressure,
                WallKeyPressure = _model.wallKeyPressure,
                WallToePressure = _model.wallToePressure
            };

            pressureResultsControl.DataContext = Results;
        }

        private void DisplayWallStem(RetWallModel model)
        {
        }

        private void DisplaySoilProperties()
        {
            tbFrontSideSoilDepth.Text = _site_data.SoilSurfaceDepthFront.ToString() + "ft";
            tbBackSideSoilDepth.Text = _site_data.SoilSurfaceDepthBehind.ToString() + "ft";
            tbGamma.Text = _soil_parameters.Density.ToString();
            tbKa.Text = _soil_parameters.Ka.ToString();
            tbKp.Text = _soil_parameters.Kp.ToString();
        }
    }
}