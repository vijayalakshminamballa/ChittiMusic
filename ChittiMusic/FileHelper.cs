using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace ChittiMusic
{
    class FileHelper
    {

        public async static void CopyMusicFileAsync(StorageFile file)
        {
            StorageFolder storageLocation = ApplicationData.Current.LocalFolder;
            StorageFolder musicFolder = await storageLocation.CreateFolderAsync("MyMusic", CreationCollisionOption.OpenIfExists);
            StorageFile copySong = await file.CopyAsync(musicFolder, file.Name, NameCollisionOption.ReplaceExisting);
            var songProperties = await file.Properties.GetMusicPropertiesAsync();
        }
        //Updating cover image
        //public async static Task<int> CopyAndReplaceAsync(StorageFile file)
        //{
        //    StorageFolder storageLocation = ApplicationData.Current.LocalFolder;
        //    StorageFolder imageFolder = await storageLocation.CreateFolderAsync("My CoverImage", CreationCollisionOption.OpenIfExists);
        //    StorageFile imageFile = await imageFolder.CreateFileAsync("CoverImage.jpg", CreationCollisionOption.OpenIfExists);
        //    await file.CopyAndReplaceAsync(imageFile);
        //    return 1;

        //}
        public async static Task<IReadOnlyCollection<StorageFile>> GetLibrarySongs()
        {
            QueryOptions queryOption = new QueryOptions(CommonFileQuery.OrderByTitle, new string[] { ".mp3", ".mp4" });

            queryOption.FolderDepth = FolderDepth.Deep;

            Queue<IStorageFolder> folders = new Queue<IStorageFolder>();

            var files = await KnownFolders.MusicLibrary.CreateFileQueryWithOptions(queryOption).GetFilesAsync();
            return files;
        }
            //Reading files
            public async static Task<IReadOnlyCollection<StorageFile>> GetPlaylistSongs()
        {
            
                StorageFolder storageLocation = ApplicationData.Current.LocalFolder;
                StorageFolder getmusicFolder = await storageLocation.CreateFolderAsync("MyMusic", CreationCollisionOption.OpenIfExists);
                var ListOfFiles = await getmusicFolder.GetFilesAsync();
                return ListOfFiles;
        }
        //Deleting all files
        public async static void DeleteMusicFile()
        {
            StorageFolder storageLocation = ApplicationData.Current.LocalFolder;
            StorageFolder getmusicFolder = await storageLocation.CreateFolderAsync("MyMusic", CreationCollisionOption.OpenIfExists);
            await getmusicFolder.DeleteAsync();
        }
        //Delete particular file
        public async static void DeleteSelectedMusic()
        {
            StorageFolder storageLocation = ApplicationData.Current.LocalFolder;
            StorageFolder getmusicFolder = await storageLocation.GetFolderAsync("MyMusic");
            //StorageFile getmusicfile = await getmusicFolder.GetFileAsync();
            // await getmusicfile.DeleteAsync();
        }
    }
}

