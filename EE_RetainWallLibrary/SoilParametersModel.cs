using System;

namespace EE_RetainWallLibrary
{
    public class SoilParametersModel
    {
        public double ShearAnglePhi { get; set; } = 30; // internal shear friction degrees
        public double Cohesion { get; set; } = 20; // cohesion of soil
        public double Density { get; set; } = 120; // pcf

        public double Ka { get => GetKa(); } // active earth pressure
        public double Kp { get => GetKp(); } // passive earth pressure

        private double GetKa()
        {
            var sin_phi = Math.Sin(ShearAnglePhi * Math.PI / 180);
            return (1 - sin_phi) / (1 + sin_phi);
        }

        private double GetKp()
        {
            var sin_phi = Math.Sin(ShearAnglePhi * Math.PI / 180);
            return (1 + sin_phi) / (1 - sin_phi);
        }
    }
}
