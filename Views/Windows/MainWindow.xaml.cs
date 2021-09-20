
using System.Windows;

using System.Windows.Input;

using Praktika.Views.Windows;

namespace Praktika
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DragPanelMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
