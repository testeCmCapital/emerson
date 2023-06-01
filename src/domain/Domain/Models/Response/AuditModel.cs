using Domain.Entities;
using System;

namespace Domain.Models.Response
{
    public class AuditModel
    {
        public int? UserId { get; set; }
        public string Type { get; set; }
        public string TableName { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumns { get; set; }
        public string PrimaryKey { get; set; }

        public DateTime DateTime { get; set; }
        public DateTime Date { get; set; }

        public AuditModel(Audit audit)
        {
            UserId = audit.UserId;
            Type = audit.Type;
            TableName = audit.TableName;
            OldValues = audit.OldValues;
            NewValues = audit.NewValues;
            AffectedColumns = audit.AffectedColumns;
            PrimaryKey = audit.PrimaryKey;
            DateTime = audit.DateTime;
            Date = audit.Date;
        }
    }
}
