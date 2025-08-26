namespace EE_RetainWallLibrary
{
    public class RetainingWallPressureResults
    {
        public PressureBlockDataModel WallStemActivePressure { get; set; }
        public PressureBlockDataModel WallStemPassivePressure { get; set; }
        public PressureBlockDataModel WallFootingActivePressure { get; set; }
        public PressureBlockDataModel WallFootingPassivePressure { get; set; }
        public PressureBlockDataModel WallKeyActivePressure { get; set; }
        public PressureBlockDataModel WallKeyPassivePressure { get; set; }

        public PressureBlockDataModel WallHeelPressure { get; set; }
        public PressureBlockDataModel WallKeyPressure { get; set; }
        public PressureBlockDataModel WallToePressure { get; set; }
    }
}
