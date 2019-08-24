using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ChittiMusic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SongDetails : Page
    {
        private Song song;
        public SongDetails()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var song = (Song)e.Parameter;
            this.song = song;
            title.DataContext = song.title;
            album.DataContext = song.album;
            artist.DataContext = song.artist;
            CoverImage.DataContext = song.coverImage;
        }

        protected async void save_title(object sender, TextChangedEventArgs args)
        {
            var titleText = title.Text;
            StorageFolder musicFolder = KnownFolders.MusicLibrary;
            StorageFile musicFile = await musicFolder.GetFileAsync(song.fileName);
            var musicProperties = await musicFile.Properties.GetMusicPropertiesAsync();
            musicProperties.Title = titleText;
            musicFile.Properties.SavePropertiesAsync();

        }

        protected async void save_album(object sender, TextChangedEventArgs args)
        {
            var albumText = album.Text;
            StorageFolder musicFolder = KnownFolders.MusicLibrary;
            StorageFile musicFile = await musicFolder.GetFileAsync(song.fileName);
            var musicProperties = await musicFile.Properties.GetMusicPropertiesAsync();
            musicProperties.Album = albumText;
            musicFile.Properties.SavePropertiesAsync();

        }
        protected async void save_artist(object sender, TextChangedEventArgs args)
        {
            var artistText = artist.Text;
            StorageFolder musicFolder = KnownFolders.MusicLibrary;
            StorageFile musicFile = await musicFolder.GetFileAsync(song.fileName);
            var musicProperties = await musicFile.Properties.GetMusicPropertiesAsync();
            musicProperties.Artist = artistText;
            musicFile.Properties.SavePropertiesAsync();
        }
    }
}
