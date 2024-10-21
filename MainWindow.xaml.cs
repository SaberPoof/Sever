using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReportGenerator.ViewModels;
using System.Windows;

namespace ReportGenerator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ServiceProvider.GetService<MainViewModel>();
        }
    }
}
