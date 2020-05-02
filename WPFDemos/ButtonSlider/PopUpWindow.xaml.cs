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
    /// Interaction logic for PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {
        public string ButtonText { get; set; }
        public int ButtonWidth { get; set; }
        public int ButtonHeight { get; set; }

        public PopUpWindow(string text, int width, int height)
        {
            InitializeComponent();

            ButtonText = text;
            ButtonWidth = width;
            ButtonHeight = height;

            DataContext = this;
        }
    }
}
