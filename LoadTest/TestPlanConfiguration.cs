using System.Collections.Generic;

namespace LoadTest {
    public class TestPlanConfiguration {
        private readonly ExecutionPlan executionPlan;

        public TestPlanConfiguration(ExecutionPlan executionPlan) {
            this.executionPlan = executionPlan;
        }

        public TestPlanConfiguration WithHeader(string key, string value) {
            executionPlan.AddHeader(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public Delay WithADelayOf(int delay) {
            executionPlan.SetDelay(delay);
            return new Delay(executionPlan);
        }

        public ExecutionPlan Build() {
            return executionPlan;
        }

        public TestPlanConfiguration WithRequestParam(string key, string value) {
            executionPlan.AddRequestParm(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public TestPlanConfiguration WithMethod(HttpVerbs httpVerb) {
            executionPlan.SetHttpVerb(httpVerb);
            return this;
        }
    }
}