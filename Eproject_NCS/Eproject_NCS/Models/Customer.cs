﻿using System;
using System.Collections.Generic;

namespace Eproject_NCS.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string AccountId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public string? AddressProof { get; set; }

    public string? ApplicationForm { get; set; }

    public string? Receipt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Billing> Billings { get; set; } = new List<Billing>();

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
