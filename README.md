LoadTest
========
Usage:
```
var loadTestConfiguration = new LoadTestConfiguration();
var plan = loadTestConfiguration.Send(10).Requests
    .To("http://www.google.com/")
    .WithADelayOf(100).Milliseconds
    .Compile();
var results = plan.Execute();
```
