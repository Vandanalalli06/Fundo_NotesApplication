using Business_Layer.Service;
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
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateLabel(long labelId, string newLabelName)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labeBL.UpdateLabel(labelId, newLabelName);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Successfully Updated", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Could Not Be Updated" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteLabel(long labelId, long notesId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labeBL.LabelDelete(notesId, labelId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Successfully Deleted", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Label Could Not Be Deleted" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

 
