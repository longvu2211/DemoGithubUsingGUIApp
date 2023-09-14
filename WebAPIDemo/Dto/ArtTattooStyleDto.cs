namespace WebAPIDemo.Dto
{
    public class ArtTattooStyleDto
    {
        public int TattooStyleId { get; set; }

        public string TattooStyleName { get; set; } = null!;

        public string? StyleDescription { get; set; }

        public double? StylePrice { get; set; }

        public string? TattooLocation { get; set; }

        public string? ServiceId { get; set; }
    }
}
