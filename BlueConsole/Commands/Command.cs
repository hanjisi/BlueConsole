using System.Text.Json.Serialization;

namespace BlueConsole.Commands
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(SimpleCommand), "simple")]
    [JsonDerivedType(typeof(InputCommand), "input")]
    [JsonDerivedType(typeof(EnumCommand), "enum")]
    public abstract class ConsoleCommand
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// 命令分组
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// 命令描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 是否为十六进制命令
        /// </summary>
        public bool IsHex { get; set; }

        /// <summary>
        /// 命令类型
        /// </summary>
        public abstract CommandKind Kind { get; }

        /// <summary>
        /// 构建命令负载
        /// </summary>
        public abstract byte[] BuildPayload();
    }
}
