using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string firstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string lastName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string username { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string password { get; set; }
    }
}
