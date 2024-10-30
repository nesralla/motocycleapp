namespace Motocycle.Infra.CrossCutting.Commons.Providers
{
    public class QueuesProvider
    {

        public QueuesConsumer Consumers { get; set; }
        public QueuesProducer Producers { get; set; }
        public TopicProducer TopicProducers { get; set; }


        public class QueuesConsumer
        {
            public string MotocycleProcess { get; set; }
            public int RetryLimit { get; set; }
        }

        public class QueuesProducer
        {
            public string CreateMotocycleProcess { get; set; }
            public string ClientNotificationSender { get; set; }
        }
        public class TopicProducer
        {
            public string MotocycleEvent { get; set; }
            public string RentEvent { get; set; }
            public string DeliverymanEvent { get; set; }
        }
    }
}
