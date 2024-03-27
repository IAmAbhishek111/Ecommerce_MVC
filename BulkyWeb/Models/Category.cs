using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BulkyWeb.Models
{
    public class Category
    {
        // it will be the primary key of our table  , but how will I know this is primary key , we will use data annotation 
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        

        public string? Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100 , ErrorMessage = "The field Display Order must be between 1 - 100.")]
        public int DisplayOrder { get; set; }
    }
}
