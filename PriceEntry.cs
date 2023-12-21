using System;
using System.Collections.Generic;

namespace Lab_11;

public partial class PriceEntry
{
    public int Id { get; set; }

    public int? TickerId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? Date { get; set; }

    public virtual TickerEntry? Ticker { get; set; }
}
