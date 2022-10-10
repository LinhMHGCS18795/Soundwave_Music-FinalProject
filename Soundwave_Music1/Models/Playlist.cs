namespace Soundwave_Music1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Playlist")]
    public partial class Playlist
    {
        [Key]
        public int Playlist_id { get; set; }

        [Required]
        [StringLength(100)]
        public string Playlist_name { get; set; }

        [Column(TypeName = "date")]
        public DateTime Create_date { get; set; }

        [Required]
        public string Song_list { get; set; }

        public int User_id { get; set; }

        public virtual User User { get; set; }
    }
}
