using System;

namespace EE_RetainWallLibrary
{
    public class PressureBlockDataModel
    {
        public double Pressure1 { get; set; }
        public double Pressure2 { get; set; }
        public double Distance { get; set; } // width of the pressure block
        public double Location { get; set; }  // offset distance to Pressure1 from key point on member attached.  Like from top of wall, or edge of toe, etc...

        public double ForcePerFt { get => ComputeResultantForcePerFoot(); } // resultant force of the pressure diagram
        public double Centroid { get => ComputeCentroid(); } // location of resultant from reference point


        // Computes the resultant force of the pressure area
        private double ComputeResultantForcePerFoot()
        {
            return 0.5 * (Pressure1 + Pressure2) * Distance;
        }

        private double ComputeCentroid()
        {
            double h = Distance;
            double Ptop = Pressure1;
            double Pbot = Pressure2;

            double tri_area = 0.5 * Math.Abs(Pbot - Ptop) * h;
            double rect_area = Math.Min(Ptop, Pbot) * h;

            double rect_centroid = h / 2.0; // from top of block
            double tri_centroid;

            if (Pbot > Ptop)
                tri_centroid = 2.0 * h / 3.0; // triangle on top of rectangle
            else if (Ptop > Pbot)
                tri_centroid = h / 3.0;        // triangle at bottom
            else
                tri_centroid = h / 2.0;        // rectangle only

            double centroid_from_top_of_block = (tri_area * tri_centroid + rect_area * rect_centroid) / (tri_area + rect_area);

            return Location + centroid_from_top_of_block; // depth from reference
        }

        public string DisplayInfo()
        {
            string str = string.Empty;
            str += $"P1={Pressure1} psf at Loc: {Location} amd P2={Pressure2} psf at Loc: {Location + Distance} from reference.";
            str += $"-- TotalForce={ForcePerFt} lbs at Loc: {Centroid} from reference";

            return str;
        }

    }
}
