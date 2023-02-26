namespace ChatAPI.Models
{
    public class RateLimitConfiguration
    {
        public int PermitLimit { get; set;}
        public int QueueLimit { get; set;}
        public int WindowSeconds { get; set;}
    }
}
