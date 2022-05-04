using Microsoft.AspNetCore.Mvc;
using NEAproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEAproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchHistoryDetailsController : Controller
    {
        private readonly NEAdbContext dbcontext;
        public SearchHistoryDetailsController(NEAdbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var data = await dbcontext.Searchhistory.FindAsync(id);
            if(data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
    }
}
