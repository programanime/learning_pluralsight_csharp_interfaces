using Microsoft.AspNetCore.Mvc;
using PersonReader.Interface;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PersonReader.Factory;

namespace PeopleViewer.Controllers
{
    public class PeopleController : Controller
    {
        private ReaderFactory factory = new ReaderFactory();
        IConfiguration Configuration;
        
        public PeopleController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IActionResult UseConfiguredReader(){
            string readerType = Configuration["PersonReaderType"];
            ViewData["Title"] = $"Using {readerType}";
            return PopulatePeopleView(readerType);
        }
        
        public IActionResult UseService()
        {
            ViewData["Title"] = "Using a Web Service";
            return PopulatePeopleView("Service");
        }

        public IActionResult UseCSV()
        {
            ViewData["Title"] = "Using CSV reader";
            return PopulatePeopleView("CSV");
        }

        public IActionResult UseSQL()
        {
            ViewData["Title"] = "Using SQL reader";
            return PopulatePeopleView("SQL");
        }

        private IActionResult PopulatePeopleView(string readerType)
        {
            IPersonReader reader = factory.GetReader(readerType);
            IEnumerable<Person> people = reader.GetPeople();

            ViewData["ReaderType"] = reader.GetType().ToString();
            ViewData["Title"] = "Using SQL reader";
            return View("Index", people);
        }
    }
}