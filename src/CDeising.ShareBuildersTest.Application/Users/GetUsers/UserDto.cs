using System;

namespace CDeising.ShareBuildersTest.Application.Users.GetUsers
{
    [Serializable]
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
