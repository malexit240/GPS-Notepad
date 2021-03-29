using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;

namespace GPSNotepad.Model.Interfaces
{
    public interface IPinService
    {
        Pin CreatePin(Guid user_id, string name);

        List<Pin> GetAllPinsForUser(Guid user_id);

        Pin FindPin(string name);

        Pin UpdatePin(Pin pin);

        void DeletePin(Pin pin);
    }
}
