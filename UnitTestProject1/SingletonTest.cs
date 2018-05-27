using System;
using Autofac;
using DesignPatternConsole.Singleton;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class SingletonTest
    {
        [TestMethod]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.AreEqual(db, db2);
            Assert.IsTrue(SingletonDatabase.Count == 1);
        }

        [TestMethod]
        public void SingletonTotalPopulationTest()
        {
            // testing on a live database
            var rf = new SingletonRecordFinder();
            var names = new[] { "Seoul", "Mexico City" };
            int tp = rf.TotalPopulation(names);
            Assert.AreEqual(tp, 17500000 + 17400000);
        }

        [TestMethod]
        public void DependantTotalPopulationTest()
        {
            // NOTE: singleton is hard to test.
            var db = new DummyDatabase();
            var rf = new ConfigurableRecordFinder(db);
            Assert.AreEqual(
              rf.GetTotalPopulation(new[] { "alpha", "gamma" }),
              4);
        }

        [TestMethod]
        public void DIPopulationTest()
        {
            // autofac setup
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>().As<IDatabase>()
                .SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();

            using (var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
                var rf2 = c.Resolve<ConfigurableRecordFinder>();

                
                Assert.IsTrue(
                    rf.GetTotalPopulation(new string[] { "Seoul" }) == 
                    rf2.GetTotalPopulation(new string[] { "Seoul" }));

                Assert.IsTrue(rf2.Count == 1);
            }
        }        
    }
}
