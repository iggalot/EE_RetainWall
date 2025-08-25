namespace EE_RetainWallLibrary
{
    enum RebarSizes
    {
        Num3,   //#3
        Num4,   //#4
        Num5,   //#5
        Num6,   //#6
        Num7,   //#7
        Num8,   //#8
        Num9,   //#9
        Num10,   //#10
        Num11,   //#11
    }

    enum RebarType
    {
        Horizontal,
        Vertical,
        UStirrup,
        ClosedStirrup,
        LBars
    }

    internal class RebarDetailsModel
    {
        public RebarSizes rebarSize { get; set; } = RebarSizes.Num4;
        public RebarType rebarType { get; set; }
        public double Length1 { get; set; } = 12;
        public double Length2 { get; set; } = 12;
        public double BendDia { get; set; } = 3;
        public double Spacing { get; set; } = 6;
        public double CoverReq { get; set; } = 3;
    }
}
