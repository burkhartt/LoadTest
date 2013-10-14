namespace LoadTest {
    public class TestPlanBuilder {
        private readonly ExecutionPlan executionPlan;
        
        public TestPlanBuilder() {
            executionPlan = new ExecutionPlan();
        }

        public TestPlanBuilder(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }

        public Send Send(int numberOfRequests) {            
            executionPlan.SetNumberOfRequests(numberOfRequests);
            return new Send(executionPlan);
        }
    }
}