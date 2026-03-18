using System.Text.Json.Serialization;

namespace ModelLayer.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OperationType
    {
        Compare,    // Not COMPARE
        Convert,    // Not CONVERT
        Add,        // Not ADD
        Subtract,   // Not SUBTRACT
        Divide      // Not DIVIDE
    }
}