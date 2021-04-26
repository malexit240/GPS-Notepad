using System;
using System.Collections.Generic;

namespace GPSNotepad.Entities
{
    public class User
    {
        #region ---Public Properties---
        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string HashPassword { get; set; }

        public string SessionToken { get; set; }

        public List<Pin> Pins { get; set; }
        #endregion

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(UserId);
        #endregion

    }
}
