using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagniseTaskDAC.Entity
{
    public class Crypto
    {
        [Key]
        [Column(TypeName = "nvarchar(100)")]
        public string Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "numeric(18,5)")]
        public double? Price_usd { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Updated { get; set; }
    }
}