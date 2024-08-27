using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class AccountDTO
    {
        public string Username { get; set; } = null!;
        public string? FullName { get; set; }
        public virtual RoleDTO? Role { get; set; }
    }
}
