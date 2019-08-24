using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var lines = await FileHelper.GetMusicFileAsync();

            //if (lines == null || lines.Count == 0)
            //{
            //    return songs;
            //}
            foreach (var line in lines)
            {
                var musicProperties = await line.Properties.GetMusicPropertiesAsync();

                var song = new Song
                {
                    title = musicProperties.Title,
                    album = musicProperties.Album,
                    artist = musicProperties.Artist,
                    durationOfSong = musicProperties.Duration.ToString()
                };
                songs.Add(song);
            }

            return songs;
        }




    }
}

