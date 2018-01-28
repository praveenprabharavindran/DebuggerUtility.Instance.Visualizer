using System;
using System.Diagnostics;

namespace VisualizerTests
{
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
    //[Serializable]
    public class Metric

    {
        public int CustomerId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Timestamp { get; set; }

        public long? ContentLength { get; set; }
        public double ElapsedSeconds { get; set; }
        public int? RecordsFetched { get; set; }
    }
}