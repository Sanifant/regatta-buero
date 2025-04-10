namespace LRV.Regatta.Buero.Models
{
    public class RegistrationObject
    {
        public int Id { get; set; }
        public RegistrationType Type {get; set;}
        public required string Race { get; set; }
        public required string StartNo { get; set; }

        public required string Team { get; set; }

        public string? Position1 { get; set; }

        public string? Position2 { get; set; }

        public string? Position3 { get; set; }

        public string? Position4 { get; set; }

        public string? Position5 { get; set; }

        public string? Position6 { get; set; }

        public string? Position7 { get; set; }

        public string? Position8 { get; set; }

        public string? PositionCox { get; set; }

        public required string ChairMan { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}
