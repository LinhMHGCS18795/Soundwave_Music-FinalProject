namespace Soundwave_Music1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VIP_Package
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VIP_Package()
        {
            Order_Detail = new HashSet<Order_Detail>();
        }

        [Key]
        public int VIP_package_id { get; set; }

        [Required]
        [StringLength(200)]
        public string VIP_package_name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime Create_date { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Detail { get; set; }
    }
}
