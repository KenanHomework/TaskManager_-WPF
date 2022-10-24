using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TaskManager__WPF.MVVM.Models;
using TaskManager__WPF.MVVM.ViewModels;
using TaskManager__WPF.Services;

namespace TaskManager__WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for Processes.xaml
    /// </summary>
    public partial class Processes : Window
    {
        public Processes()
        {
            InitializeComponent();
            App.Container.GetInstance<ProcessesVM>().Window = this;
            DataContext = App.Container.GetInstance<ProcessesVM>();
            App.Container.GetInstance<ProcessesVM>().RefreshProcesses();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(RefreshTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            App.Container.GetInstance<ProcessesVM>().RefreshProcesses();
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
                        Application.Current.Shutdown();
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


        private void ProcessesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
