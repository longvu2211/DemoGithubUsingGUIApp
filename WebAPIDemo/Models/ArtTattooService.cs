using System;
using System.Collections.Generic;

namespace WebAPIDemo.Models;

public partial class ArtTattooService
{
    public string ServiceId { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public string ServiceNote { get; set; } = null!;

    public string? ServiceAddress { get; set; }

    public string? TelephoneNumber { get; set; }

    public virtual ICollection<ArtTattooStyle> ArtTattooStyles { get; set; } = new List<ArtTattooStyle>();
}
