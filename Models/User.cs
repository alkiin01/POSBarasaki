using System;
using System.Collections.Generic;

namespace POSBarasaki.Models;

public partial class Users
{
    public int Id { get; set; }

    public int? GroupId { get; set; }

    public int? GroupName { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Fullname { get; set; }

    public string? Callname { get; set; }
    public string? Email { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }
}
