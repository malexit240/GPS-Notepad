using System;
using System.Collections.Generic;

namespace GPSNotepad.Model.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }

        public List<Pin> Pins { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId);
        }
    }
}
