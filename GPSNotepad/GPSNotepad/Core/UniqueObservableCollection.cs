using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GPSNotepad
{
    public class UniqueObservableCollection<T> : ObservableCollection<T>
    {
        #region ---Constructors---
        public UniqueObservableCollection() : base() => SetId();
        public UniqueObservableCollection(IList<T> collection) : base(collection) => SetId();
        public UniqueObservableCollection(IEnumerable<T> enumerable) : base(enumerable) => SetId();
        #endregion

        #region ---Public Properties---
        public Guid CollectionId { get; private set; }
        #endregion

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(CollectionId);
        public override bool Equals(object obj)
        {
            var pins = (UniqueObservableCollection<PinViewModel>)obj;

            return pins != null ? pins.CollectionId == this.CollectionId : false;
        }
        #endregion

        #region ---Private Helpers---
        private void SetId() => CollectionId = Guid.NewGuid();
        #endregion
    }
}
