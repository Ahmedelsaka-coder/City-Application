using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityApp.Models
{
    [Table("Region",Schema="dbo")]
    public class Region
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Region ID")]
        public int RegionID { get; set; }

        [Required]
        [Column(TypeName="varchar(150)")]
        [Display(Name = "Region Name")]
        public string RegionName { get; set; }


      



    }
}
