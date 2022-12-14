using Business_Layer.Service;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labeBL;
        private readonly FundoContext fundocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<LabelController> _logger;

        public LabelController(ILabelBL labelBL, FundoContext fundocontext, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<LabelController> _logger)
        {
            this.labeBL = labelBL;
            this.fundocontext = fundocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this._logger = _logger;
        }

        [Authorize]
        [HttpPost("CreateLabel")]

        public IActionResult CreateLabel(long notesId, string LabelName)
        {
            long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
            var result = labeBL.CreateLabel(UserId, notesId, LabelName);
            if (result != null)
            {
                _logger.LogInformation("Label created Successfully ");
                return this.Ok(new { success = true, message = "Label created Successfully", data = result });
            }
            else
            {
                _logger.LogInformation("Label is not created ");
                return this.BadRequest(new { success = false, message = "Label is not Created" });
            }
        }
        [Authorize]
        [HttpGet]

        public IActionResult GetLabel()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = labeBL.GetLabel(UserId);
                if (result != null)
                {
                    _logger.LogInformation("Label retrieved Successfully ");
                    return this.Ok(new { success = true, message = "Label Retrieved Successfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Unable to retrieve label");
                    return this.BadRequest(new { success = false, message = "Unable to retrieve Label" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
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
                    _logger.LogInformation("Label updated Successfully ");
                    return Ok(new { success = true, message = "Label Successfully Updated", data = result });
                }
                else
                {
                    _logger.LogInformation("Label could not be updated ");
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
                    _logger.LogInformation("Label deleted Successfully ");
                    return Ok(new { success = true, message = "Label Successfully Deleted", data = result });
                }
                else
                {
                    _logger.LogInformation("Label COuld not be Deleted");
                    return BadRequest(new { success = false, message = "Label Could Not Be Deleted" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var labelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                _logger.LogInformation("Label Retrieved SUccessfully");
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                _logger.LogInformation("Label COuld not be Retrieved");
                labelList = await fundocontext.LabelTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }
    }
}
            

    


 
