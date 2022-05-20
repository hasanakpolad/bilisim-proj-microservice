using SkiPass.User.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiPass.User.Data.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserGenderEnum Gender { get; set; }
        public UserBodySizeEnum UserBodySize { get; set; }
    }
}
