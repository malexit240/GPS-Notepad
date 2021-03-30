using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPSNotepad.Model.Interfaces
{
    public interface IPermanentPinService
    {
        Task<bool> CreatePin(Pin pin);

        Task<List<Pin>> GetAllPinsForUser(Guid user_id);

        Task<Pin> UpdatePin(Pin pin);

        void DeletePin(Pin pin);
    }
}
