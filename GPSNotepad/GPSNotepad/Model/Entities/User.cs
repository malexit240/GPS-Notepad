using System;
using System.Collections.Generic;

namespace GPSNotepad.Model.Entities
{
    public class User
    {
        #region ---Model Fields---
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
