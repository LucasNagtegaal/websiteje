using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTemplate.Models
{
    public class PersonModel
    {
       public string Voornaam { get; set; }

       [Required(ErrorMessage = "Achternaam is verplicht")]
       public string Achternaam { get; set; }

        [Required(ErrorMessage = "E-mail is verplicht")]
        [EmailAddress(ErrorMessage = "Geen geldig e-mail adres")]
       public string Email { get; set; }
       public DateTime Geboortedatum { get; set; }
    }
}
