using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySQLStoreAPI.Models
{
    // https://docs.microsoft.com/en-us/ef/core/modeling/entity-types?tabs=data-annotations
    // [Table("Category")]
    [Table("Category")] // การกำหนดชื่อ table และ schema
    [Comment("Category table data")] // ใส่คำอธิบายให้ตาราง
    public class Category
    {
        // public int CategoryId {get; set;} // หากชื่อ column ลงท้ายด้วย Id จะถือว่าเป็น Primaray Key

        // [Column("CategoryId", Order = 0)] // กำหนดชื่อ column และ ลำดับที่ของ column เอง
        // public int Id {get; set;} // หากชื่อ column ลงท้ายด้วย Id จะถือว่าเป็น PrimarayKey

        [Key] // กำหนดให้ file ที่ต้องการเป็น Primary Key
        public int CategoryId { get; set; }

        [Column("CategoryName", TypeName = "varchar(64)", Order = 1)] // กำหนด datatype ให้กับ field
        [Required] // Not null
        public string CategoryName { get; set; }

        [Column(Order = 2)]
        [Required] // Not null
        public int CategoryStatus { get; set; }

    }
}