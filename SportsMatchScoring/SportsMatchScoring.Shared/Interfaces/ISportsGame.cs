using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMatchScoring.Shared.Interfaces
{
    public interface ISportsGame
    {
        public int Id { get; set; }
        public string SportName { get; set; }
        public string Description { get; set; }
        
    }
}
