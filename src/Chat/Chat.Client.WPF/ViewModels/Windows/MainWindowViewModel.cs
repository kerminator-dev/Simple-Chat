using Chat.Client.WPF.Commands;
using Chat.Client.WPF.Services.Implementation.Navigation;
using Chat.Client.WPF.Views.Pages;
using System.Windows.Controls;
using System.Windows.Input;

namespace Chat.Client.WPF.ViewModels.Windows
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;
        private readonly ICommand _navigateToChats;
        private readonly ICommand _navigateToSettings;

        /// <summary>
        /// Основная текущая выбранная страница
        /// </summary>
        public ContentControl? MainPage
        {
            get => _navigationService.MainPage;
        }

        /// <summary>
        /// Перейти к странице Чатов
        /// </summary>
        public ICommand NavigateToChats => _navigateToChats;

        /// <summary>
        /// Перейти к странице Настроек
        /// </summary>
        public ICommand NavigateToSettings => _navigateToSettings;

        public MainWindowViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;

            _navigateToSettings = new RelayCommand
            (
                (parameter) =>
                {
                    _navigationService.Navigate<SettingsPage>();
                    OnPropertyChanged(nameof(MainPage));
                }
            );

            _navigateToChats = new RelayCommand
            (
                (parameter) =>
                {
                    _navigationService.Navigate<ChatsPage>();
                    OnPropertyChanged(nameof(MainPage));
                }
            );
        }
    }
}
