using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public int? CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string ConnectionType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string FeasibilityStatus { get; set; } = null!;

    public int? EquipmentId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Equipment { get; set; }

    public virtual ICollection<FeasibilityCheck> FeasibilityChecks { get; set; } = new List<FeasibilityCheck>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
