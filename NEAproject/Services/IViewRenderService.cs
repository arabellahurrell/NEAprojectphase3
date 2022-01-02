using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NEAproject.Services
{
    //interface for render service page to convert the razor page into string 
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string Viewname, object model);
        //Task is the generic class that works as threading behind scene and will return
        //any data type (in this case string) for any user defined class
        //Asynchronised means that 8 and 9 will be executed at the same time (like pipelining)

    }
}
