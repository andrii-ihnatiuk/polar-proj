using System;

namespace Polar.Models
{
    public class ApplicationSettings 
    {
        public PathsSettings Paths { get; set; }
        public AuthOptionsSettings AuthOptions { get; set; }
    }

    public class PathsSettings
    {
        public string FilesPath { get; set; }
    }

    public class AuthOptionsSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int Lifetime { get; set; }
    }
}