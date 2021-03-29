using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;

namespace GPSNotepad.Model.Interfaces
{
    public interface IPermanentPinService
    {
        bool CreatePin(Pin pin);

        List<Pin> GetAllPinsForUser(Guid user_id);

        Pin UpdatePin(Pin pin);

        void DeletePin(Pin pin);
    }
}
