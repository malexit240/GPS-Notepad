using System;
using System.Collections.Generic;
using System.Text;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;

namespace GPSNotepad.Model
{
    public static class CurrentUser
    {
        public static User Instance { get; set; }

    }
}
