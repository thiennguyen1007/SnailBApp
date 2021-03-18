using SQLite;
namespace SnailBApp.Models
{
    //6 thuoc tinh
    [Table("Food")]
    public class Food
    {
        [PrimaryKey, NotNull]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string IMG { get; set; }
        public float Price { get; set; }
        public int SL { get; set; }
    }
}
