using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPSNotepad.Model.Interfaces
{
    public interface IPinService
    {
        Pin CreatePin(Guid user_id, string name);

        List<Pin> GetAllPinsForUser(Guid user_id);

        Task<Pin> FindPin(string name);

        Pin UpdatePin(Pin pin);

        void DeletePin(Pin pin);
    }
}
