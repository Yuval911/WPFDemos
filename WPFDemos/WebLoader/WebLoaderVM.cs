using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFDemos
{
    /// <summary>
    /// This is the View Model class of the Arithmetic window.
    /// </summary>

    public class WebLoaderVM : INotifyPropertyChanged
    {
        /// <summary>
        /// This section of the project demonstrates the implementation of asynchronous behavior in two different ways.
        /// One way is with async function, and the second is with using a Task and the UI Dispatcher.
        /// The user inserts a URL address and then choose how to fire the web request - using a ***********************************************
        /// </summary>

        // The URL address of the web request.
        public string URL { get; set; }

        /// <summary>
        /// Each property here is tied to the "PropertyChanged" event. 
        /// WPF Uses that event to know when the UI Elements needs to be updated.
        /// </summary>

        // When the app is not loading the data (idle state) this boolean is true.
        private bool isIdle;  
        public bool IsIdle
        {
            get
            {
                return isIdle;
            }
            set
            {
                isIdle = value;
                OnPropertyChanged("IsIdle");
            }
        }

        // The size of the loaded page.
        private string size;
        public string Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }

        // Shows loading message when request is being processed.
        private string loading;
        public string Loading
        {
            get
            {
                return loading;
            }
            set
            {
                loading = value;
                OnPropertyChanged("Loading");
            }
        }

        // These commands made for the load buttons in the window.
        public DelegateCommand LoadPageSizeAsync { get; set; }
        public DelegateCommand LoadPageSizeDispatcher { get; set; }

        // This event is triggered when one the properties above is changed.
        public event PropertyChangedEventHandler PropertyChanged;

        public WebLoaderVM()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            LoadPageSizeAsync = new DelegateCommand(GetWebPageSizeAsync, CanExecute);
            LoadPageSizeDispatcher = new DelegateCommand(GetWebPageSizeDispatcer, CanExecute);
            URL = "https://";
            Loading = "";
            IsIdle = true;
        }

        // This handles the "PropertyChanged" event triggering
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // This method makes a web request to load a page and determine its size. Its an async function.
        private async void GetWebPageSizeAsync()
        {
            IsIdle = false;
            Loading = "Loading...";
            try
            {
                WebRequest webRequest = WebRequest.Create(URL);
                WebResponse response = await webRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string pageText = await reader.ReadToEndAsync();
                    Size = pageText.Length.ToString();
                }
            }
            catch(UriFormatException e)
            {
                MessageBox.Show("This is not a valid URL address");
            }
            finally
            {
                IsIdle = true;
                Loading = "";
            }
        }

        // This method does the same as the above, but using a Task and UI Dispatcher.
        private void GetWebPageSizeDispatcer()
        {
            IsIdle = false;
            Loading = "Loading...";


            Task.Run(() =>
            {
                try
                {
                    WebRequest webRequest = WebRequest.Create(URL);
                    WebResponse response = webRequest.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string pageText = reader.ReadToEnd();
                        Application.Current.Dispatcher.Invoke(() => { Size = pageText.Length.ToString(); });
                    }
                }
                catch (UriFormatException e)
                {
                    Application.Current.Dispatcher.Invoke(() => MessageBox.Show("This is not a valid URL address"));
                }
                finally
                {
                    IsIdle = true;
                    Loading = "";
                }

            });
        }

        // When the window is processing the request, the user cannot click the load buttons.
        private bool CanExecute()
        {
            if (IsIdle && !String.IsNullOrEmpty(URL))
                return true;
            else
                return false;
        }       
    }
}
