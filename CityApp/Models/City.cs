using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityApp.Models
{
    [Table("City",Schema ="dbo")]
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "City ID")]
        public int CityId { get; set; }

        [Required]
        [Column(TypeName="varchar(5)")]
        [MaxLength(5)]
        [Display(Name ="City Code")]
        public string Code { get; set; }

        [Required]
        [Column(TypeName = "varchar(150)")]
        [MaxLength(100)]
        [Display(Name ="Arabic Name")]
        public string ArabicName { get; set; }


        [Required]
        [Column(TypeName = "varchar(150)")]
        [MaxLength(100)]
        [Display(Name = "English Name")]
        public string EnglishName { get; set; }

        [Required]
        [Display(Name = "Active")]
        public bool isActive { get; set; }


        [Required]
        [Column(TypeName = "varchar(255)")]
        [MaxLength(200)]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [ForeignKey("Region")]
        [Required]
        public int Regionid { get; set; }

        [Display(Name ="Region")]
        [NotMapped]
        public string RegionName { get; set; }


        public virtual Region Region { get; set; }




    }
}
