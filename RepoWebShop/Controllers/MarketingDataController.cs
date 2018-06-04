using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    public class MarketingDataController : Controller
    {
        private readonly IMarketingRepository _marketingRepo;

        public MarketingDataController (IMarketingRepository marketingRepo)
        {
            _marketingRepo = marketingRepo;
        }

        [HttpPost]
        [Route("SaveTemplate/{subject}")]
        public IActionResult SaveTemplate(string subject, [FromBody] string bodyHtml)
        {
            if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(bodyHtml))
                return BadRequest(new BadRequestObjectResult(new { error = "Falta el asunto" }));
            
            _marketingRepo.SaveTemplate(subject, bodyHtml);
            return Ok();
        }

        [HttpPost]
        [Route("SendPromoEmails/{templateId}")]
        [Route("SendPromoEmails/{templateId}/{*email}")]
        public async Task<IActionResult> SendPromoEmails(int templateId, string email = null)
        {
            await _marketingRepo.SendPromoEmailsAsync(templateId, email);
            return Ok();
        }

        [HttpGet]
        [Route("GetTemplate/{templateId}")]
        public IActionResult GetTemplate(int templateId)
        {
            var result = _marketingRepo.GetTemplatesById(templateId);
            return Ok(new { bodyHtml = result.EmailBody });
        }
    }
}
