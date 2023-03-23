using System;

namespace Chat.Client.WPF.Services.Interfaces
{
    internal interface IObservableStore<TKey, TModel>
    {
        public event Action<TKey, TModel> OnNewItemAdded;
        public event Action<TKey, TModel> OnItemUpdated;
        public event Action<TKey, TModel> OnItemRemoved;
    }
}
