using System;
using System.Collections.Generic;
using System.Text;

namespace SteelPlant.ConsoleApp.Models;

public class SteelGrade
{
    public int GradeId { get; set; }
    public string GradeName { get; set; }
    public int TargetTemperature { get; set; }
}
