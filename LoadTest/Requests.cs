namespace LoadTest {
    public class Requests {
        private readonly ExecutionPlan executionPlan;

        public Requests(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }

        public To To(string url) {
            executionPlan.SetUrl(url);
            return new To(executionPlan);
        }
    }
}