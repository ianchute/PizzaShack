using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Services
{
    public static class Constants
    {
        /// <summary>
        /// Number of customer records sent per page.
        /// </summary>
        public const Int32 CUSTOMER_PAGE_SIZE = 20;

        /// <summary>
        /// Number of delivery person records sent per page.
        /// </summary>
        public const Int32 DELIVERY_PERSON_PAGE_SIZE = 20;
    }
}