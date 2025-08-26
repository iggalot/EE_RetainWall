using System;
using System.ComponentModel;

namespace EE_RetainWallLibrary
{
    public class RetWallModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private double concrete_density = 150; // pcf
        private double _height;
        private double _base;
        private double _widthTop;
        private double _widthBot;
        private double _toeLength;
        private double _footingThick = 12;
        private double _heelLength;
        private double _keyDepth = 12;
        private double _keyWidth;

        public double Height { get => _height; set { _height = value; OnPropertyChanged(nameof(Height)); } }
        public double Base { get => _base; set { _base = value; OnPropertyChanged(nameof(Base)); } }
        public double WidthTop { get => _widthTop; set { _widthTop = value; OnPropertyChanged(nameof(WidthTop)); } }
        public double WidthBot { get => _widthBot; set { _widthBot = value; OnPropertyChanged(nameof(WidthBot)); } }
        public double ToeLength { get => _toeLength; set { _toeLength = value; OnPropertyChanged(nameof(ToeLength)); } }
        public double FootingThick { get => _footingThick; set { _footingThick = value; OnPropertyChanged(nameof(FootingThick)); } }
        public double HeelLength { get => _heelLength; set { _heelLength = value; OnPropertyChanged(nameof(HeelLength)); } }
        public double KeyDepth { get => _keyDepth; set { _keyDepth = value; OnPropertyChanged(nameof(KeyDepth)); } }
        public double KeyWidth { get => _keyWidth; set { _keyWidth = value; OnPropertyChanged(nameof(KeyWidth)); } }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Height):
                        return Height <= 0 ? "Height must be positive (ft)." : null;
                    case nameof(Base):
                        return Base <= 0 ? "Base must be positive (ft)." : null;
                    case nameof(FootingThick):
                        return FootingThick < 6 ? "Footing thickness should be ≥ 6 in." : null;
                    default:
                        return null;
                }
            }
        }

        public string Error => null;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Lateral Pressures on the wall stem, footing, and key
        /// </summary>
        public PressureBlockDataModel wallStemActivePressure { get; set; }
        public PressureBlockDataModel wallStemPassivePressure { get; set; }
        public PressureBlockDataModel wallFootingActivePressure { get; set; }
        public PressureBlockDataModel wallFootingPassivePressure { get; set; }
        public PressureBlockDataModel wallKeyActivePressure { get; set; }
        public PressureBlockDataModel wallKeyPassivePressure { get; set; }

        /// <summary>
        /// Vertica Pressures on the heel, toe, and key
        /// </summary>
        public PressureBlockDataModel wallHeelPressure { get; set; }
        public PressureBlockDataModel wallKeyPressure { get; set; }
        public PressureBlockDataModel wallToePressure { get; set; }

        public double Mo_Stem { get; set; }
        public double V_Stem { get; set; }

        public string DisplayInfo()
        {
            string str = String.Empty;
            str += $"\nActive Pressures: Ht: {Height}";
            str += $"\n--WallStem: Ht: {Height} Active: " + wallStemActivePressure.DisplayInfo();
            str += $"\n--BaseSlab: Ht: {FootingThick / 12.0} Active: " + wallFootingActivePressure.DisplayInfo();
            str += $"\n--Key: Ht: {KeyDepth / 12.0} ft.  Active: " + wallKeyActivePressure.DisplayInfo();
            str += "\n----------------------------------------------";
            str += $"\n--WallStem: Ht: {Height}  Passive" + wallStemPassivePressure.DisplayInfo();
            str += $"\n--BaseSlab: Ht: {FootingThick / 12.0}  Passive" + wallFootingPassivePressure.DisplayInfo();
            str += $"\n--Key: Ht: {KeyDepth / 12.0} ft.  Passive" + wallKeyPassivePressure.DisplayInfo();
            str += "\n----------------------------------------------";

            return str;
        }

        public double KeyWeight()
        {
            return KeyWidth * Height * concrete_density / 12.0;
        }

        public double KeyCentroidFromToe()
        {
            return ToeLength + 0.5 * KeyWidth / 12.0;
        }

        public double KeyOverturningMomentAboutToe()
        {
            return KeyWeight() * KeyCentroidFromToe();
        }
        public double StemWeight()
        {
            return 0.5 * (WidthTop + WidthBot) / 12.0 * Height * concrete_density;
        }

        public double StemCentroidFromToe()
        {
            var rect_area = WidthTop;
            var rect_centroid = 0.5 * WidthTop;  // from face of wall
            var tri_area = 0.5 * (WidthBot - WidthTop);
            var tri_centroid = WidthTop + WidthBot / 3.0;  // from face of wall

            return ToeLength + (rect_area * rect_centroid + tri_area * tri_centroid) / (rect_area + tri_area) / 12.0;
        }

        public double StemOverturningMomentAboutToe()
        {
            return StemWeight() * StemCentroidFromToe();
        }

        public double BaseSlabWeight()
        {
            return (HeelLength * FootingThick + ToeLength * FootingThick + (WidthBot / 12.0)) * FootingThick * concrete_density;
        }

        public double BaseSlabCentroidFromToe()
        {
            return 0.5 * (HeelLength + ToeLength + WidthBot / 12.0);
        }

        public double BaseSlabMomentAboutToe()
        {
            return BaseSlabWeight() * BaseSlabCentroidFromToe();
        }

        public double ComputeOverturningMomentAboutToe()
        {
            return BaseSlabMomentAboutToe() + StemOverturningMomentAboutToe() + KeyOverturningMomentAboutToe();
        }

        public static RetWallModel ProportionInitial(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            RetWallModel new_model = wall;
            new_model.Height = wall.Height;
            new_model.Base = 0.65 * wall.Height;
            new_model.HeelLength = 0.65 * new_model.Base;
            new_model.ToeLength = 0.35 * new_model.Base;
            new_model.FootingThick = 12;
            new_model.WidthTop = 9;
            new_model.WidthBot = 14;

            return new_model;
        }

        public static RetWallModel Create(
            double ht,
            SiteDataModel site_data,
            SoilParametersModel soil_parameters)
        {
            var new_wall = new RetWallModel
            {
                Height = ht
            };

            // Set up the initial trial dimensions
            new_wall = ProportionInitial(new_wall, site_data, soil_parameters);

            // Set up the initial pressures.
            new_wall.wallStemActivePressure = SoilCalculator.ComputeActivePressureOnWallStem(new_wall, site_data, soil_parameters);
            new_wall.wallFootingActivePressure = SoilCalculator.ComputeActivePressureOnBaseSlab(new_wall, site_data, soil_parameters);
            new_wall.wallKeyActivePressure = SoilCalculator.ComputeActivePressureOnKey(new_wall, site_data, soil_parameters);


            new_wall.wallStemPassivePressure = SoilCalculator.ComputePassivePressureOnWallStem(new_wall, site_data, soil_parameters);
            new_wall.wallFootingPassivePressure = SoilCalculator.ComputePassivePressureOnBaseSlab(new_wall, site_data, soil_parameters);
            new_wall.wallKeyPassivePressure = SoilCalculator.ComputePassivePressureOnKey(new_wall, site_data, soil_parameters);



            return new_wall;
        }
    }
}
