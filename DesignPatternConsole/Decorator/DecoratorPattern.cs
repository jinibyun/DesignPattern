using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole.Decorator
{
    public interface Vehicle
    {
        string Make { get; }
        string Model { get; }
        double Price { get; }
    }

    public class HondaCity : Vehicle
    {
        public string Make
        {
            get { return "HondaCity"; }
        }

        public string Model
        {
            get { return "CNG"; }
        }

        public double Price
        {
            get { return 1000000; }
        }
    }

    // Decorator abstract class
    public abstract class VehicleDecorator : Vehicle
    {
        private Vehicle _vehicle;

        public VehicleDecorator(Vehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public string Make
        {
            get { return _vehicle.Make; }
        }

        public string Model
        {
            get { return _vehicle.Model; }
        }

        public double Price
        {
            get { return _vehicle.Price; }
        }

    }

    // ConcreteDecorator Class

    public class SpecialOffer : VehicleDecorator
    {
        public SpecialOffer(Vehicle vehicle) : base(vehicle) { }

        public int DiscountPercentage { get; set; }
        public string Offer { get; set; }

        public double Price
        {
            get
            {
                double price = base.Price;
                int percentage = 100 - DiscountPercentage;
                return Math.Round((price * percentage) / 100, 2);
            }
        }

    }
}
