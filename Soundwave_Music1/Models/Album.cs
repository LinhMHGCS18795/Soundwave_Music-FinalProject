namespace Soundwave_Music1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Album")]
    public partial class Album
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            Album_Comment = new HashSet<Album_Comment>();
            Album_Love_React = new HashSet<Album_Love_React>();
            Songs = new HashSet<Song>();
        }

        [Key]
        public int Album_id { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; }

        [Required]
        [StringLength(100)]
        public string Album_name { get; set; }

        public DateTime Create_date { get; set; }

        [StringLength(100)]
        public string Create_by { get; set; }

        public int Genre_id { get; set; }

        public int Singer_id { get; set; }

        public int Area_id { get; set; }

        public virtual Area Area { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual Singer Singer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album_Comment> Album_Comment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Album_Love_React> Album_Love_React { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song> Songs { get; set; }
    }
}
