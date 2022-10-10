namespace Soundwave_Music1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            News_Comment = new HashSet<News_Comment>();
            Tags = new HashSet<Tag>();
        }

        [Key]
        public int News_id { get; set; }

        [Required]
        [StringLength(500)]
        public string News_title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; }

        public int View_count { get; set; }

        public DateTime Create_date { get; set; }

        public DateTime Update_date { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        public int User_id { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<News_Comment> News_Comment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
