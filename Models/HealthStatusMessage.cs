using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseMicroservice.Models
{
    public class HealthStatusMessage
    {
        public string ID { get; set; }
        public long MemoryUsed { get; set; }
        public double CPU { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public string ServiceName { get; set; }
    }

    public enum MSSTATUS
    {
        healthy = 1,
        Bad = 2
    }
}
