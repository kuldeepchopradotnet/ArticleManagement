using System;
using System.ComponentModel.DataAnnotations;

namespace AM.Data
{
    public class ArticleModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Url { get; set; }
        public string Article { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
