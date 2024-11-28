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
        public virtual Status   OrderStatus     { get; set; }

        [JsonIgnore]
        public virtual ICollection<Transaction> Transactions { get; set; }

        public Person()
        {
            
        }
        public Person(string neptun, string name, int age, string specialrequest, string student, string orderstatus)
        {
            NeptunID = neptun;
            Name = name;
            Age = age;
            SpecialRequest = specialrequest;
            Student = GetJob(student);
            OrderStatus = GetOrder(orderstatus);
        }
        private Job GetJob(string s)
        {
            Job job;
            if (s=="1")
            {
                job = Job.Student;
                return job;
            }
            else
            {
                job = Job.Teacher;
                return job;
            }
            
        }
        private Status GetOrder(string s)
        {
            Status stat;
            if (s == "1")
            {
                stat = Status.Not_Paid;
                return stat;
            }
            else if (s=="2")
            {
                stat = Status.Paid;
                return stat;
            }
            else
            {
                stat = Status.Ordered;
                return stat;
            }

        }



    }
}
