namespace LoadTest {
    public class DelayOf {
        private readonly ExecutionPlan executionPlan;

        public DelayOf(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }

        public TestPlanConfiguration Milliseconds {
            get {
                executionPlan.SetDelayMultiplier(1);
                return new TestPlanConfiguration(executionPlan);
            }
        }
    }
}