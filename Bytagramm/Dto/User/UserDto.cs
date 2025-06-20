﻿using Bytagramm.Dto.Community;

namespace Bytagramm.Dto.User
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<CommunityDto> SubscribedCommunities { get; set; }
    }
}
