using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApi.Models
{
    public class Grooming
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Appointment { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string PetName { get; set; }
        [Required]
        public int PetTypeId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string PetTypeName { get; set; }
        public int OwnerId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string OwnerName { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string ServiceName { get; set; }
        [Required]
        public int VeterinarId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string VeterinarName { get; set; }

    }
}
