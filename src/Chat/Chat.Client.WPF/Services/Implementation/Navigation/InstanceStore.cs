using System;
using System.Collections.Generic;

namespace Chat.Client.WPF.Services.Implementation.Navigation
{
    internal abstract class InstanceStore<TModel> where TModel : class
    {
        // Для каждого конкретного типа есть соответствующий объект
        private readonly Dictionary<Type, TModel> _items = new Dictionary<Type, TModel>();

        public virtual bool Register<TModelType>(TModel model)
        {
            return _items.TryAdd(typeof(TModelType), model);
        }

        public virtual bool TryGet<TModelType>(out TModel? model)
        {
            return _items.TryGetValue(typeof(TModelType), out model);
        }
    }
}
