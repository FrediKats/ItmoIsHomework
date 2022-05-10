using System;

namespace ReviewYourself.WebApi.Models
{
    [Flags]
    public enum MemberPermission
    {
        None = 0,
        Invited = 0b0001,
        Member = 0b0010,
        Mentor = 0b0110,
        Creator = 0b1110
    }
}