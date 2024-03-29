﻿using System.Windows;
using Chat.Client.WPF.Extensions;
using Chat.Client.WPF.ViewModels.Windows;
using Chat.Client.WPF.Views;
using Chat.Client.WPF.Views.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Client.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        protected void ConfigureServices(ServiceCollection services)
        {
            services.AddNavigationService();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();
          
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = serviceProvider.GetService<MainWindow>();
            MainWindow.DataContext = serviceProvider.GetService<MainWindowViewModel>();
            MainWindow.Show();

            base.OnStartup(e);
        }


    }
}
