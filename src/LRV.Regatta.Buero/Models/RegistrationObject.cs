namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// RegistrationObject class representing the data structure for regatta registrations in the regatta management system. This class includes properties for Id, Type
    /// </summary>
    public class RegistrationObject
    {
        /// <summary>
        /// Gets or sets the unique identifier for the registration object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the registration object.
        /// </summary>
        public RegistrationType Type {get; set;}

        /// <summary>
        /// Gets or sets the race information for the registration object. This property is required and allows for the storage
        /// </summary>
        public required string Race { get; set; }

        /// <summary>
        /// Gets or sets the start number for the registration object. This property is required and allows for the storage of the start number associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public required string StartNo { get; set; }

        /// <summary>
        /// Gets or sets the team information for the registration object. This property is required and allows for the storage of the team associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public required string Team { get; set; }

        /// <summary>
        /// Gets or sets the first position for the registration object. This property allows for the storage of the first position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public string? Position1 { get; set; }

        /// <summary>
        /// Gets or sets the second position for the registration object. This property allows for the storage of the second position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public string? Position2 { get; set; }
        /// <summary> 
        /// Gets or sets the third position for the registration object. This property allows for the storage of the third position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context
        /// of a regatta management system.
        /// </summary>
        public string? Position3 { get; set; }

        /// <summary>
        /// Gets or sets the fourth position for the registration object. This property allows for the storage of the fourth position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public string? Position4 { get; set; }

        /// <summary>
        /// Gets or sets the fifth position for the registration object. This property allows for the storage of the fifth position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public string? Position5 { get; set; }

        /// <summary>
        /// Gets or sets the sixth position for the registration object. This property allows for the storage of the sixth position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public string? Position6 { get; set; }

        /// <summary>
        /// Gets or sets the seventh position for the registration object. This property allows for the storage of the seventh position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public string? Position7 { get; set; }

        /// <summary>
        /// Gets or sets the eighth position for the registration object. This property allows for the storage of the eighth position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public string? Position8 { get; set; }

        /// <summary>
        /// Gets or sets the ninth position for the registration object. This property allows for the storage of the ninth position associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public string? PositionCox { get; set; }

        /// <summary>
        /// Gets or sets the chairman information for the registration object. This property is required and allows for the storage of the chairman associated with the registration, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public required string ChairMan { get; set; }

        /// <summary>
        /// Gets or sets the requested at timestamp for the registration object. This property allows for the storage of the timestamp when the registration was requested, enabling the application to manage and analyze regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        public DateTime RequestedAt { get; set; }
    }
}
