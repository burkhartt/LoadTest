namespace LoadTest {
    public class Delay {
        private readonly ExecutionPlan executionPlan;

        public Delay(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }

        public TestPlanConfiguration Milliseconds { get { return MultipliedBy(1); } }
        public TestPlanConfiguration Seconds { get { return MultipliedBy(1000); } }
        public TestPlanConfiguration Minutes { get { return MultipliedBy(60000); } }

        private TestPlanConfiguration MultipliedBy(int multiplier) {
            executionPlan.SetDelayMultiplier(multiplier);
            return new TestPlanConfiguration(executionPlan);
        }
    }
}