using System;
using System.ComponentModel.DataAnnotations;

namespace Chat.Client.WPF.Entities
{
    public class MessageEntity
    {
        [Key]
        public Guid Id { get; private set; }
        public string ReceiverUsername { get; private set; }
        public string SenderUsername { get; private set; }
        public string StaticKey { get; private set; }
        public string Content { get; private set; }

        public MessageEntity(string senderUsername, string receiverUsername, string staticKey, string content)
        {
            Id = new Guid();
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
            Content = content;
            StaticKey = staticKey;
        }
    }
}