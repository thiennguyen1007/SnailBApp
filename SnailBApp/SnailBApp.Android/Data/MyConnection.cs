using SQLite;
using System.IO;

namespace SnailBApp.Droid.Data
{
    class MyConnection
    {
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var documentsPath = "QLNhaHang_DB.db3";
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), documentsPath);
            var conn = new SQLiteAsyncConnection(path);
            return conn;
        }
    }
}