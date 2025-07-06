using System.Windows;
using System.Windows.Controls;

namespace BrainWaves.View
{
    public partial class Presets : Page
    {
        public Presets()
        {
            InitializeComponent();
        }

        private void StopPropagation(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
