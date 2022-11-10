using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Models
{
    public class Result
    {
        public string? artist_href { get; set; }
        public string? artist_name { get; set; }
        public string? source_url { get; set; }
        public string? url { get; set; }
    }

    public class NekoModel
    {
        public List<Result>? results { get; set; }
    }


}