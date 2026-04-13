using System.Text.Json.Serialization;

namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// RegistrationType enum representing the different types of registrations in the regatta management system. This enumeration includes values for Registration, LateRegistration, and Reregistration, allowing for the categorization and management of various registration types in the application. The RegistrationType enum can be used to effectively manage and analyze regatta registrations based on their specific types in the context of a regatta management system.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RegistrationType{
        /// <summary>
        /// Represents a standard registration type for the regatta management system. This value indicates that the registration is a regular registration, allowing for the categorization and management of standard registrations in the application. The Registration value can be used to effectively manage and analyze regatta registrations based on their specific types in the context of a regatta management system.
        /// </summary>
        Registration,
        /// <summary>
        /// Represents a late registration type for the regatta management system. This value indicates that the registration is a late registration, allowing for the categorization and management of late registrations in the application. The LateRegistration value can be used to effectively manage and analyze regatta registrations based on their specific types in the context of a regatta management system.
        /// </summary>
        LateRegistration,
        /// <summary>
        /// Represents a reregistration type for the regatta management system. This value indicates that the registration is a reregistration, allowing for the categorization and management of reregistrations in the application. The Reregistration value can be used to effectively manage and analyze regatta registrations based on their specific types in the context of a regatta management system.
        /// </summary>
        Reregistration
    }
}