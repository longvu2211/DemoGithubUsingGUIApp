using AutoMapper;

namespace WebAPIDemo.Dto
{
    public class ArtTattooServiceDto
    {
        public string ServiceId { get; set; } = null!;

        public string ServiceName { get; set; } = null!;

        public string ServiceNote { get; set; } = null!;

        public string? ServiceAddress { get; set; }

        public string? TelephoneNumber { get; set; }
    }
}
