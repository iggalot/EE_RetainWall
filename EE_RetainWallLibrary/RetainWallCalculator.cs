namespace EE_RetainWallLibrary
{
    public class RetainWallCalculator
    {
        public double OverTurningMoment { get; set; } = 0;

        public static double ComputeMo_WallStem(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil )
        {
            return 0.0;
        }

        public static double ToeSoilWeight(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            return wall.ToeLength * (wall.Height - site_data.SoilSurfaceDepthFront) * soil.Density;
        }

        public static double ToeSoilCentroidFromToe(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            return wall.ToeLength + wall.WidthBot / 12.0 + 0.5 * wall.HeelLength;
        }

        public static double ToeSoilOverturningMomentAboutToe(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            return ToeSoilWeight(wall, site_data, soil) * ToeSoilCentroidFromToe(wall, site_data, soil);
        }

        public static double HeelSoilWeight(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            return wall.HeelLength * (wall.Height-site_data.SoilSurfaceDepthBehind) * soil.Density;
        }

        public static double HeelSoilCentroidFromToe(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            return wall.ToeLength + wall.WidthBot / 12.0 + 0.5 * wall.HeelLength;
        }

        public static double HeelSoilOverturningMomentAboutToe(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            return HeelSoilWeight(wall, site_data, soil) * HeelSoilCentroidFromToe(wall, site_data, soil);
        }

        public double ComputeSystemOverturningMoment(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {

            return wall.ComputeOverturningMomentAboutToe() + HeelSoilOverturningMomentAboutToe(wall, site_data, soil) + ToeSoilOverturningMomentAboutToe(wall, site_data, soil);
        }

        public double ComputeSystemVerticalLoad(RetWallModel wall, SiteDataModel site_data, SoilParametersModel soil)
        {
            return wall.BaseSlabWeight() + wall.StemWeight() + wall.KeyWeight() + HeelSoilWeight(wall, site_data, soil) + ToeSoilWeight(wall, site_data, soil); 
        }
    }
}
