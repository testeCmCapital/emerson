using Domain.Entities;
using Infrastructure.CrossCutting.Helpers;
using Newtonsoft.Json;
using System;

namespace Domain.Models.Response
{
    public class AuditResponse
    {
        public string Operation { get; set; }
        public string ObjectType { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime OccurrenceDateTime { get; set; }
        public string RelativeDate => Utils.GetRelativeDateTime(OccurrenceDateTime);
        public object ObjectInfo { get; set; }

        public AuditResponse(Audit audit)
        {
            Operation = audit.Type;
            ObjectType = audit.TableName;
            Timestamp = audit.DateTime;
            ObjectInfo = audit.NewValues != null ? JsonConvert.DeserializeObject<object>(audit.NewValues) : null;
            OccurrenceDateTime = audit.DateTime;
        }
    }
}
