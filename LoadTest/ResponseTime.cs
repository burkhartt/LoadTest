using System;

namespace LoadTest {
    public class ResponseTime {
        public TimeSpan Time { get; set; }
        public long RequestContentLength { get; set; }
        public long ResponseContentLength { get; set; }
    }
}