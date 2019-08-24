using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace ChittiMusic
{
    class Song
    {

        public string title { get; set; }
        public string album { get; set; }
        public string artist { get; set; }
        public string durationOfSong { get; set; }

        // public string displayName { get; set; }

        public async static Task<ICollection<Song>> GetAllSongsAsync()
        {
            var songs = new List<Song>();
            var header = new Song
            {
                title = "Title",
                album = "Album",
                artist = "Artist",
                durationOfSong = "Duration"
            };
            songs.Add(header);

          IReadOnlyCollection<StorageFile>   files = await FileHelper.GetMusicFileAsync();

           
            foreach (StorageFile file in files)
            {
                  MusicProperties musicProperties = await file.Properties.GetMusicPropertiesAsync();
                  string songTitle = musicProperties.Title;
                  string Album = musicProperties.Album;
                  string Artist = musicProperties.Artist;
                if(string.IsNullOrEmpty(songTitle))
                {
                    songTitle = file.DisplayName;
                }
                if(string.IsNullOrEmpty(Album))
                {
                    Album = "Unknown Album";
                }
                if(string.IsNullOrEmpty(Artist))
                {
                    Artist = "Unknown Artist";
                }

                var song = new Song
                {
                    title = songTitle,
                    album = Album,
                    artist = Artist,
                    durationOfSong = musicProperties.Duration.ToString()
                    
                };
                songs.Add(song);
            }

            return songs;
        }




    }
}

