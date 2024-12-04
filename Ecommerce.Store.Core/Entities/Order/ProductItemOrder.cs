using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Entities.Order
{
    public class ProductItemOrder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public string ProductName { get; internal set; }
        [NotMapped]
        public int ProductId { get; internal set; }

        public ProductItemOrder(int id, string name, string pictureUrl)
        {
            Id = id;

            Name = name;

            PictureUrl = pictureUrl;
        }
    }

}
