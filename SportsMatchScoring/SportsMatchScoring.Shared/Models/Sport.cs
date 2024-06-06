using SportsMatchScoring.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Models
{
    public class Sport : ISportsGame
    {
        public required int Id { get; set; }
        public required string SportName { get; set; }
        public required string Description { get; set; }
    }
}
