using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;

namespace LoadTest {
    public class ExecutionPlan {
        private int delay;
        private int delayMultiplier;
        private int numberOfRequests;
        private string baseUrl;
        private readonly NameValueCollection headers = new NameValueCollection();
        private readonly NameValueCollection requestParams = new NameValueCollection();
        private RequestMethod requestMethod;

        public void SetNumberOfRequests(int requests) {
            numberOfRequests = requests;
        }

        public void SetUrl(string url) {
            this.baseUrl = url;
        }

        public void SetDelay(int delay) {
            this.delay = delay;
        }

        public void SetDelayMultiplier(int multiplier) {
            delayMultiplier = multiplier;
        }

        public void AddHeader(KeyValuePair<string, string> header) {
            headers.Add(header.Key, header.Value);
        }

        public void AddRequestParm(KeyValuePair<string, string> requestParam) {
            requestParams.Add(requestParam.Key, requestParam.Value);
        }

        public void SetMethod(RequestMethod method) {
            requestMethod = method;
        }

        public ExecutionResults Execute() {
            var requests = new List<LoadRequest>(numberOfRequests);
            for (var i = 0; i < numberOfRequests; ++i) {
                var loadRequest = new LoadRequest("Thread-" + i, baseUrl, requestMethod, headers, requestParams);
                loadRequest.Run();
                requests.Add(loadRequest);
                Thread.Sleep(delay*delayMultiplier);
            }

            while (requests.Any(r => !r.IsFinished)) {
                
            }

            return new ExecutionResults {
                NumberOfRequests = numberOfRequests,
                Url = requests.First().Url,
                Delay = delay*delayMultiplier,
                ResponseTimes = requests.Select(r => new ResponseTime {
                    Name = r.Name,
                    Time = r.TimeTaken
                }),
                AverageResponseTime = TimeSpan.FromMilliseconds(requests.Average(x => x.TimeTaken.Milliseconds))
            };
        }
    }
}