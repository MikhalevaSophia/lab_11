using System;
using System.Collections.Generic;

namespace Lab_11;

public partial class TickerEntry
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Condition { get; set; }

    public virtual ICollection<PriceEntry> PriceEntries { get; } = new List<PriceEntry>();
}
