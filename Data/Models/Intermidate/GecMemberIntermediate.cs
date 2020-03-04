using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Profiles;

namespace FinalWork_BD_Test.Data.Models.Intermidate
{
    public class GecMemberIntermediate
    {
        public Guid GecId { get; set; }
        [ForeignKey("GecId")]
        public virtual GEC GEC { get; set; }

        public Guid MemberProfileId { get; set; }
        [ForeignKey("MemberProfileId")]
        public virtual GecMemberProfile MemberProfile { get; set; }
    }
}
