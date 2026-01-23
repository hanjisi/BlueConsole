using BlueConsole.Commands;
using System.Collections.ObjectModel;

namespace BlueConsole.Models
{
    public class CommandTab
    {
        public string? Title { get; set; }
        public CommandKind Kind { get; set; }

        public IEnumerable<ConsoleCommand>? Commands { get; set; }
    }
}
