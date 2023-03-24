using Chat.Client.WPF.Entities;
using Chat.Client.WPF.Services;
using Chat.Client.WPF.Services.Implementation;
using Chat.Client.WPF.Services.Implementation.Navigation;
using Chat.Client.WPF.Services.Interfaces;
using Chat.Client.WPF.ViewModels.Pages;
using Chat.Client.WPF.Views.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Client.WPF.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить сервис навигации между страницами
        /// </summary>
        /// <param name="services">Сервисы</param>
        /// <returns>Сервисы</returns>
        public static ServiceCollection AddNavigationService(this ServiceCollection services)
        {
            var navigationService = new NavigationService();
            {
                // Регистрация страницы Chats
                navigationService.Register<ChatsPage>
                (
                    view: new ChatsPage(),
                    viewModel: new ChatPageViewModel()
                );

                // Регистрация страницы Settings
                navigationService.Register<SettingsPage>
                (
                    view: new SettingsPage(),
                    viewModel: new SettingsPageViewModel()
                );
            }

            // Добавление сервиса для навигации
            services.AddSingleton<NavigationService>(navigationService);

            return services;
        }

        /// <summary>
        /// Добавить сервисы для хранения
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ServiceCollection AddStores(this ServiceCollection services)
        {
            services.AddSingleton<IStore<string, UserEntity>, UserStore>();
            services.AddSingleton<IStore<string, ChatEntity>, ChatStore>();

            return services;
        }
    }
}
