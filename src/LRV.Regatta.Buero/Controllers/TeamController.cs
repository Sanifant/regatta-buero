using LRV.Regatta.Buero.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Serialization;
using System;
using Microsoft.EntityFrameworkCore;

namespace LRV.Regatta.Buero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly DatabaseContext dbContext;

        public TeamController(DatabaseContext context)
        {
            this.dbContext = context;
        }

        [HttpPost()]
        public ActionResult GetVereineFromXml()
        {
            try
            {
                using var reader = new StreamReader(Request.Body, Encoding.UTF8);
                var xmlContent = reader.ReadToEndAsync().Result;

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                var serializer = new XmlSerializer(typeof(RegattaMeldungen));
                using var stringReader = new StringReader(xmlContent);
                var meldungen = (RegattaMeldungen)serializer.Deserialize(stringReader);

                this.dbContext.TeamObjects.AddRange(meldungen.Vereine);
                this.dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid XML format or deserialization failed. {ex.Message}");
            }
        }

        [HttpGet]
        public List<TeamObject> GetTeamObjects(string name = "")
        {
            return this.dbContext.TeamObjects.ToList();
        }
    }
}
