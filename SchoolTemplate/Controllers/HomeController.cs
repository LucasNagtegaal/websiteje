using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
              Datum = DateTime.Parse(reader["Datum"].ToString()),
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
       return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
