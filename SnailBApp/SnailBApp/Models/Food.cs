using SQLite;
namespace SnailBApp.Models
{
    [Table("Food")]
    public class Food
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string IMG { get; set; }
        public float Price { get; set; }
        public int SL { get; set; }
    }
}
