using SnailBApp.Data;
using SnailBApp.Droid.Data;
using SnailBApp.Models;
using SQLite;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(MyConnection))]
namespace SnailBApp.Droid.Data
{
    class MyConnection : ISQLite
    {
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "SQLiteSnailB.db3");
            var db = new SQLiteAsyncConnection(path);
            db.CreateTableAsync<Food>();
            db.CreateTableAsync<NhanVien>();
            db.CreateTableAsync<HoaDon>();
            db.CreateTableAsync<Bill>();
            return db;
        }
    }
}