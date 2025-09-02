using ACI318_19Library;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EE_RetainWallLibrary
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private CrossSectionViewModel _crossSectionVM = new CrossSectionViewModel();
        public CrossSectionViewModel CrossSectionViewModel
        {
            get => _crossSectionVM;
            set
            {
                if (_crossSectionVM != value)
                {
                    _crossSectionVM = value;
                    OnPropertyChanged(nameof(_crossSectionVM));
                }
            }
        }

        private double _mu_kft = 0.0;
        private double _mu_kipin;

        public double Mu_kipin { get => Mu_kft * 12; }
        public double Mu_kft 
        {
            get => _mu_kft;
            set
            {
                if (_mu_kft != value)
                {
                    _mu_kft = value;
                    OnPropertyChanged(nameof(Mu_kft));
                    OnPropertyChanged(nameof(Mu_kipin));

                }
            }
        }

        private PressureResultsModel _pressureResults = new PressureResultsModel();
        public PressureResultsModel PressureResults
        {
            get => _pressureResults;
            set
            {
                if (_pressureResults != value)
                {
                    _pressureResults = value;
                    OnPropertyChanged(nameof(PressureResults));
                }
            }
        }

        private DesignResultModel _concreteDesignResults;
        public DesignResultModel ConcreteDesignResults
        {
            get => _concreteDesignResults;
            set
            {
                if (_concreteDesignResults != value)
                {
                    _concreteDesignResults = value;
                    OnPropertyChanged(nameof(ConcreteDesignResults));
                }
            }
        }


        private DesignResultModel _currentSelection;
        public DesignResultModel CurrentSelection
        {
            get => _currentSelection;
            set
            {
                if (_currentSelection != value)
                {
                    _currentSelection = value;
                    OnPropertyChanged(nameof(CurrentSelection));
                }
            }
        }



        private ObservableCollection<DesignResultModel> _designResults = new ObservableCollection<DesignResultModel>();
        public ObservableCollection<DesignResultModel> DesignResults
        {
            get => _designResults;
            set
            {
                if (_designResults != value)
                {
                    _designResults = value;
                    OnPropertyChanged(nameof(DesignResults));
                }
            }
        }

        private double _numDesignResults = 0;
        public double NumDesignResults
        {
            get => _numDesignResults;
            set
            {
                if (_numDesignResults != value)
                {
                    _numDesignResults = value;
                    OnPropertyChanged(nameof(NumDesignResults));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
