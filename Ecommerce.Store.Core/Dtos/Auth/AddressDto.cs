using Ecommerce.Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Dtos.Auth
{
    public class AddressDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
    }
}
