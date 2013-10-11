namespace LoadTest {
    public class DelayUnits {
        private readonly ExecutionPlan executionPlan;

        public DelayUnits(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }

        public ExecutionPlan Compile() {
            return executionPlan;
        }
    }
}