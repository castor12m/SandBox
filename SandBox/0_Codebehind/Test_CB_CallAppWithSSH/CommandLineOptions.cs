using CommandLine;

namespace SandBox
{
    class CommandLineOptions
    {
        [Option(shortName: 'h', longName: "host", Required = false, HelpText = "Host IP", Default = "127.0.0.1")]
        public string Host { get; set; }

        [Option(shortName: 'u', longName: "user", Required = true, HelpText = "Login User")]
        public string LoginUser { get; set; }

        [Option(shortName: 'p', longName: "passward", Required = true, HelpText = "Password")]
        public string Passward { get; set; }

        [Option(shortName: 'c', longName: "cmd", Required = false, HelpText = "Command", Default = "")]
        public string RunCommnand { get; set; }

        [Option(shortName: 's', longName: "src", Required = false, HelpText = "Source Dir")]
        public string SourceDir { get; set; }

        [Option(shortName: 'd', longName: "dst", Required = false, HelpText = "Destination Dir")]
        public string DestinationDir { get; set; }
    }
}
