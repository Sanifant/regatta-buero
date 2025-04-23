using System.Text.Json.Serialization;

namespace LRV.Regatta.Buero.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RegistrationType{
        Registration,
        LateRegistration,
        Reregistration
    }
}