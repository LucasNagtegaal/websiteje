using System;

namespace SchoolTemplate.Database
{
  public class Festival
  {
    public int Id { get; set; }
    
    public string Naam { get; set; }

    public string Beschrijving { get; set; }    

    public DateTime Start_dt { get; set; }

    public DateTime Eind_dt { get; set; }

    public string Plaatje { get; set; }

    public DateTime Datum { get; set; }
    }
}
