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
    /// This section of the project is made in MVVW. It means that there are no logics in this class.
    /// All the logics of this window are inside the View Model class.
    /// </summary>
    public partial class WebLoader : Window
    {
        public WebLoader()
        {
            InitializeComponent();

            WebLoaderVM vm = new WebLoaderVM();

            // Binding the View Model to the data context of the window.
            DataContext = vm;
        }
    }
}
