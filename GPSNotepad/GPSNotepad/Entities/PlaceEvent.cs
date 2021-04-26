using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPSNotepad.Entities
{
    public class PlaceEvent : IEntityBase
    {
        #region ---Public Properties---
        public Guid PlaceEventId { get; set; }
        public Guid PinId { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        #endregion

        #region ---IEntityBase Implementation---
        [NotMapped]
        public Guid Id => PlaceEventId;
        #endregion

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(Id);
        public override bool Equals(object obj)
        {
            var pin = (IEntityBase)obj;

            return this.PinId == pin?.Id;
        }
        #endregion
    }

}
