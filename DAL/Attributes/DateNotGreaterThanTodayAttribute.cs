using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Attributes
{
    public class DateNotGreaterThanTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            bool isValid = DateTime.TryParse(Convert.ToString(value), out dateTime);
            return (isValid && dateTime <= DateTime.Now);
        }
    }
}
