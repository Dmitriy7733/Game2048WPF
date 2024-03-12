using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game2048WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            vm = (ViewModel2048)DataContext;
        }

        private readonly ViewModel2048 vm;

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: vm.NextStep(DirectionEnum.Up); break;
                case Key.Down: vm.NextStep(DirectionEnum.Down); break;
                case Key.Left: vm.NextStep(DirectionEnum.Left); break;
                case Key.Right: vm.NextStep(DirectionEnum.Right); break;
            }
        }
    }
}