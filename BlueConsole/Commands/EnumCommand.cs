using System.Text;
using System.Text.Json.Serialization;

namespace BlueConsole.Commands
{
    internal class EnumCommand : ConsoleCommand
    {
        public List<EnumOption> Options { get; set; } = new();
        [JsonIgnore]
        public EnumOption? Selected { get; set; }
        public override CommandKind Kind => CommandKind.EnumSelect;
        public override byte[] BuildPayload()
        {
            if (Selected == null)
            {
                return [];
            }
            return Encoding.ASCII.GetBytes(Selected.Payload);
        }
    }
}
