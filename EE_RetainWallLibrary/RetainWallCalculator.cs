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

            new_wall.wallStemActivePressure = ComputeActivePressureOnWallStem();
            return new_wall;
        }

        private static void ComputeActivePressuresOnWallStem()
        {

        }
    }
}
