namespace Chat.WebAPI.Models
{
    internal class Subscription
    {
        public string Subscriber { get; private set; }
        public string SubscribedOn { get; private set; }

        public Subscription(string subscriber, string subscribedOn)
        {
            Subscriber = subscriber;
            SubscribedOn = subscribedOn;
        }
    }
}
