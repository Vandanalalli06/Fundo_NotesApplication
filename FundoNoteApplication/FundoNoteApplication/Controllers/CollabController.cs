using Business_Layer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Context;

namespace FundoNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly FundoContext fundocontext;
        public CollabController(ICollabBL collabBL,FundoContext fundocontext)
        {
            this.collabBL = collabBL;
            this.fundocontext = fundocontext;
        }
    }
}