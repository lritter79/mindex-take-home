using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        public int NumberOfReports { get; set; }
        public Employee Employee { get; set; }
    }
}
