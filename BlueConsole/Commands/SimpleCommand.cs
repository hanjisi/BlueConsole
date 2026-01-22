using System.Text;

namespace BlueConsole.Commands
{
    internal class SimpleCommand : ConsoleCommand
    {
        public string? Payload { get; set; }
        public override CommandKind Kind => CommandKind.Simple;

        public override byte[] BuildPayload()
        {
            if(string.IsNullOrEmpty(Payload))
            {
                return [];
            }

            return IsHex
            ? Convert.FromHexString(Payload)
            : Encoding.ASCII.GetBytes(Payload);
        }
    }
}
