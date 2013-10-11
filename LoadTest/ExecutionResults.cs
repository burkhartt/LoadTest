using System.Collections.Generic;

namespace LoadTest {
    public class ExecutionResults {
        public IEnumerable<ResponseTime> ResponseTimes { get; set; }
        public int Delay { get; set; }
        public string Url { get; set; }
        public int NumberOfRequests { get; set; }
        public double AverageResponseTime { get; set; }
    }
}