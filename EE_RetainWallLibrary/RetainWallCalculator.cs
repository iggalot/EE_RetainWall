using System;

namespace EE_RetainWallLibrary
{
    public static class RetainWallCalculator
    {
        public static RetWallModel Create(
            double ht,
            SiteDataModel site_data,
            SoilParametersModel soil_parameters)
        {
            var new_wall = new RetWallModel();
            new_wall.Height = ht;

            new_wall.wallStemActivePressure = ComputeActivePressureOnWallStem(new_wall, site_data, soil_parameters);
            new_wall.wallStemPassivePressure = ComputePassivePressureOnWallStem(new_wall, site_data, soil_parameters);

            return new_wall;
        }

        public static PressureBlockDataModel ComputeActivePressureOnWallStem(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            var model = new PressureBlockDataModel();
            var press_ht = wall.Height - site_data.SoilSurfaceDepthBehind;

            model.Pressure1 = soil.Ka * soil.Density * 0;
            model.Pressure2 = soil.Ka * soil.Density * (press_ht);
            model.Location = site_data.SoilSurfaceDepthBehind;
            model.Distance = (press_ht);

            return model;
        }

        public static PressureBlockDataModel ComputePassivePressureOnWallStem(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            var model = new PressureBlockDataModel();
            var press_ht = wall.Height - site_data.SoilSurfaceDepthFront;


            model.Pressure1 = soil.Kp * soil.Density * 0;
            model.Pressure2 = soil.Kp * soil.Density * (press_ht);
            model.Location = site_data.SoilSurfaceDepthFront;
            model.Distance = (press_ht);

            return model;
        }
    }
}
