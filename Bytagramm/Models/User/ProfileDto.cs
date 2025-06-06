using Bytagramm.Models.Community;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytagramm.Models.User
{
    public class ProfileDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<CreateCommunityDto> communities { get; set; }
    }
}
