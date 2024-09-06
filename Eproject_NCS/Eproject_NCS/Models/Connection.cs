using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Connection
{
    public int ConnectionId { get; set; }

    public int? CustomerId { get; set; }

    public string ConnectionType { get; set; } = null!;

    public string PlanType { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual Customer? Customer { get; set; }
}
