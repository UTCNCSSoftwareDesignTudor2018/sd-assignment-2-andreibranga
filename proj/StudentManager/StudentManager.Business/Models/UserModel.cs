using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public Nullable<int> StudentCurrentYear { get; set; }
        public string StudentIdNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
