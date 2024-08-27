using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BLL.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public string RoleId { get; set; } = null!;
        public string RoleName { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
