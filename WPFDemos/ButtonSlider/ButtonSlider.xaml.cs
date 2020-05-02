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

namespace WPFDemos
{
    /// <summary>
    /// Interaction logic for ButtonSlider.xaml
    /// </summary>
    public partial class ButtonSlider : Window
    {
        public ButtonSlider()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// When the button is clicked it opens a new window and sends it some information.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PopUpWindow popUpWindow = new PopUpWindow(txtBox.Text, (int)sliderW.Value, (int)sliderH.Value);
            popUpWindow.ShowDialog();
        }
    }
}
