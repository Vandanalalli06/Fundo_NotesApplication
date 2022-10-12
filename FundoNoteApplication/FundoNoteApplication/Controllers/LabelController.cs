using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FundoNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labeBL;

        public LabelController(ILabelBL labelBL)
        {
            this.labeBL = labelBL;
        }

        [Authorize]
        [HttpPost("CreateLabel")]

        public IActionResult CreateLabel(long notesId, string LabelName)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            var result = labeBL.CreateLabel(UserId, notesId, LabelName);
            if (result != null)
                return this.Ok(new { success = true, message = "Label created Successfully", data = result });
            else
                return this.BadRequest(new { success = false, message = "Label is not Created" });
        }
        [Authorize]
        [HttpGet]

        public IActionResult GetLabel()
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            var result = labeBL.GetLabel(UserId);
            if (result != null)
                return this.Ok(new { success = true, message = "Label Retrieved Successfully", data = result});
            else
                return this.BadRequest(new { success = false, message = "Unable to retrieve Label" });
        }

    }
}

 
