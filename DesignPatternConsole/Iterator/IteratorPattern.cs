using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternConsole.Iterator
{
    public interface Iterator
    {
        object Current { get; }
        bool Next();

    }
    public interface Aggregate
    {
        Iterator CreateIterator();
    }

    public class ConcreteAggregate : Aggregate
    {
        private ArrayList items = new ArrayList();

        public Iterator CreateIterator()
        {
            return new ConcreteIterator(this);
        }

        public object this[int index]
        {
            get { return items[index]; }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public void Add(object o)
        {
            items.Add(o);
        }
    }


    public class ConcreteIterator : Iterator
    {
        private ConcreteAggregate aggregate;
        int index;

        public ConcreteIterator(ConcreteAggregate aggregate)
        {
            this.aggregate = aggregate;
            index = -1;
        }

        public bool Next()
        {
            index++;
            return index < aggregate.Count;
        }

        public object Current
        {
            get
            {
                if (index < aggregate.Count)
                    return aggregate[index];
                else
                    throw new InvalidOperationException();
            }
        }
    }
}
