using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPSNotepad.Model.Entities
{
    public class PlaceEvent : IEntityBase
    {
        public Guid PlaceEventId { get; set; }
        public Guid PinId { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public Guid Id => PlaceEventId;

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(Id);
        public override bool Equals(object obj)
        {
            if (!(obj is IEntityBase))
                return false;

            var pin = (IEntityBase)obj;

            return this.PinId == pin.Id;
        }
        #endregion
    }

}
