using System;
using System.Collections.Generic;

namespace WebAPIDemo.Models;

public partial class ArtTattooStyle
{
    public int TattooStyleId { get; set; }

    public string TattooStyleName { get; set; } = null!;

    public string? StyleDescription { get; set; }

    public double? StylePrice { get; set; }

    public string? TattooLocation { get; set; }

    public string? ServiceId { get; set; }

    public virtual ArtTattooService? Service { get; set; }
}
