using System;

namespace EE_RetainWallLibrary
{
    public class PressureBlockDataModel
    {
        public double Pressure1 { get; set; }
        public double Pressure2 { get; set; }
        public double Distance { get; set; } // width of the pressure block
        public double Location { get; set; }  // offset distance to Pressure1 from key point on member attached.  Like from top of wall, or edge of toe, etc...

        public double Force { get => ComputeResultantForce(); } // resultant force of the pressure diagram
        public double Centroid { get => ComputeCentroid(); } // location of resultant from reference point


        // Computes the resultant force of the pressure area
        private double ComputeResultantForce()
        {
            return 0.5 * (Pressure1 + Pressure2) * Distance;
        }

        private double ComputeCentroid()
        {
            double tri_area = (0.5) * Math.Abs(Pressure2 - Pressure1) * Distance;
            double rect_area = (Pressure2 > Pressure1) ? Pressure2 * Distance : Pressure1 * Distance;
            double rect_centroid = 0.5 * Distance;
            double tri_centroid = 0;

            if (Pressure2 > Pressure1)
            {
                tri_centroid = 2.0 * Distance / 3.0 + Location;
            }
            else if (Pressure2 < Pressure1)
            {
                tri_centroid = Distance / 3.0 + Location;
            } else
            {
                tri_centroid = Distance / 2.0 + Location;
            }

            return (tri_area * tri_centroid + rect_area * rect_centroid) / (tri_area + rect_area);
        }

        public string DisplayInfo()
        {
            string str = string.Empty;
            str += $"P1={Pressure1} psf at Loc: {Location} amd P2={Pressure2} psf at Loc: {Location + Distance} from reference.";
            str += $"-- TotalForce={Force} lbs at Loc: {Centroid} from reference";

            return str;
        }

    }
}
