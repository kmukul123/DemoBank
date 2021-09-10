using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class SaveResponse
    {
        public bool Saved { get; set; } = false;
        public int ReturnCode { get; set; }
        public string ExternalError { get; set; }

    }
}
