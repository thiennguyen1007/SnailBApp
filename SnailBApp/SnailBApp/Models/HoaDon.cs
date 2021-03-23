using SQLite;
namespace SnailBApp.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [PrimaryKey, NotNull]
        public int ID { get; set; }
        [NotNull, MaxLength(250)]
        public string Email { get; set; }
        [NotNull, MaxLength(20)]
        public string PhoneNumber { get; set; }
        [NotNull]
        public float Price { get; set; }
        [NotNull]
        public string Foods { get; set; }
    }
}
