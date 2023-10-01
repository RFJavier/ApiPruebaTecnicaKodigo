using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entityesLayer.interfaces
{
    public class ProductCategory
    {
        public int idcategory { get; set; }

        public string categoryname { get; set; }

        public int statuscategory { get; set; }
    }
}
