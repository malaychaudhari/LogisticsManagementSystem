﻿using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Resource
{
    public int Id { get; set; }

    public int ResourceId { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<ResourceMapping> ResourceMappings { get; set; } = new List<ResourceMapping>();

    public virtual User ResourceNavigation { get; set; } = null!;
}
