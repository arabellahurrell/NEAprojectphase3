using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace NEAproject.Models
{
    public class homemodel
    {
        public string selectcomplexity
        {
            get;
            set;
        }

        public int valueofn
        {
            get;
            set;
        }
        //creates the list of complexity for the drop down list for the page
        public static List<SelectListItem> getcomplexity()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem{ Text = "O(n)", Value = "getLineardatapoints"});
            result.Add(new SelectListItem{ Text = "O(n^2)", Value = "getNtothe2points"});
            result.Add(new SelectListItem{ Text = "O(2^n)", Value = "get2totheNpoints"});
            result.Add(new SelectListItem{ Text = "O(logn)", Value = "getlogn"});
            result.Add(new SelectListItem{ Text = "O(nlogn)", Value = "getnlogn"});
            return result;
        }
    }
}