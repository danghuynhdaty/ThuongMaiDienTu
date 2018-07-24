using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Model.Abstract
{
    public class Auditable
    {
        public DateTime? CreatedDate { get; set; }

        [MaxLength(250)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(250)]
        public string UpdatedBy { get; set; }

        [MaxLength(256)]
        public string MetaKeyword { set; get; }

        [MaxLength(256)]
        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}