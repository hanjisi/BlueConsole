using System.Text;
using System.Text.Json.Serialization;

namespace BlueConsole.Commands
{
    internal class InputCommand : Command
    {
        public string? Prefix { get; set; }
        public ValueType ValueType { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }

        public object? DefaultValue { get; set; }
        [JsonIgnore]
        public object? InputValue { get; set; }
        public override CommandKind Kind => CommandKind.ValueInput;

        public override byte[] BuildPayload()
        {
            var value = InputValue ?? DefaultValue;
            var text = $"{Prefix}{value}";

            return IsHex
            ? Convert.FromHexString(text)
            : Encoding.ASCII.GetBytes(text);
        }
    }
}
