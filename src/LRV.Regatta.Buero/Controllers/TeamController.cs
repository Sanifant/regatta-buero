using LRV.Regatta.Buero.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using LRV.Regatta.Buero.Attributes;
using LRV.Regatta.Buero.Services;

namespace LRV.Regatta.Buero.Controllers
{
    /// <summary>
    /// TeamController class responsible for handling HTTP requests related to team information in the regatta management system. This controller includes endpoints for uploading team data from XML, retrieving team information, and deleting team data. The TeamController interacts with the DatabaseContext to manage team data effectively in the context of a regatta management system.
    /// </summary>
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly DatabaseContext dbContext;

        /// <summary>
        /// Constructor for the TeamController class, initializing the DatabaseContext dependency.
        /// </summary>
        /// <remarks>
        /// This constructor sets up the necessary context for managing team data in the regatta management system, 
        /// allowing the controller to handle team-related HTTP requests effectively.
        /// </remarks>
        /// <param name="context">The DatabaseContext instance used for accessing the database.</param>
        public TeamController(DatabaseContext context)
        {
            this.dbContext = context;
        }

        /// <summary>
        /// Processes the POST HTTP Verb, allowing clients to upload team data in XML format.
        /// </summary>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        /// <response code="200">Indicates that the team data was uploaded and processed successfully.</response>
        /// <response code="400">Indicates that there was an error with the XML format or deserialization process, 
        /// and the team data could not be processed.</response>
        /// <remarks>
        /// This method reads the XML content from the request body, deserializes it into a RegattaMeldungen object, 
        /// and updates the team data in the database accordingly. 
        /// The method also includes error handling to manage invalid XML formats or deserialization failures, 
        /// ensuring that the application can handle team data uploads effectively in the context of a regatta 
        /// management system.
        /// 
        /// Sample XML format for uploading team data:
        /// <code>
        /// &lt;RegattaMeldungen&gt;
        ///   &lt;Vereine&gt;
        ///     &lt;TeamObject&gt;
        ///       &lt;Name&gt;Team A&lt;/Name&gt;
        ///       &lt;Kurzform&gt;TA&lt;/Kurzform&gt;
        ///       &lt;Lettern&gt;T&lt;/Lettern&gt;
        ///     &lt;/TeamObject&gt;
        ///     &lt;TeamObject&gt;
        ///       &lt;Name&gt;Team B&lt;/Name&gt;
        ///       &lt;Kurzform&gt;TB&lt;/Kurzform&gt;
        ///       &lt;Lettern&gt;T&lt;/Lettern&gt;
        ///     &lt;/TeamObject&gt;
        ///   &lt;/Vereine&gt;
        /// &lt;/RegattaMeldungen&gt;
        /// </code>
        /// </remarks>
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

        /// <summary>
        /// Processes the GET HTTP Verb, retrieving all team information from the database. This method returns a list of TeamObject instances representing the team data stored in the database, allowing clients to access and analyze team information effectively in the context of a regatta management system.
        /// </summary>
        /// <returns>A list of TeamObject instances representing the team data stored in the database.</returns>
        [HttpGet]
        public List<TeamObject> GetTeams()
        {
            return this.dbContext.TeamObjects.ToList();
        }

        /// <summary>
        /// Processes the GET HTTP Verb with a query parameter, allowing clients to search for team information based on the team name. This method accepts a teamName query parameter, performs a case-insensitive search in the database for matching team names, short forms, or letters, and returns a list of TeamObject instances that match the search criteria. The method also includes validation for the query parameter to ensure that it is at least 3 characters long, allowing for effective searching of team information in the context of a regatta management system.
        /// </summary>        
        /// <param name="teamName">The team name query parameter used for searching team information.</param>
        /// <returns>A list of TeamObject instances that match the search criteria based on the team name.</returns>
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

        /// <summary>
        /// Processes the DELETE HTTP Verb, allowing clients to delete all team information from the database. This method executes a delete operation on the TeamObjects table, removing all team data stored in the database, and returns an Ok result to indicate the successful deletion of team information in the context of a regatta management system.
        /// </summary>
        /// <returns>An Ok result indicating the successful deletion of all team information.</returns>
        [HttpDelete]
        public IActionResult DeleteAllTeams()
        {
            this.dbContext.TeamObjects.ExecuteDelete();
            this.dbContext.SaveChanges();
            return Ok();
        }
    }
}
