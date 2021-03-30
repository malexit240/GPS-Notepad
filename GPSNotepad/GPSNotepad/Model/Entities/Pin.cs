using System;
using Xamarin.Forms.Maps;

namespace GPSNotepad.Model.Entities
{
    public class Pin
    {
        public Guid PinId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Favorite { get; set; }
        public Position Position { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(PinId);
        }
    }
}
