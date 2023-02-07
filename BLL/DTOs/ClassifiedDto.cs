using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ClassifiedDto
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Amount { get; set; }
        public Item ReplacementItem { get; set; }
        public int ReplacementAmount { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
