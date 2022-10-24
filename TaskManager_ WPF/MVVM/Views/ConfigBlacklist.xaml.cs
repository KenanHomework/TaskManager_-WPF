using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManager__WPF.MVVM.ViewModels;
using TaskManager__WPF.Services;

namespace TaskManager__WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for ConfigBlacklist.xaml
    /// </summary>
    public partial class ConfigBlacklist : Window
    {
        public ConfigBlacklist()
        {
            InitializeComponent();
            BlacklistListView.ItemsSource = App.BlackList;
            App.Container.GetInstance<ConfigBlacklistVM>().Window = this;
            DataContext = App.Container.GetInstance<ConfigBlacklistVM>();
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Content.ToString())
                {
                    case "_":
                        this.WindowState = WindowState.Minimized;
                        break;
                    case "X":
                        JSONService.Write("BlackList.json", App.BlackList);
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

    }
}
