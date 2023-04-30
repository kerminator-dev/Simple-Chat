using Chat.WebAPI.Entities;

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
        public override bool Equals(object? obj)
        {
            return obj is Subscription contact &&
                   Subscriber == contact.Subscriber &&
                   SubscribedOn == contact.SubscribedOn;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Subscriber, SubscribedOn);
        }
    }
}
