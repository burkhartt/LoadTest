using System.Collections.Generic;
using LoadTest;

namespace LoadTests.Console {
    internal class Program {
        private static void Main(string[] args) {
            var loadTestConfiguration = new TestPlanBuilder();
            var testPlan = loadTestConfiguration
                .Send(10).Requests.To("http://www.goaheadtours.com/")
                .WithHttpVerb(HttpVerbs.Post)
                .WithHeader("some-header", "headerValue")
                .WithRequestParam("request-param", "paramValue")
                .WithADelayOf(100).Milliseconds
                .Build();
            var results = testPlan.Execute();
        }
    }
}