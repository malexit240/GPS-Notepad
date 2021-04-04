using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPSNotepad.Model.Interfaces
{
    public interface IPinService // Workes with "buffered" pin state
    {
        Pin CreatePin(Guid user_id, string name);
        void CreateOrUpdatePin(Pin pin);

        List<Pin> GetAllPinsForUser(Guid user_id);

        Task<List<Pin>> FindPin(string name);

        Pin UpdatePin(Pin pin);

        void DeletePin(Pin pin);
    }
}
