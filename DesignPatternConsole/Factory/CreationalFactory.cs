using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole.Factory
{
    //public enum CoordinateSystem
    //{
    //    Cartesian,
    //    Polar
    //}

    public class Point
    {
        private double x, y;
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        //public Point(double a,
        //    double b, // names do not communicate intent
        //    CoordinateSystem cs = CoordinateSystem.Cartesian)
        //{
        //    switch (cs)
        //    {
        //        case CoordinateSystem.Polar:
        //            x = a * Math.Cos(b);
        //            y = a * Math.Sin(b);
        //            break;
        //        default:
        //            x = a;
        //            y = b;
        //            break;
        //    }

        //    // steps to add a new system
        //    // 1. augment CoordinateSystem
        //    // 2. change ctor
        //}

        // factory property
        // public static Point Origin => new Point(0, 0);

        // singleton field
        public static Point Origin2 = new Point(0, 0); // better than factory property

        // factory method: violate single responsibility. therefore use inner class as below
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }
        public static Point NewPolarPoint(double rho, double theta)
        {
            //...
            return null;
        }

        // make it lazy
        // make inner class: better than factory method
        public static class Factory
        {
            public static Point NewCartesianPoint(float x, float y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(float rho, float theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }
}
