using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTemplate.Models
{
    public class PersonModels
    {
       public string Voornaam { get; set; }

       [Required]
       public string Achternaam { get; set; }

        [Required]
        [EmailAddress]
       public string Email { get; set; }
       public DateTime Geboortedatum { get; set; }
    }
}
