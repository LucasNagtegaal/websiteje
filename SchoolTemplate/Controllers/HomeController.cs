using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
  public class HomeController : Controller
  {
    // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
    string connectionString = "Server=172.16.160.21;Port=3306;Database=110580;Uid=110580;Pwd=omborALD;";

    public IActionResult Index()
    {
      List<Festival> festivals = new List<Festival>();
      // uncomment deze regel om producten uit je database toe te voegen
      festivals = GetFestivals();

      return View(festivals);
    }
    private void SavePerson(PersonModel person)
    {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(voornaam, achternaam, emailadres, geb_datum) VALUES(?voornaam, ?achternaam, ?email, ?geb_datum)", conn);

                cmd.Parameters.Add("?voornaam", MySqlDbType.VarChar).Value = person.Voornaam;
                cmd.Parameters.Add("?achternaam", MySqlDbType.VarChar).Value = person.Achternaam;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.Email;
                cmd.Parameters.Add("?geb_datum", MySqlDbType.Date).Value = person.Geboortedatum;
                cmd.ExecuteNonQuery();
            }
    }



        private List<Festival> GetFestivals()
    {
       List<Festival> festivals = new List<Festival>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            Festival p = new Festival
            {
              Id = Convert.ToInt32(reader["Id"]),
              Naam = reader["Naam"].ToString(),
              Beschrijving = reader["Beschrijving"].ToString(),
              Start_dt = DateTime.Parse(reader["start_dt"].ToString()),
              Eind_dt= DateTime.Parse(reader["eind_dt"].ToString()),
              Plaatje = reader["Plaatje"].ToString(),
            };
            festivals.Add(p);
          }
        }
      }

      return festivals;
    }


    [Route("Privacy")]
    public IActionResult Privacy()
    {
      return View();
    }
    [Route("Informatie")]
    public IActionResult Informatie()
    {
      return View();
    }
    [Route("Overzicht")]
    public IActionResult Overzicht()
    {
      return View(GetFestivals());
    }
    [Route("Contact")]
    public IActionResult Contact()
    {
      return View();
    }           
        
   [Route("contact")]
   [HttpPost]
    public IActionResult Contact(PersonModel model)
    {
      if(!ModelState.IsValid)
       return View(model);

      SavePerson(model);

            return Redirect("/gelukt");
    }

    [Route("gelukt")]
    
    public IActionResult Gelukt()
    {
            return View();


        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

        [Route("festival/{id}")]

    public IActionResult Festival(string id)
    {
            var model = GetFestival(id);

            return View(model);
    }

    private Festival GetFestival(string id)
        {
            List<Festival> festivals = new List<Festival>();

            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from ticket where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naam = reader["Naam"].ToString(),
                            Start_dt = DateTime.Parse(reader["start_dt"].ToString()),
                            Eind_dt = DateTime.Parse(reader["eind_dt"].ToString()),
                            Beschrijving = reader["beschrijving"].ToString(),
                            Prijs = reader["prijs"].ToString(),
                            Plaatje = reader["Plaatje"].ToString(),
                            Beschikbaarheid = Convert.ToInt32(reader["Beschikbaarheid"]),
                        };
                        festivals.Add(p);
                    }
                }
            }
            return festivals[0];
        }
  }
}
