﻿namespace Motocycle.Infra.CrossCutting.Commons.Providers
{
    public class QueuesProvider
    {

        public QueuesConsumer Consumers { get; set; }
        public QueuesProducer Producers { get; set; }

        public class QueuesConsumer
        {
            public string RechargeProcess { get; set; }
            public int RetryLimit { get; set; }
        }

        public class QueuesProducer
        {
            public string RechargeProcess { get; set; }
            public string ClientNotificationSender { get; set; }
        }
    }
}
