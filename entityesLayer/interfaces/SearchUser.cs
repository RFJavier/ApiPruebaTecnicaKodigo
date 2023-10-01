using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entityesLayer.interfaces
{
    public class SearchUser
    {
        public int idrol { get; set; }

        public string username { get; set; }

        public string nickname { get; set; }

        public string email { get; set; }

        public string userpassword { get; set; }

        public int isactive { get; set; }

        public DateTime registerdate { get; set; }
    }
}
