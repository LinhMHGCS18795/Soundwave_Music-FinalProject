namespace Soundwave_Music1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Song")]
    public partial class Song
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Song()
        {
            Song_Comment = new HashSet<Song_Comment>();
            Song_Love_React = new HashSet<Song_Love_React>();
        }

        [Key]
        public int Song_id { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; }

        [Required]
        [StringLength(100)]
        public string Song_name { get; set; }

        public int View_count { get; set; }

        [Required]
        [StringLength(500)]
        public string Music_File_Upload { get; set; }

        public DateTime Release_date { get; set; }

        public string Lyric { get; set; }

        public int? Album_id { get; set; }

        public int Singer_id { get; set; }

        public int Composer_id { get; set; }

        public int Genre_id { get; set; }

        public int Supplier_id { get; set; }

        public int Area_id { get; set; }

        public virtual Album Album { get; set; }

        public virtual Area Area { get; set; }

        public virtual Composer Composer { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual Singer Singer { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song_Comment> Song_Comment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Song_Love_React> Song_Love_React { get; set; }
    }
}
