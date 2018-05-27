using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternConsole.Builder;
using DesignPatternConsole.Factory;
using DesignPatternConsole.Prototype;
using DesignPatternConsole.Singleton;
using DesignPatternConsole.Solid_Design_Principles;

namespace DesignPatternConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // -------------- SOLID
            // SingleResponsibilityPrinciple();
            // OpenClosePrincipal();
            // LiskovSubstitutePrincipal();
            // InterfaceSeggregation();
            // DependencyInversion();

            // ------------- Each Patterns
            // Builder(); 
            // NOTE: Fluent Builder inheritance with recursive generics is skipped for unnecessary complexity
            // FacadeBuilder();
            // ExerciseBuilder();

            // ------------- Factory Patterns
            // CreationalFactory();
            // AbstractFactory();
            // ExerciseFactory();

            // ------------- Prototype Pattern
            // ICloneableIsBad(); // do not use this
            // CopyConstructor();
            // ThroughSerialization(); // can cover disadvantate of copy constructor
            // ExercisePrototype();

            // ------------- Singleton Pattern
            SingletonPattern(); // initialze only once 
            // NOTE: Go to UnitTest in more detail about how to avoid singleton pattern

        }

        private static void SingletonPattern()
        {
            
            var db = SingletonDatabase.Instance;

            // works just fine while you're working with a real database.
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");

            
        }

        private static void ExercisePrototype()
        {
            var line1 = new Line
            {
                Start = new Prototype.Point { X = 3, Y = 3 },
                End = new Prototype.Point { X = 10, Y = 10 }
            };

            var line2 = line1.DeepCopy();
            line1.Start.X = line1.End.X = line1.Start.Y = line1.End.Y = 0;

            Console.WriteLine(line2.Start.X);
            Console.WriteLine(line2.Start.Y);
            Console.WriteLine(line2.End.X);
            Console.WriteLine(line2.End.Y);
        }

        private static void ThroughSerialization()
        {
            Foo foo = new Foo { Stuff = 42, Whatever = "abc", SomeProperty = new SubFoo {  someProperty2 = new SubFoo2 { someProperty = "11111" } } };

            //Foo foo2 = foo.DeepCopy(); // crashes without [Serializable]
            Foo foo2 = foo.DeepCopyXml();

            foo2.Whatever = "xyz";
            foo2.SomeProperty.someProperty2.someProperty = "22222";
            Console.WriteLine(foo);
            Console.WriteLine(foo2);
        }

        private static void CopyConstructor()
        {
            var john = new Employee2("John", new Address2("123 London Road", "London", "UK"));

            //var chris = john;
            var chris = new Employee2(john);

            // NOTE: we can use interface (with this conceopt: copy contructor, but caveat of this is to let all classess should implment to achive "deep copy"

            chris.Name = "Chris";            
            Console.WriteLine(john);
            Console.WriteLine(chris);
        }

        private static void ICloneableIsBad()
        {
            var john = new DesignPatternConsole.Prototype.Person(new[] { "John", "Smith" }, new DesignPatternConsole.Prototype.Address("London Road", 123));

            var jane = (DesignPatternConsole.Prototype.Person)john.Clone();
            jane.Address.HouseNumber = 321; // oops, John is now at 321

            // this doesn't work
            // var jane = john;

            // but clone is typically shallow copy
            jane.Names[0] = "Jane";

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }

        private static void ExerciseFactory()
        {
            var pf = new PersonFactory();

            var p1 = pf.CreatePerson("Chris");
            Console.WriteLine(p1.Name);
            Console.WriteLine(p1.Id);

            var p2 = pf.CreatePerson("Sarah");
            Console.WriteLine(p2.Id);
        }

        private static void AbstractFactory()
        {
            var machine = new HotDrinkMachine();

            // 1
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 300);
            //drink.Consume();

            // 2. more advance: open close principal
            IHotDrink drink = machine.MakeDrink();
            drink.Consume();
        }

        private static void CreationalFactory()
        {
            // without factory
            // var p1 = new Point(2, 3, CoordinateSystem.Cartesian);
            var origin = Factory.Point.Origin2;

            var p1 = Factory.Point.Factory.NewCartesianPoint(1, 2);
            var p2 = Factory.Point.Factory.NewPolarPoint(3, 9);

        }

        private static void ExerciseBuilder()
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb.ToString());
        }

        private static void FacadeBuilder()
        {
            var pb = new PersonBuilder();
            Builder.Person person = pb
                .Lives
                    .At("123 London Road")
                    .In("London")
                    .WithPostcode("SW12BC")
                .Works
                    .At("Fabrikam")
                    .AsA("Engineer")
                    .Earning(123000);

            Console.WriteLine(person);
        }

        private static void Builder()
        {
            // ------- without builder pattern
            // if you want to build a simple HTML paragraph using StringBuilder
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            Console.WriteLine(sb);

            // now I want an HTML list with 2 words in it
            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");
            Console.WriteLine(sb);

            // ------- with builder pattern
            // ordinary non-fluent builder
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            Console.WriteLine(builder.ToString());

            // fluent builder
            sb.Clear();
            builder.Clear(); // disengage builder from the object it's building, then...
            builder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
            Console.WriteLine(builder);
        }

        private static void DependencyInversion()
        {
            // presume: we have following data (We just initialized for example
            var parent = new DesignPatternConsole.Solid_Design_Principles.Person { Name = "John" };
            var child1 = new DesignPatternConsole.Solid_Design_Principles.Person { Name = "Chris" };
            var child2 = new DesignPatternConsole.Solid_Design_Principles.Person { Name = "Matt" };

            // low-level module
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            // execute
            new Research(relationships);
        }

        private static void InterfaceSeggregation()
        {
            IMultiFunctionDevice multi = new MultiFunctionMachine(new Printer(), new Scanner());
            multi.Print(new Document());
            multi.Scan(new Document());
        }

        private static void LiskovSubstitutePrincipal()
        {
            Rectangle rc = new Rectangle(2, 3);
            Console.WriteLine($"{rc} has area {Area(rc)}");

            // should be able to substitute a base type for a subtype
            /*Square*/
            Rectangle sq = new Square();
            sq.Width = 4;
            Console.WriteLine($"{sq} has area {Area(sq)}");

        }
        static public int Area(Rectangle r) => r.Width * r.Height;

        private static void OpenClosePrincipal()
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();
            Console.WriteLine("Green products (old):");
            foreach (var p in pf.FilterByColor(products, Color.Green))
                Console.WriteLine($" - {p.Name} is green");

            // ^^ BEFORE

            // vv AFTER
            var bf = new BetterFilter();
            Console.WriteLine("Green products (new):");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
                Console.WriteLine($" - {p.Name} is green");

            Console.WriteLine("Large products");
            foreach (var p in bf.Filter(products, new SizeSpecification(Size.Large)))
                Console.WriteLine($" - {p.Name} is large");

            Console.WriteLine("Large blue items");
            foreach (var p in bf.Filter(products,
                new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large)))
            )
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }
        }

        private static void SingleResponsibilityPrinciple()
        {
            var j = new Journal();
            j.AddEntry("I cried today.");
            j.AddEntry("I ate a bug.");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(j, filename);
            Process.Start(filename);
        }
    }
}
