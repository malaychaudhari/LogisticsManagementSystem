using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class UserDetail
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? ShippingAddress { get; set; }

    public string? LicenseNumber { get; set; }

    public string? VehicleType { get; set; }

    public string? VechicleNumber { get; set; }

    public int? WarehouseId { get; set; }

    public bool? IsApproved { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Warehouse? Warehouse { get; set; }
}
