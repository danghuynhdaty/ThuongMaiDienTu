using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Model.Abstract
{
    public class Auditable
    {
        public DateTime? CreatedDate { get; set; }

        [MaxLength(250)]
        public string CreatedBy { get; set; }

        public DateTime? UpdateDate { get; set; }

        [MaxLength(250)]
        public string UpdateBy { get; set; }

        [MaxLength(256)]
        public string MetaKeyword { set; get; }

        [MaxLength(256)]
        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}
