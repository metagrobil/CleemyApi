﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace Cleemy.Entities
{
    public partial class CleemyUser
    {
        public CleemyUser()
        {
            Expense = new HashSet<Expense>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Currency { get; set; }

        public virtual ICollection<Expense> Expense { get; set; }
    }
}