using GPSNotepad.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPSNotepad.Core
{
    public interface IPinService
    {
        List<Pin> GetAllPinsForUser(Guid user_id);
        Pin FindPin(string name);
        bool CreatePin(Guid user_id, string name);
    }
}
