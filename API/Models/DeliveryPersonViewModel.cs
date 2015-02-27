using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class DeliveryPersonViewModel : BaseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}