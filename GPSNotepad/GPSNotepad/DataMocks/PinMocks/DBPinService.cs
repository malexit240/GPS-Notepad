using System;
using System.Collections.Generic;
using GPSNotepad.Core;
using GPSNotepad.Core.Entities;
using GPSNotepad.DatabaseMocks;

namespace GPSNotepad.DatabaseMocks.PinMocks
{
    public class DBPinService : IPinService
    {
        public bool CreatePin(Guid user_id, string name)
        {
            throw new NotImplementedException();
        }

        public Pin FindPin(string name)
        {
            throw new NotImplementedException();
        }

        public List<Pin> GetAllPinsForUser(Guid user_id)
        {
            throw new NotImplementedException();
        }
    }
}
