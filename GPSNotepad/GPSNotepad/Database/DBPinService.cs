using System;
using System.Collections.Generic;
using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;

namespace GPSNotepad.Database
{
    public class DBPinService : IPermanentPinService
    {

        public bool CreatePin(Pin pin)
        {
            throw new NotImplementedException();
        }

        public List<Pin> GetAllPinsForUser(Guid user_id)
        {
            throw new NotImplementedException();
        }

        public Pin UpdatePin(Pin pin)
        {
            throw new NotImplementedException();
        }
        public void DeletePin(Pin pin)
        {
            throw new NotImplementedException();
        }
    }
}
