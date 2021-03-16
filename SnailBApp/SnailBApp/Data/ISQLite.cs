namespace SnailBApp.Data
{
    public interface ISQLite
    {
        SQLite.SQLiteAsyncConnection GetAsyncConnection();
    }
}
