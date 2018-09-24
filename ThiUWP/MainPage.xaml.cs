using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ThiUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        

        public MainPage()
        {
            this.InitializeComponent();
        }
        private String dialogMessage;
        private async void btn_Search(object sender, RoutedEventArgs e)
        {

            String Name = FileName.Text;
            String Content = ContentToSearch.Text;
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile file = await folder.GetFileAsync(Name);
                if (file == null)
                {
                    dialogMessage = "File not found";
                }
                else
                {
                    String text = await FileIO.ReadTextAsync(file);
                    if (text.Contains(Content))
                    {
                        dialogMessage = "File found and text found";
                    }
                    else
                    {
                        dialogMessage = "File found but text not found";
                    }
                }
            }
            catch (Exception exception)
            {
                dialogMessage = "File not found";
                Debug.WriteLine(exception.Message);
            }



            DisplayNoWifiDialog();
        }

        private async void DisplayNoWifiDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "",
                Content = dialogMessage,
                CloseButtonText = "Close"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }

   
    }
}