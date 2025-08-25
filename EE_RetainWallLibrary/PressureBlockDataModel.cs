namespace EE_RetainWallLibrary
{
    public class PressureBlockDataModel
    {
        public double Pressure1 { get; set; }
        public double Pressure2 { get; set; }
        public double Distance { get; set; }
        public double Location { get; set; }  // offset distance to Pressure1 from key point on member attached.  Like from top of wall, or edge of toe, etc...
    }
}
