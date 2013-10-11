namespace LoadTest {
    public class LoadTestConfiguration {
        public Send Send(int numberOfRequests) {
            var executionPlan = new ExecutionPlan();
            executionPlan.SetNumberOfRequests(numberOfRequests);
            return new Send(executionPlan);
        }
    }
}