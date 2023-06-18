using SQLite;
using System.IO;
//SQLite code from official MAUI documentation https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite#install-the-sqlite-nuget-package
namespace MealPlan.DataAccess
{

    public static class SQLdb
    {
        public const string DatabaseFilename = "MealPlan.db";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }
}

