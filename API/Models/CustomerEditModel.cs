using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class CustomerEditModel : BaseModel
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(50)]
        public string MobileNumber { get; set; }
    }
}