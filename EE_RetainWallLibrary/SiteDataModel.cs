namespace EE_RetainWallLibrary
{
    public class SiteDataModel
    {
        public double SoilSurchargeSlopeAngle { get; set; } = 0;
        public double SoilSurfaceDepthBehind { get; set; } = 0; // depth behind wall where soil depth occurs
        public double SoilSurfaceDepthFront { get; set; } = 0; // depth behind wall where soil depth occurs

        public double H20TableDepth { get; set; } = 1000;  // distance (ft) from ground elevation to water table depth
        public double BedrockDepth { get; set; } = 1000; // distance (ft) from ground elevatiomn to bedrock depth

        public double LoadSurchargeQ { get; set; } = 0;  // additional vertical surcharge load behind the wall

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="height"></param>
        public SiteDataModel(double depth_front, double depth_behind)
        {
            SoilSurfaceDepthBehind = depth_behind;
            SoilSurfaceDepthFront = depth_front;
        }
    }
}
