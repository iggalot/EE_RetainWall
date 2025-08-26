using System;

namespace EE_RetainWallLibrary
{
    public static class SoilCalculator
    {
        public static PressureBlockDataModel ComputePassivePressureOnWallStem(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            var model = new PressureBlockDataModel();
            model.Name = "Base Slab - Passive Pressure";

            var press_ht = wall.Height - site_data.SoilSurfaceDepthFront;

            model.Pressure1 = soil.Kp * soil.Density * 0;
            model.Pressure2 = soil.Kp * soil.Density * (press_ht);
            model.Location = site_data.SoilSurfaceDepthFront;
            model.Distance = (press_ht);

            return model;
        }

        public static PressureBlockDataModel ComputePassivePressureOnBaseSlab(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            var model = new PressureBlockDataModel();
            model.Name = "Passive Pressure - Base Slab";
            var press_ht = wall.Height;
            var start_depth = press_ht;
            var end_depth = press_ht + wall.FootingThick / 12.0;

            model.Pressure1 = soil.Kp * soil.Density * start_depth;
            model.Pressure2 = soil.Kp * soil.Density * end_depth;
            model.Location = press_ht;
            model.Distance = (end_depth - start_depth);

            return model;
        }

        public static PressureBlockDataModel ComputePassivePressureOnKey(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            var model = new PressureBlockDataModel();
            model.Name = "Passive Pressure - Key";

            var press_ht = wall.Height + wall.FootingThick / 12.0;

            var start_depth = press_ht;
            var end_depth = press_ht + wall.KeyDepth / 12.0;

            model.Pressure1 = soil.Kp * soil.Density * start_depth;
            model.Pressure2 = soil.Kp * soil.Density * end_depth;
            model.Location = press_ht;
            model.Distance = (end_depth - start_depth);

            return model;
        }

        public static PressureBlockDataModel ComputeActivePressureOnWallStem(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            var model = new PressureBlockDataModel();
            model.Name = "Active Pressure - Wall Stem";

            var press_ht = wall.Height - site_data.SoilSurfaceDepthBehind;

            model.Pressure1 = soil.Ka * soil.Density * 0;
            model.Pressure2 = soil.Ka * soil.Density * (press_ht);
            model.Location = site_data.SoilSurfaceDepthBehind;
            model.Distance = (press_ht);

            return model;
        }

        public static PressureBlockDataModel ComputeActivePressureOnBaseSlab(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            var model = new PressureBlockDataModel();
            model.Name = "Active Pressure - Base Slab";

            var press_ht = wall.Height;
            var start_depth = press_ht;
            var end_depth = press_ht + wall.FootingThick / 12.0;

            model.Pressure1 = soil.Ka * soil.Density * start_depth;
            model.Pressure2 = soil.Ka * soil.Density * end_depth;
            model.Location = press_ht;
            model.Distance = (end_depth - start_depth);

            return model;
        }

        public static PressureBlockDataModel ComputeActivePressureOnKey(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            var model = new PressureBlockDataModel();
            model.Name = "Active Pressure - Key";

            var press_ht = wall.Height + wall.FootingThick / 12.0;
            var start_depth = press_ht;
            var end_depth = press_ht + wall.KeyDepth / 12.0;

            model.Pressure1 = soil.Ka * soil.Density * start_depth;
            model.Pressure2 = soil.Ka * soil.Density * end_depth;
            model.Location = press_ht;
            model.Distance = (end_depth - start_depth);

            return model;
        }
    }
}
