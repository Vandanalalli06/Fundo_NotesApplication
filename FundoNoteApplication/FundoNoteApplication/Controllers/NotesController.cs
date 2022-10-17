using Business_Layer.Interface;
using Common_Layer.Model;
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
using Repository_Layer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        private readonly FundoContext fundocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<NotesController> _logger;
        public NotesController(INotesBL notesBL,FundoContext fundocontext,IMemoryCache memoryCache,IDistributedCache distributedCache, ILogger<NotesController> _logger)
        {
            this.notesBL = notesBL;
            this.fundocontext = fundocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this._logger = _logger;
        }
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(NotesModel notes)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Create(notes, UserID);
                if (result != null)
                {
                    _logger.LogInformation("Notes Created Succesfully");
                    return Ok(new { success = true, message = "Notes Created Successfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Notes Not Created Succesfully");
                    return NotFound(new { success = false, message = "Notes is not created " });
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
        public ActionResult Update(NotesModel notesPostModel, long NoteId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Update(notesPostModel, UserID, NoteId);
                if (result != null)
                {
                    _logger.LogInformation("Notes Updated Succesfully");
                    return Ok(new { success = true, message = "Updates Notes Successfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Notes not updated Succesfully");
                    return BadRequest(new { success = false, message = "Not Updated " });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Retrieve")]
        public ActionResult RetrieveNote(long UserId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.NotesRetrieve(UserID);
                if (result != null)
                {
                    _logger.LogInformation("Notes Retrieved Succesfully");
                    return Ok(new { success = true, message = "Notes Retrieved", data = result });
                }
                else
                {
                    _logger.LogInformation("Notes not Retrieved Succesfully");
                    return BadRequest(new { success = false, message = "Notes Retrieved Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteNote(long NoteId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.NoteDelete(UserID, NoteId);
                if (result != true)
                {
                    _logger.LogInformation("Notes Deleted Succesfully");
                    return Ok(new { success = true, message = "Notes Deleted Succesfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Notes Not Deleted Succesfully");
                    return BadRequest(new { success = false, message = "Notes Deletion Unsuccesfull" });
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
        [Route("Pin")]
        public ActionResult PinNote(long NoteId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.NotePin(NoteId, UserID);
                if (result != null)
                {
                    _logger.LogInformation("Notes Pinneded Succesfully");
                    return Ok(new { success = true, message = "Pinned Note Successfully", data = result });
                }
                else if (result == null)
                {
                    _logger.LogInformation("Notes Pinneded Succesfully");
                    return Ok(new { success = false, message = "Pin Note is UnSuccessfull" });
                }
                return BadRequest(new { success = false, message = "Could not perform Pin Operation" });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public ActionResult ArchiveNote(long noteId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.NoteArchive(noteId, userID);
                if (result != null)
                {
                    _logger.LogInformation("Notes Archived Succesfully");
                    return Ok(new { success = true, message = "Archived Note Successfully", data = result });
                }
                else if (result == null)
                {
                    _logger.LogInformation("Notes Not Archived Succesfully");
                    return Ok(new { success = true, message = "Archived Note UnSuccessfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could not perform Archive Operation" });
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
        [Route("Trash")]
        public ActionResult TrashNote(long NoteId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.NoteTrash(UserID, NoteId);
                if (result != false)
                {
                    _logger.LogInformation("Notes Trashed Succesfully");
                    return Ok(new { success = true, message = "Trashed Note" });
                }
                else if (result != true)
                {
                    _logger.LogInformation("Notes Trashed Succesfully");
                    return Ok(new { success = true, message = "Notes Trashed Successfully" });
                }
                else
                {
                    _logger.LogInformation("Notes not Trashed Succesfully");
                    return BadRequest(new { success = false, message = "Could not Perform Trash Operation" });
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
        [Route("Colour")]
        public IActionResult ColourChange(long notesId, string Colour)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var resdata = notesBL.NoteColourChange(notesId, Colour);
                if (resdata != null)
                {
                    _logger.LogInformation("Colour Changed Succesfully");
                    return this.Ok(new { success = true, message = "Colour changed Successfully", data = resdata });
                }
                else
                {
                    _logger.LogInformation("Colour not Changed Succesfully");
                    return this.BadRequest(new { success = false, message = "Colour Changing Unsuccessfull" });
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
        [Route("Image")]
        public IActionResult Image(IFormFile image, long notesId)
        {
            try
            {
                long UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserID").Value);
                var result = notesBL.Image(UserID,notesId ,image);
                if (result != null)
                {
                    _logger.LogInformation("Image Uplaod Succesfully");
                    return Ok(new { success = true, message = "Image Uploaded SUccesfully", data = result });
                }
                else
                {
                    _logger.LogInformation("Image Uplaod UnSuccesfull");
                    return BadRequest(new { success = false, message = "Image Upload Unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw(ex);
            }
        }
        [Authorize]
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            try
            {
                var cacheKey = "NotesList";
                string serializedNotesList;
                var notesList = new List<NotesEntity>();
                var redisNotesList = await distributedCache.GetAsync(cacheKey);
                if (redisNotesList != null)
                {
                    _logger.LogInformation("Notes Retrieved Succesfully");
                    serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                    notesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
                }
                else
                {
                    _logger.LogInformation("Not not Retrieved Succesfully");
                    notesList = await fundocontext.NotesTable.ToListAsync();
                    serializedNotesList = JsonConvert.SerializeObject(notesList);
                    redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisNotesList, options);
                }
                return Ok(notesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw (ex);
            }
        }
    }
}