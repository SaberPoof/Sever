using Microsoft.Extensions.DependencyInjection;
using ReportGenerator.Interfaces;
using ReportGenerator.Services;
using ReportGenerator.ViewModels;
using System;
using System.Threading;
using System.Windows;

namespace ReportGenerator
{
    public partial class App : Application
    {
        private static Mutex _mutex = null;

        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            const string appName = "ReportGeneratorApp";
            bool createdNew;

            // Мьютекс, для запуск только одного экземпляра 
            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Приложение уже запущено.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                Shutdown();
                return;
            }

            // Продолжаем запуск, если еще не запущено приложение 
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при запуске приложения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IFileDialogService, FileDialogService>();
            services.AddSingleton<IExcelDataService, ExcelDataService>();
            services.AddSingleton<IReportGeneratorService, WordGeneratorService>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }
    }
}
