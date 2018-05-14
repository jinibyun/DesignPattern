using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole.Factory
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonFactory
    {
        private int id = 0;

        public Person CreatePerson(string name)
        {
            return new Person { Id = id++, Name = name };
        }
    }
}
