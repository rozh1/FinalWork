using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Localization.Enum;
using FinalWork_BD_Test.Data.Models.Base;

namespace FinalWork_BD_Test.Data.Models
{
    public class UploadableDocument : HistoricalModelBase<UploadableDocument>
    {
        [DisplayName("Вид документа")]
        public string Type { get; set; }
        public string OriginalName { get; set; }
        public long Length { get; set; }
        public string Path { get; set; }
        
        [DisplayName("Статус")]
        public DocumentStatus Status { get; set; }
        [ForeignKey("Status")]
        public virtual LocalizedDocumentStatus LocalizedStatus { get; set;}
        
        [DisplayName("Комментарий")]
        public string RejectReason { get; set; }
        public VKR Vkr { get; set; }
    }

    public enum DocumentStatus
    {
        Verification,
        Rejected,
        Approved,
    }
}
