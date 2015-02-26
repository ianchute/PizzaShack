namespace API.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer : BaseEntity
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string MobileNumber { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
