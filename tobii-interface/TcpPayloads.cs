using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tobii_interface
{
    [JsonObject]
    public class DataStreamStatusPayload
    {
        public int Status { get; set; }
    }

    [JsonObject]
    public class ClockSyncPayload
    {
        public long T1 { get; set; }
        public long T2 { get; set; }
    }

    [JsonObject]
    public class TextFilePayload
    {
        public string Filename { get; set; }
        public string Content { get; set; }
    }
}
