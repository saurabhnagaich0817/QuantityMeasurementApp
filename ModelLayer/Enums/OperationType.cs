using System.Text.Json.Serialization;

namespace ModelLayer.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OperationType
    {
        Compare,    
        Convert,    
        Add,        
        Subtract,   
        Divide     
    }
}
