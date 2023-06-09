﻿using System;
using System.Collections.Generic;

namespace ItAgency6.Models;

public partial class Project
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual ICollection<Tool> Tools { get; } = new List<Tool>();
}
