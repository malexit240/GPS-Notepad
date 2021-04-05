using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPSNotepad.Services.PinService
{
    public interface IPinService
    {
        Task<bool> Create(Pin pin);

        Task<List<Pin>> GetAllPinsForUser(Guid userId);

        void LoadUserPins(Guid userId);

        Task<bool> Update(Pin pin);

        Task<bool> Delete(Pin pin);
        Task<bool> CreateOrUpdatePin(Pin pin);
    }
}
