namespace Chat.Core.DTOs.Notifications
{
    public class MessageNotificationDTO
    {
        // Username отправителя
        public string Sender { get; set; }

        // Username получателя
        public string Receiver { get; set; }

        // Статический ключ - у всех клиентов одинаковый,
        // Шифруется перед отправкой
        // Нужен для определения - возможно ли расшифровать сообщение
        public string StaticKey { get; set; }

        // Сообщение
        // Шифруется перед отправкой
        public string Message { get; set; }
    }
}
