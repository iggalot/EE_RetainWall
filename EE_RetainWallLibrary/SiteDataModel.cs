namespace EE_RetainWallLibrary
{
    public class SiteDataModel
    {
        public double SoilSurchargeSlopeAngle { get; set; } = 0;
        public double SoilSurfaceDepth { get; set; } = 0; // depth behind wall where soil depth occurs
        public double H20TableDepth { get; set; } = 1000;  // distance (ft) from ground elevation to water table depth
        public double BedrockDepth { get; set; } = 1000; // distance (ft) from ground elevatiomn to bedrock depth

        public double LoadSurchargeQ { get; set; } = 0;  // additional vertical surcharge load behind the wall
    }
}
