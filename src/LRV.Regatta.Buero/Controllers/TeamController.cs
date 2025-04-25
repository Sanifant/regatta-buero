using LRV.Regatta.Buero.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Serialization;
using System;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using LRV.Regatta.Buero.Attributes;

namespace LRV.Regatta.Buero.Controllers
{
    [ApiKey]
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

                if (meldungen != null)
                {
                    this.dbContext.TeamObjects.ExecuteDelete();
                    this.dbContext.SaveChanges();

                    this.dbContext.TeamObjects.AddRange(meldungen.Vereine);
                    this.dbContext.SaveChanges();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Invalid XML format or deserialization failed. {ex.Message}");
            }
        }

        [HttpGet]
        public List<TeamObject> GetTeams()
        {
            return this.dbContext.TeamObjects.ToList();
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetTeam([FromQuery] string teamName) {

            if (string.IsNullOrWhiteSpace(teamName) || teamName.Length < 3)
                return BadRequest("Query must be at least 3 characters long.");

            teamName = $"%{teamName}%";

            var results = await this.dbContext.TeamObjects
            .Where(team =>
                EF.Functions.Like(team.Name, teamName) ||
                EF.Functions.Like(team.Kurzform, teamName) ||
                EF.Functions.Like(team.Lettern, teamName)
                    )
                .ToListAsync();

            return Ok(results);
        }

        [HttpDelete]
        public IActionResult DeleteAllTeams()
        {
            this.dbContext.TeamObjects.ExecuteDelete();
            this.dbContext.SaveChanges();
            return Ok();
        }
    }
}
