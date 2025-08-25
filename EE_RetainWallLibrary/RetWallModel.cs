using System;

namespace EE_RetainWallLibrary
{
    public class RetWallModel
    {
        private double concrete_density = 150; // pcf
        public double Height { get; set; }  // ft
        public double Base { get; set; } // ft
        public double WidthTop { get; set; } // in.
        public double WidthBot { get; set; } // in.
        public double StemKeyDepth { get; set; }  // in.
        public double StemKeyWidth { get; set; }  // in.
        public double ToeLength { get; set; }  // in.
        public double FootingThick { get; set; } = 12;  // in.
        public double HeelLength { get; set; } // in. 
        public double KeyDepth { get; set; } = 12; // in.
        public double KeyWidth { get; set; } // in.
        
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
