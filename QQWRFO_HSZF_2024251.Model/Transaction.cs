using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QQWRFO_HSZF_2024251.Model
{
    public class Transaction
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

        [JsonIgnore]
        [Required]
        public virtual Person? ThatPerson  { get; set; }
    }
}
