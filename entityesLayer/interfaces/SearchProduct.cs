using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entityesLayer.interfaces
{
    public class SearchProduct
    {
        public int idcategory { get; set; }

        public string productname { get; set; }

        public string code { get; set; }

        public int statusproduct {  get; set; }
    }
}
