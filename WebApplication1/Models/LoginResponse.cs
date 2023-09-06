using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication1.Models
{

    public class Attachments2Line
    {
        public string SourcePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string AttachmentDate { get; set; }
        public string Override { get; set; }
        public object FreeText { get; set; }
    }

    public class FileResponse : Response
    {
        public List<Attachments2Line> Attachments2_Lines { get; set; }
    }

    public class LoginResponse : Response
    {
        public string SessionId { get; set; }
        public string Version { get; set; }
        public string RouteId { get; set; }
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
