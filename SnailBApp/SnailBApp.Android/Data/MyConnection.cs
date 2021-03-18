using SnailBApp.Data;
using SnailBApp.Droid.Data;
using SQLite;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(MyConnection))]
namespace SnailBApp.Droid.Data
{
    class MyConnection : ISQLite
    {
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            //var documentsPath = "QLNhaHang_DB.db3";
            //var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), documentsPath);
            //var conn = new SQLiteAsyncConnection(path);
            //return conn;
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "SQLiteSnailB.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}