namespace Chat.WebAPI.Services.Interfaces
{
    public interface IPubSubService<TIdentifier>
    {
        // Подписаться на издателя
        void Subscribe(TIdentifier subscriber, TIdentifier publisher);

        // Подписаться на издателей
        void Subscribe(string subcriber, IEnumerable<string> publishers);

        // Отписаться от издателя
        void Unsubscribe(TIdentifier subscriber, TIdentifier publisher);

        // Отписаться от всех издателей
        void RemoveAllSubscribes(TIdentifier subscriber);

        // Получить список подписчиков издателя
        IEnumerable<TIdentifier> GetSubscribers(TIdentifier publisher);

        // Получить список подписок подписчика
        IEnumerable<TIdentifier> GetSubscribes(TIdentifier subscriber);

        // Подписан ли пользователь на издателя
        bool IsSubscribed(string subscriber, string publisher);
    }
}
