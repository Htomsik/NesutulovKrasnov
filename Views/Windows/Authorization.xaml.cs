using System.Windows;
using System.Windows.Input;

namespace Praktika.Views.Windows
{
    /// <summary>
    ///     Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void DragPanel(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}