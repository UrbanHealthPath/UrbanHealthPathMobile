using System;
using System.Linq;
using Mapbox.Utils;

namespace PolSl.UrbanHealthPath.Map
{
    /// <summary>
    /// Calculates the given angle to a smoother value.
    /// </summary>
    public class AngleSmootherLowPas
    {
        private int _measurments = 5;

        private CircularBuffer<double> _angles;

        private double _smoothingFactor = 0.5;

        private const double DEG_TO_RAD = Math.PI / 180.0d;

        private const double RAD_TO_DEG = 180.0d / Math.PI;
        
        public AngleSmootherLowPas()
        {
            _angles = new CircularBuffer<double>(_measurments);
        }

        public AngleSmootherLowPas(int measurments)
        {
            _measurments = measurments;
            _angles = new CircularBuffer<double>(_measurments);

        }
        
        public void Add(double angle)
        {
            angle = angle < 0 ? angle + 360 : angle >= 360 ? angle - 360 : angle;
            _angles.Add(angle); 
        }

        public double Calculate()
        {
            double[] angles = _angles.Reverse().ToArray();
            double lastSin = Math.Sin(angles[0] * DEG_TO_RAD);
            double lastCos = Math.Cos(angles[0] * DEG_TO_RAD);
            
            for (int i = 1; i < angles.Length; i++)
            {
                double angle = angles[i];
                lastSin = _smoothingFactor * Math.Sin(angle * DEG_TO_RAD) + (1 - _smoothingFactor) * lastSin;
                lastCos = _smoothingFactor * Math.Cos(angle * DEG_TO_RAD) + (1 - _smoothingFactor) * lastCos;
            }
            
            double finalAngle = Math.Round(Math.Atan2(lastSin, lastCos) * RAD_TO_DEG, 2);
            finalAngle = finalAngle < 0 ? finalAngle + 360 : finalAngle >= 360 ? finalAngle - 360 : finalAngle;
            return finalAngle;
        }
    }
}
