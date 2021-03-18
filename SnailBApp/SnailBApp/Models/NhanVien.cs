using SQLite;
namespace SnailBApp.Models
{
    [Table("NhanVien")]
    public class NhanVien
    {
        [PrimaryKey, NotNull, MaxLength(5)]
        public int ID { get; set; }
        [NotNull, MaxLength(150)]
        public string Name { get; set; }
        public string GioiTinh { get; set; }
        public string Date { get; set; }
        [MaxLength(200)]
        public string Adrress { get; set; }
        [MaxLength(200)]
        public string Desc { get; set; }
        [MaxLength(200)]
        public string IMG { get; set; }
        public string PhoneNumber { get; set; }
    }
}
