using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternConsole.Builder;
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
            Bulder(); // NOTE: Fluent Builder inheritance with recursive generics is skipped for unnecessary complexity

        }

        private static void Bulder()
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
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Matt" };

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
