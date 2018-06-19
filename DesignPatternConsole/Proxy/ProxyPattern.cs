using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole.Proxy
{
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car being driven");
        }
    }

    public class Driver
    {
        public int Age { get; set; }

        public Driver(int age)
        {
            Age = age;
        }
    }

    // proxy: provide client with interface of system (external or whatever...)
    // replicating actual class
    public class CarProxy : ICar
    {
        private Car car = new Car();
        private Driver driver; // for new business logc

        public CarProxy(Driver driver)
        {
            this.driver = driver;
        }

        public void Drive() // replicate
        {
            if (driver.Age >= 16)
                car.Drive();
            else
            {
                Console.WriteLine("Driver too young");
            }
        }
    }
}
