using System;

namespace NEAproject.Models
{
    public class ErrorViewModel
        //view model for the error page
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
