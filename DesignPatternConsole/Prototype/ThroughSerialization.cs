using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DesignPatternConsole.Prototype
{
    // NOTE: point is serialize and deserialize again to "deep copy"
    public static class ExtensionMethods
    {
        // 1
        public static T DeepCopy<T>(this T self)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        // 2
        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }
    }

    //[Serializable] // this is, unfortunately, required if we use #1 above
    public class Foo
    {
        public uint Stuff;
        public string Whatever;
        public SubFoo SomeProperty { get; set; }
         
        public override string ToString()
        {
            return $"{nameof(Stuff)}: {Stuff}, {nameof(Whatever)}: {Whatever}, {nameof(SomeProperty)}: {SomeProperty.someProperty2.someProperty}";
        }
    }

    public class SubFoo
    {
        public string someProperty { get; set; }       
        public SubFoo2 someProperty2 { get; set; }
    }

    public class SubFoo2
    {
        public string someProperty { get; set; }        
    }
}
