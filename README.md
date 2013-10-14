LoadTest
========
Usage:
```
var testPlanBuilder = new TestPlanBuilder();
var plan = testPlanBuilder.Send(10).Requests
    .WithMethod(RequestMethod.Get)
    .WithHeader("some-header", "some-value")
    .WithHeader("another-header", "another-value")
    .WithRequestParam("some-param", "some-value")
    .WithRequestParam("another-param", "another-value")
    .To("http://www.google.com/")
    .WithADelayOf(100).Milliseconds
    .Build();
var results = plan.Execute();
```

Download on NuGet:
```
Install-Package LoadTest
```
