using System.Collections.Generic;

namespace ESE.Store.MVC.Models
{
    public class ErrorViewModel
    {
        public int ErroCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class ResponseResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }
    }

    public class ResponseErrorMessages
    {        
        public List<string> Messages { get; set; }
    }
}
