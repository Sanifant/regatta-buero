using LRV.Regatta.Buero.Interfaces;
using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LRV.Regatta.Buero.Controllers.Tests
{
    [TestClass]
    public class LogControllerTests
    {
        private MockLogService logService = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            logService = new MockLogService();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            logService.Dispose();
        }

        [TestMethod]
        public void Post_ReturnsOk_And_AddsAllLogs()
        {
            var controller = new LogController(logService);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Client-Name"] = "RegattaClient";
            httpContext.Request.Headers["X-Client-Version"] = "1.2.3";
            httpContext.Request.Headers["X-Forwarded-For"] = "203.0.113.7";

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var logs = new List<LogObject>
            {
                new() { ClientName = "", ClientVersion = "" },
                new() { Message = "manual message", ClientName = "keep-me", ClientVersion = "keep-me" }
            };

            var result = controller.Post(logs);

            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(2, logService.Logs.Count);

            var first = logService.Logs[0];
            Assert.IsFalse(string.IsNullOrWhiteSpace(first.CreatedDate.ToString()));
            Assert.AreEqual("RegattaClient", first.ClientName);
            Assert.AreEqual("1.2.3", first.ClientVersion);
            Assert.AreEqual("dummy", first.Message);
        }

        [TestMethod]
        public void Post_UsesRemoteIp_FromForwardedHeader()
        {
            var controller = new LogController(logService);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Client-Name"] = "RegattaClient";
            httpContext.Request.Headers["X-Client-Version"] = "1.2.3";
            httpContext.Request.Headers["X-Forwarded-For"] = "10.1.2.3";

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var logs = new List<LogObject> { new() { Message = "test" } };

            controller.Post(logs);

            Assert.AreEqual(1, logService.Logs.Count);

            // Erwartetes Verhalten fachlich: Remote IP soll übernommen werden
            Assert.AreEqual("10.1.2.3", logService.Logs[0].ClientIp);
        }

        [TestMethod]
        public void Post_UsesClientName_FromForwardedHeader()
        {
            var controller = new LogController(logService);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Client-Name"] = "RegattaClient";
            httpContext.Request.Headers["X-Client-Version"] = "1.2.3";
            httpContext.Request.Headers["X-Forwarded-For"] = "1.2.3";

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var logs = new List<LogObject> { new() { Message = "test" } };

            controller.Post(logs);

            Assert.AreEqual(1, logService.Logs.Count);

            // Erwartetes Verhalten fachlich: Client Name soll übernommen werden
            Assert.AreEqual("RegattaClient", logService.Logs[0].ClientName);
        }

        [TestMethod]
        public void Post_UsesClientVersion_FromForwardedHeader()
        {
            var controller = new LogController(logService);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Client-Name"] = "RegattaClient";
            httpContext.Request.Headers["X-Client-Version"] = "1.2.3";
            httpContext.Request.Headers["X-Forwarded-For"] = "1.2.3";

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var logs = new List<LogObject> { new() { Message = "test" } };

            controller.Post(logs);

            Assert.AreEqual(1, logService.Logs.Count);

            // Erwartetes Verhalten fachlich: Client Version soll übernommen werden
            Assert.AreEqual("1.2.3", logService.Logs[0].ClientVersion);
        }

        [TestMethod]
        public void Post_UsesDummy_ClientName()
        {
            var controller = new LogController(logService);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["X-Client-Version"] = "1.2.3";
            httpContext.Request.Headers["X-Forwarded-For"] = "1.2.3";

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var logs = new List<LogObject> { new() { Message = "test", ClientName = "RegattaClient" } };

            controller.Post(logs);

            Assert.AreEqual(1, logService.Logs.Count);

            // Erwartetes Verhalten fachlich: Dummy Client Name soll übernommen werden
            Assert.AreEqual("RegattaClient", logService.Logs[0].ClientName);
        }
    }
}