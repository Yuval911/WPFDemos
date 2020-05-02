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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDemos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            ButtonSlider buttonConverter = new ButtonSlider();
            buttonConverter.ShowDialog();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Arithmetic arithmetic = new Arithmetic();
            arithmetic.ShowDialog();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            WebLoader webLoader = new WebLoader();
            webLoader.ShowDialog();
        }
    }
}
