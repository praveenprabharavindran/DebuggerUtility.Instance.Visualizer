using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoFixture;
using DebuggerUtility.Visualizers.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VisualizerTests
{
    [TestClass]
    public class DebuggerVisualizerTests
    {
        [TestMethod]
        public void RunTimeCheck()
        {
            var dict = new Dictionary<string, List<string>>();

            var fixture = new Fixture();
            var metrics = fixture.CreateMany<Metric>(5).ToList();
            //var strValue = typeof(DebuggerUtility.Visualizers.Xml.DebuggerSideXmlVisualizer).AssemblyQualifiedName;
            //DebuggerSideJsonVisualizer.TestShowVisualizer(metrics);
        }

        //public static void TestShowVisualizer(object objectToVisualize)
        //{
        //    VisualizerDevelopmentHost myHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(DebuggerSideJsonVisualizer));
        //    myHost.ShowVisualizer();
        //}

        [TestMethod]
        public void MetricListTest()
        {
            var name = typeof(List<>).AssemblyQualifiedName;
            var fixture = new Fixture();
            var metrics = fixture.CreateMany<Metric>(5).ToList();

            var cont = new MetricsContainer {Metrics = metrics};
        }

        [TestMethod]
        public void MetricDictionaryTest()
        {
            var fixture = new Fixture();
            var dict = new Dictionary<int, Metric>
            {
                {1, fixture.Create<Metric>()},
                {2, fixture.Create<Metric>()},
                {3, fixture.Create<Metric>()},
                {4, fixture.Create<Metric>()}
            };
        }
    }

    [DebuggerVisualizer(
        @"DebuggerUtility.Visualizers.Json.DebuggerSideJsonVisualizer, "
        + @"DebuggerUtility.Visualizers, "
        + @"Version=1.0.0.0, Culture=neutral, "
        + @"PublicKeyToken=e8c91feafdcfb6e2",
        @"DebuggerUtility.Visualizers.Json.JsonVisualizerObjectSource, "
        + @"DebuggerUtility.Visualizers, "
        + @"Version=1.0.0.0, Culture=neutral, "
        + @"PublicKeyToken=e8c91feafdcfb6e2",
        Description = "Json Visualizer ")]
    public class MetricsContainer
    {
        public List<Metric> Metrics { get; set; }
    }
}