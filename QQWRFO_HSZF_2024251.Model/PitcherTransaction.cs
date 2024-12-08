using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QQWRFO_HSZF_2024251.Model
{
    public class PitcherTransaction
    {
        [Key]
        [Required]
        [JsonIgnore]
        [StringLength(16)]
        public virtual string   TransactionId   { get; set; }

        [Required]
        public virtual int      Amount          { get; set; }

        [Required]
        public virtual DateTime PaymentTime     { get; set; }


        [Required]
        public virtual string NeptunID { get; set; }

        [ForeignKey(nameof(NeptunID))]
        public virtual Person Person { get; set; }

        
    }
}
