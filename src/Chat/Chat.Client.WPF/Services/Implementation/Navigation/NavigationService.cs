using Chat.Client.WPF.ViewModels;
using System;
using System.Windows.Controls;

namespace Chat.Client.WPF.Services.Implementation.Navigation
{
    /// <summary>
    /// Сервис для реализации простой навигации по страницам
    /// 
    /// На главном окне приложения должен быть основной компонент для представления странц,
    /// Который должен ссылаться на NavigationService.MainPage
    /// 
    /// Жизненный цикл зависимостей (для View и ViewModel) реализовывать не стал (может потом)
    /// 
    /// Может показаться, что я собираю велосипед. НУ ДА И ЧТО ВЫ МНЕ СДЕЛАЕТЕ? НУЖНО ЖЕ РЕАЛИЗОВАТЬ СВОЁ И РАЗОБРАТЬСЯ ВО ВСЁМ
    /// </summary>
    internal class NavigationService
    {
        // Хранилище представлений
        private readonly InstanceStore<ContentControl> _viewStore;
        // Хранилище ViewModel'ей
        private readonly InstanceStore<ViewModelBase> _viewModelStore;

        /// <summary>
        /// Активная основная страница
        /// </summary>
        public ContentControl? MainPage { get; private set; }

        public NavigationService(ContentControl defaultPage) : this()
        {
            MainPage = defaultPage;
        }

        public NavigationService()
        {
            _viewStore      = new ViewStore<ContentControl>();
            _viewModelStore = new ViewModelStore<ViewModelBase>();
        }

        /// <summary>
        /// Зарегистрировать View и соответствующий ViewModel
        /// </summary>
        /// <typeparam name="TView">Конкретный тип View</typeparam>
        /// <param name="view">Объект View</param>
        /// <param name="viewModel">Объект ViewModel</param>
        /// <returns>Выполнено без ошибок</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Register<TView>(TView view, ViewModelBase viewModel) 
            where TView : ContentControl
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            return _viewStore.Register<TView>(view) && _viewModelStore.Register<TView>(viewModel);
        }

        /// <summary>
        /// Зарегистрировать View
        /// </summary>
        /// <typeparam name="TViewTViewType">Конкретный тип View</typeparam>
        /// <param name="view">Объект View</param>
        /// <returns>Выполнено без ошибок</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public bool Register<TView>(ContentControl view)
            where TView : ContentControl
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            return _viewStore.Register<TView>(view);
        }

        /// <summary>
        /// Выполнить навигацию
        /// </summary>
        /// <typeparam name="TView">Тип представления, к которому необходимо выполнить навигацию</typeparam>
        /// <exception cref="ArgumentException"><typeparamref name="TView"/> не зарегистрирован!</exception>
        public void Navigate<TView>() 
            where TView : ContentControl
        {
            if (!_viewStore.TryGet<TView>(out ContentControl? content))
            {
                throw new ArgumentException("There is no corresponding registered component in ViewStore", nameof(TView));
            }

            if (_viewModelStore.TryGet<TView>(out var viewModel))
            {
                content.DataContext = viewModel;
            };

            MainPage = content;
        }
    }
}
