using System.ComponentModel.DataAnnotations;

namespace IdyllicWeb.Models
{
    public class MediaCategoryMaster
    {
        [Key]
        public int MediaCategoryId { get; set; }
        //public MediaType MediaType { get; set; }
        public string MediaCategoryName { get; set; }
        public string UserRole { get; set; }
    }
}