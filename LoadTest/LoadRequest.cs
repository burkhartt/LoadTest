using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace LoadTest {
    public class LoadRequest {
        private readonly string url;
        public string Name { get; private set; }
        public TimeSpan TimeTaken { get; private set; }
        public bool IsFinished { get; private set; }

        public LoadRequest(string name, string url) {
            this.Name = name;
            this.url = url;
        }

        public void Run() {
            var t = new Thread(ThreadProc);
            t.Start();
        }

        private void ThreadProc() {
            var request = WebRequest.Create(url);
            var timer = new Stopwatch();
            timer.Start();
            request.GetResponse().Close();
            timer.Stop();
            TimeTaken = timer.Elapsed;
            IsFinished = true;
        }
    }
}