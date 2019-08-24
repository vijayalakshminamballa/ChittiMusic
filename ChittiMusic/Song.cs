using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

namespace ChittiMusic
{
    class Song
    {
        
        public string title { get; set; }
        public string album { get; set; }
        public string artist { get; set; }
        public string durationOfSong { get; set; }
        
        public BitmapImage coverImage { get; set; }

        // public string displayName { get; set; }

            public async static Task<ICollection<Song>> GetLibraryAsync ()
         {
            
           IReadOnlyCollection<StorageFile> files= await FileHelper.GetLibrarySongs();
            return await GetSongProperties(files);
         }

        public async static Task<ICollection<Song>> GetPlaylistAsync()
        {
            IReadOnlyCollection<StorageFile> files = await FileHelper.GetPlaylistSongs();
            return await GetSongProperties(files);
        }

        private async static Task<ICollection<Song>> GetSongProperties(IReadOnlyCollection<StorageFile> files)
        {
            var songs = new List<Song>();

            foreach (StorageFile file in files)
            {
                MusicProperties musicProperties = await file.Properties.GetMusicPropertiesAsync();
                string songTitle = musicProperties.Title;
                string Album = musicProperties.Album;
                string Artist = musicProperties.Artist;
                BitmapImage image = await GetThumbnail(file);

                if (string.IsNullOrEmpty(songTitle))
                {
                    songTitle = file.DisplayName;
                }
                if (string.IsNullOrEmpty(Album))
                {
                    Album = "Unknown Album";
                }
                if (string.IsNullOrEmpty(Artist))
                {
                    Artist = "Unknown Artist";
                }

                var song = new Song
                {
                    title = songTitle,
                    album = Album,
                    artist = Artist,
                    durationOfSong = musicProperties.Duration.ToString(),
                    coverImage = image

                };
                songs.Add(song);
            }

            return songs;
        }
    


        private async static Task<BitmapImage> GetThumbnail(StorageFile file)
        {
            if (file != null)
            {
                StorageItemThumbnail thumb = await file.GetScaledImageAsThumbnailAsync(ThumbnailMode.VideosView);
                if (thumb != null)
                {
                    BitmapImage img = new BitmapImage();
                    await img.SetSourceAsync(thumb);
                    return img;
                }
            }
            return null;
        }

    }

    
}

