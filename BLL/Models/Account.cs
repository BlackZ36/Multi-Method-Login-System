using System;
using System.Collections.Generic;

namespace BLL.Models
{
    public partial class Account
    {
        public string AccountId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }
        public string? RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
