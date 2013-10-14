namespace LoadTest {
    public class Requests {
        private readonly ExecutionPlan executionPlan;

        public Requests(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }

        public TestPlanConfiguration To(string url) {
            executionPlan.SetUrl(url);
            return new TestPlanConfiguration(executionPlan);
        }
    }
}