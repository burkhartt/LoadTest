LoadTest
========
Usage:
```
var testPlanBuilder = new TestPlanBuilder();
var plan = testPlanBuilder
    .Send(10).Requests
    .To("http://www.google.com/")
    .WithHttpVerb(HttpVerbs.Get)
    .WithHeader("some-header", "some-value")
    .WithHeader("another-header", "another-value")
    .WithRequestParam("some-param", "some-value")
    .WithRequestParam("another-param", "another-value")
    .WithADelayOf(100).Milliseconds
    .Build();
var results = plan.Execute();

var responseTimes = results.ResponseTimes; // IEnumerable<ResponseTime>
var averageResponseTime = results.AverageResponseTime;
```

Download on NuGet:
```
Install-Package LoadTest
```
