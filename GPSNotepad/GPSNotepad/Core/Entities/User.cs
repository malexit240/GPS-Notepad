using System;
using System.Collections.Generic;

namespace GPSNotepad.Core.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }

        public List<Pin> Pins { get; set; }
    }
}
