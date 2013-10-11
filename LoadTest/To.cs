namespace LoadTest {
    public class To {
        private readonly ExecutionPlan executionPlan;

        public To(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }        

        public DelayOf WithADelayOf(int delay) {
            executionPlan.SetDelay(delay);
            return new DelayOf(executionPlan);
        }
    }
}