using ACI318_19Library;
using EE_RetainWallLibrary;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace EE_RetainWallDriver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private RetWallModel _model;
        private SiteDataModel _site_data;
        private SoilParametersModel _soil_parameters;

        private DesignResultModel _concreteDesignResults;

        public MainWindowViewModel ViewModel { get; set; }





        //public PressureResultsModel Results { get; set; }
        public DesignResultModel ConcreteDesignResults
        {
            get => _concreteDesignResults;
            set
            {
                _concreteDesignResults = value;
                OnPropertyChanged(nameof(ConcreteDesignResults));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public MainWindow()
        {
            
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            DataContext = ViewModel;

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


            // test our new Concrete calculator
            var catalog = new RebarCatalog();
            var section = new CrossSection(width: 12.0, depth: 12.0, 
                fck_psi: 4000, fy_psi: 60000, epsilon_cu: 0.002, es_psi: 29000000, 
                tension_cover: 1.5, compression_cover: 1.5, side_cover: 1.5, clear_spacing: 2.0 );

            //// If the design uses compression steel, add it (optional)
            section.AddCompressionRebar("#9", 1, catalog, 1.5);
            section.AddCompressionRebar("#9", 1, catalog, 3.0);


            //// Optionally add an initial tension layer to set d (not required by routine)
            section.AddTensionRebar("#9", 1, catalog, 9.0);
            section.AddTensionRebar("#9", 1, catalog, 10.5);

            //ACIDrawingHelpers.DrawCrossSection(cnvCrossSection, section);

            var design = FlexuralDesigner.ComputeFlexuralStrength(section);
            ViewModel.ConcreteDesignResults = design;

            // Populate DesignResults list





            // now try our sizing algorithm
            double appliedMoment = 50;  // a test load in kip-ft
            FlexuralDesigner flex_designer = new FlexuralDesigner();
            List<DesignResultModel> autoDesignSectionList = flex_designer.DesignAllSections(appliedMoment, 4000, 60000, 0.003, 29000000, 1.5, 1.5, 1.5, 2.0);

            foreach (var auto_design in autoDesignSectionList)
            {
                if (auto_design.crossSection == null)
                {
                    auto_design.crossSection = new CrossSection(); // set proper section
                }

                ViewModel.DesignResults.Add(auto_design);
            }

            ViewModel.NumDesignResults = ViewModel.DesignResults.Count;

            if (ViewModel.DesignResults.Count > 0)
            {
                ViewModel.CurrentSelection = ViewModel.DesignResults[0];
            }

            ViewModel.Mu_kft = appliedMoment;



            // Pressure Results
            ViewModel.PressureResults = new PressureResultsModel
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

        private void btnEnterCrossSectionButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = new CrossSectionViewModel();  // or reuse an existing instance
            var dialog = new CrossSectionInputDialog(vm)
            {
                Owner = this
            };

            if (dialog.ShowDialog() == true)
            {
                // Now vm contains the user’s input
                ViewModel.CrossSectionViewModel = vm;
            }
            //RebarCatalog catalog = new RebarCatalog();
            //var barSizes = new[] { "#3", "#4", "#5", "#6", "#7", "#8", "#9", "#10", "#11" };

            //var dialog = new RebarLayerInputDialog(barSizes, true, 12, 1.5)
            //{
            //    Title = "Add Tension Rebar Layer",
            //    Owner = Application.Current.MainWindow
            //};

            //dialog.BarSizes = catalog.RebarTable.Keys;

            //if (dialog.ShowDialog() == true)
            //{
            //    RebarLayerViewModel viewModel = new RebarLayerViewModel("#6", 1, 12);
            //    viewModel.AddTensionRebar(dialog.SelectedBarSize, dialog.Qty, dialog.DepthFromTop);
            //    // you could also remember the depth here if you want
            //    viewModel.LastTensionDepth = dialog.DepthFromTop;
            //}
        }


    }
}