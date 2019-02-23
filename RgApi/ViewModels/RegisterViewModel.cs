using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [MaxLength(128)]
        public string Street { get; set; }

        [Required]
        [MaxLength(90)]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        [MaxLength(20)]
        public string Zip { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // If Salon
        public string SalonName { get; set; }
        public string SalonLicense { get; set; }
    }
}
