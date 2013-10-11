using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;

namespace LoadTest {
    public class ExecutionPlan {
        private int delay;
        private int delayMultiplier;
        private int numberOfRequests;
        private string url;

        public void SetNumberOfRequests(int requests) {
            numberOfRequests = requests;
        }

        public void SetUrl(string url) {
            this.url = url;
        }

        public void SetDelay(int delay) {
            this.delay = delay;
        }

        public void SetDelayMultiplier(int multiplier) {
            delayMultiplier = multiplier;
        }

        public ExecutionResults Execute() {
            var requests = new List<LoadRequest>(numberOfRequests);
            for (var i = 0; i < numberOfRequests; ++i) {
                var loadRequest = new LoadRequest("Thread-" + i, url);
                loadRequest.Run();
                requests.Add(loadRequest);
                Thread.Sleep(delay*delayMultiplier);
            }

            while (requests.Any(r => !r.IsFinished)) {
                
            }

            return new ExecutionResults {
                NumberOfRequests = numberOfRequests,
                Url = url,
                Delay = delay*delayMultiplier,
                ResponseTimes = requests.Select(r => new ResponseTime {
                    Name = r.Name,
                    Time = r.TimeTaken
                }),
                AverageResponseTime = requests.Average(x => x.TimeTaken.Milliseconds)
            };
        }
    }
}