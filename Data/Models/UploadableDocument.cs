using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;

namespace FinalWork_BD_Test.Data.Models
{
    public class UploadableDocument : HistoricalModelBase<UploadableDocument>
    {
        public string Type { get; set; }
        public string OriginalName { get; set; }
        public long Length { get; set; }
        public string Path { get; set; }
        public DocumentStatus Status { get; set; }
        public string RejectReason { get; set; }
        public VKR Vkr { get; set; }
    }

    public enum DocumentStatus
    {
        Rejected,
        Approve,
        Verification
    }
}
