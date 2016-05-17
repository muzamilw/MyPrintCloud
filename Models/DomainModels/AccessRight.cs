using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class AccessRight
    {
        public int RightId { get; set; }
        public int SectionId { get; set; }
        public string RightName { get; set; }
        public string Description { get; set; }
        public bool? CanEdit { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; }
        public virtual ICollection<Roleright> Rolerights { get; set; }
    }
}
