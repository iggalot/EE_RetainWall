namespace EE_RetainWallLibrary
{
    public class RetWallModel
    {
        public double Height { get; set; } = 10;  // ft
        public double WidthTop { get; set; } = 8; // in.
        public double WidthBot { get; set; } = 8; // in.
        public double StemKeyDepth { get; set; } = 3;  // in.
        public double StemKeyWidth { get; set; } = 3;  // in.
        public double ToeLength { get; set; } = 36;  // in.
        public double ToeThick { get; set; } = 12;  // in.
        public double HeelLength { get; set; } =  12; // in. 
        public double HeelThick { get; set; } = 12; // in.
        public double KeyDepth { get; set; } = 3; // in.
        public double KeyWidth { get; set; } = 8; // in.
        
        /// <summary>
        /// Lateral Pressures on the wall stem, footing, and key
        /// </summary>
        public PressureBlockDataModel wallStemActivePressure { get; set; }
        public PressureBlockDataModel wallFootingActivePressure { get; set; }
        public PressureBlockDataModel wallKeyActivePressure { get; set; }
        public PressureBlockDataModel wallStemPassivePressure { get; set; }
        public PressureBlockDataModel wallFootingPassivePressure { get; set; }
        public PressureBlockDataModel wallKeyPassivePressure { get; set; }

        /// <summary>
        /// Vertica Pressures on the heel, toe, and key
        /// </summary>
        public PressureBlockDataModel wallHeelPressure { get; set; }
        public PressureBlockDataModel wallKeyPressure { get; set; }
        public PressureBlockDataModel wallToePressure { get; set; }
    }

    }
}
