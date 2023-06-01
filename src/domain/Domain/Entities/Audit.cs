using System;

namespace Domain.Entities
{
    public class Audit
    {
        public int ID { get; set; }
        public int? UserId { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }        
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }

        public DateTime DateTime { get; set; }
        public DateTime Date { get; set; }

        public User User { get; set; }
    }
}
