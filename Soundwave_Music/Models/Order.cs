namespace Soundwave_Music.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int Order_id { get; set; }

        public DateTime Order_date { get; set; }

        public double Total { get; set; }

        public int User_id { get; set; }

        public int Payment_id { get; set; }

        public int product_id { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
