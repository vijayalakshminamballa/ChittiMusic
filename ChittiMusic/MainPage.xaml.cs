using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ChittiMusic
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

        //protected async override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);
        //    await Song.GetAllSongsAsync();
        //}

        public string Test { get; set; }

        private void nvTopLevelNav_Loaded(object sender, RoutedEventArgs args)
        {
            contentFrame.Navigate(typeof(AddMusic));
        }
        private async void AddSong()
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            filePicker.FileTypeFilter.Add(".mp3");
            filePicker.FileTypeFilter.Add(".mp4");
            IReadOnlyList<StorageFile> files = await filePicker.PickMultipleFilesAsync();
            foreach (var file in files)
            {
                FileHelper.CopyMusicFileAsync(file);
            }

            contentFrame.Navigate(typeof(AddMusic));
        }
        private void DeleteSongs()
        {


            FileHelper.DeleteMusicFile();

        }


        private void nvTopLevelNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            contentFrame.Navigate(typeof(AddMusic));
        }


        private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string itemTag = args.InvokedItem.ToString();

            if (itemTag != null)
            {
                switch (itemTag)
                {
                    case "Add Music":
                        AddSong();
                        contentFrame.Navigate(typeof(AddMusic));
                        break;
                    case "DeleteAllSongs":
                        DeleteSongs();
                        contentFrame.Navigate(typeof(AddMusic));
                        break;

                    default:
                       contentFrame.Navigate(typeof(AddMusic));
                        break;
                }
            }

        }

    }
}

