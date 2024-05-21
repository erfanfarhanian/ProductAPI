using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name of product.")]
        [MaxLength(150, ErrorMessage = "The value entered is greater than the limit.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the produce date.")]
        public DateTime ProduceDate { get; set; }

        [Required(ErrorMessage = "Please enter the manufacture phone.")]
        [MaxLength(15, ErrorMessage = "The value entered is greater than the limit.")]
        public string ManufacturePhone { get; set; }

        [Required(ErrorMessage = "Please enter the manufacture Email")]
        [EmailAddress(ErrorMessage = "Please enter the correct format of email.")]
        [MaxLength(250, ErrorMessage = "The value entered is greater than the limit.")]
        public string ManufactureEmail { get; set; }

        public bool IsAvailable { get; set; }

        public string CreatedBy { get; set; }
    }
}
