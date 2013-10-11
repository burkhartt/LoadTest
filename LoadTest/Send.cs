namespace LoadTest {
    public class Send {
        private readonly ExecutionPlan executionPlan;

        public Send(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }

        public Requests Requests {
            get { return new Requests(executionPlan); }
        }
    }
}