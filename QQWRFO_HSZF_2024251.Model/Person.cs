using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QQWRFO_HSZF_2024251.Model
{
    public enum Job {Student, Teacher }
    public enum Status { Not_Paid, Paid, Ordered }
    public class Person
    {
        [Key]
        [Required]
        [JsonIgnore]
        [StringLength(6)]
        public virtual string   NeptunID        { get; set; }
        [Required]
        [StringLength(100)]
        public virtual string   Name            { get; set; }

        [Required]
        public virtual int      Age             { get; set; }

        
        [StringLength(500)]
        public virtual string   SpecialRequest  { get; set; }

        [Required]
        public virtual Job      Student         { get; set; }

        [Required]
        [JsonIgnore]
        public virtual Status   OrderStatus     { get; set; }

        [JsonIgnore]
        public virtual ICollection<PitcherTransaction> Transactions { get; set; }

        public Person()
        {
            
        }

    }
}
