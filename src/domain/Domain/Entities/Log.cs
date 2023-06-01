using System;

namespace Domain.Entities
{
    public class Log
    {
        public int ID { get; set; }
        public string Command { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
    }
}
