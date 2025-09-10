using SQLite;

namespace MauiApp1.MVVM.Models
{
    public class UserModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public string Username { get; set; }

        public string Password { get; set; } 
    }
}