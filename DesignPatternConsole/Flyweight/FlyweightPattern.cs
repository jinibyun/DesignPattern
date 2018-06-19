using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole.Flyweight
{
    interface IShape
    {
        void Print();
    }


    class RectangleShape : IShape
    {
        public void Print()
        {
            Console.WriteLine("Printing Rectangle");
        }
    }

    class CircleShape : IShape
    {
        public void Print()
        {
            Console.WriteLine("Printing Circle");
        }
    }

    /// <summary>
    /// The 'FlyweightFactory' class
    /// Flyweight is used when there is a need to create a large number of objects of almost similar nature and storage cost is high.
    /// Similar to Cache
    /// Most of the state can be kept on disk or calculated at runtime.
    /// </summary>
    class ShapeObjectFactory
    {
        Dictionary<string, IShape> shapes = new Dictionary<string, IShape>();

        public int TotalObjectsCreated
        {
            get { return shapes.Count; }
        }

        public IShape GetShape(string ShapeName)
        {
            IShape shape = null;
            if (shapes.ContainsKey(ShapeName))
            {
                shape = shapes[ShapeName];
            }
            else
            {
                switch (ShapeName)
                {
                    case "RectangleShape":
                        shape = new RectangleShape();
                        shapes.Add("RectangleShape", shape);
                        break;
                    case "CircleShape":
                        shape = new CircleShape();
                        shapes.Add("CircleShape", shape);
                        break;
                    default:
                        throw new Exception("Factory cannot create the object specified");
                }
            }
            return shape;
        }
    }
}
