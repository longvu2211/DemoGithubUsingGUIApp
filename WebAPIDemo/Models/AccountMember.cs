using System;
using System.Collections.Generic;

namespace WebAPIDemo.Models;

public partial class AccountMember
{
    public int AccountId { get; set; }

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public int? Role { get; set; }
}
