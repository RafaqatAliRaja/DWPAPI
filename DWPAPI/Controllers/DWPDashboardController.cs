using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DWPAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DWPDashboardController : ControllerBase
    {
        private readonly BusReports bus;

        public DWPDashboardController(BusReports busReports)
        {
            bus = busReports;
        }
        [HttpGet, Route("GetCompaignSummary")]
        public List<Dictionary<string, object>> GetCompaignSummary()
        {
            return bus.GetCompaignSummary();
        }

        [HttpGet, Route("GetRealtimeMonitering")]
        public List<Dictionary<string, object>> GetRealtimeMonitering()
        {
            return bus.GetDWPRealtimeMonitering();
        }

        [HttpGet, Route("GetAgentsRealtimeStatus")]
        public List<Dictionary<string, object>> GetAgentsRealtimeStatus()
        {
            return bus.GetDWPAgentRealtimeStatus();
        }
    }
}
