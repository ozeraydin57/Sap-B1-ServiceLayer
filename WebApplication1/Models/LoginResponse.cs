using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication1.Models
{
    public class LoginResponse : Response
    {
        public string SessionId { get; set; }
        public string Version { get; set; }
    }


    public class Message
    {
        public string lang { get; set; }
        public string value { get; set; }
    }
    public class Error
    {
        public int code { get; set; }
        public Message message { get; set; }
    }
    public class Response
    {
        public Error error { get; set; }
    }

}
