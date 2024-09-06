using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class FeasibilityCheck
{
    public int CheckId { get; set; }

    public string? OrderId { get; set; }

    public string FeasibilityStatus { get; set; } = null!;

    public string? Comments { get; set; }

    public virtual Order? Order { get; set; }
}
