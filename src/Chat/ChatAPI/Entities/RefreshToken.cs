namespace ChatAPI.Entities
{
    public class RefreshToken
    {
        public string OwnerUsername { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationDateTime { get; set; }
    }
}
