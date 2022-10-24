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

namespace TaskManager__WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AskProcessPath.xaml
    /// </summary>
    public partial class AskProcessPath : Window
    {
        public AskProcessPath()
        {
            InitializeComponent();
        }

        public bool Result { get; set; } = false;

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

        private void Submite_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void ProcessPath_TextChanged(object sender, TextChangedEventArgs e) => Submite.IsEnabled = !string.IsNullOrWhiteSpace(ProcessPath.Text);

    }
}
