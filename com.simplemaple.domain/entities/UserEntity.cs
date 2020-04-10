using System;
using System.Collections.Generic;
using System.Text;

namespace com.simplemaple.domain.entities
{
    public class UserEntity : BaseEntity
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Wxopenid { get; set; }
        public string QQopenid { get; set; }
        public string Unionid { get; set; }
        public string Avataurl { get; set; }

    }
}
