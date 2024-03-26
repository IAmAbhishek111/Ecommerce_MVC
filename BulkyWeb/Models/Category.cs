using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        // it will be the primary key of our table  , but how will I know this is primary key , we will use data annotation 
        [Key]
        public int Id { get; set; }
        [Required]
        

        public string? Name { get; set; }
        public int DisplayOrder { get; set; }
    }
}
