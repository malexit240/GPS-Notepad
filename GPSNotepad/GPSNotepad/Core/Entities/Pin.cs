using System;
using Xamarin.Forms.Maps;

namespace GPSNotepad.Core.Entities
{
    public class Pin
    {
        public Guid PinId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public Position Position { get; set; }

    }
}
