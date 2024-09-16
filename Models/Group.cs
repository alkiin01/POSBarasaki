using System;
using System.Collections.Generic;

namespace POSBarasaki.Models;

public partial class Group
{
    public int Id { get; set; }

    public string? GroupCode { get; set; }

    public string? GroupName { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
