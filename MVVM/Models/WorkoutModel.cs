using SQLite;

namespace MauiApp1.MVVM.Models
{
    public class WorkoutModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }
    }
}