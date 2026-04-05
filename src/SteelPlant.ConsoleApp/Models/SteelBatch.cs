using System;
using System.Collections.Generic;
using System.Text;

namespace SteelPlant.ConsoleApp.Models;

public class SteelBatch
{
    public int BatchId { get; set; }
    public int GradeId { get; set; }
    public DateTime StartTime { get; set; }
    public decimal? WeightKG { get; set; }
    public string Status { get; set; }
}
