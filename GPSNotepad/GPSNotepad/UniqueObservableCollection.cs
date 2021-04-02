using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GPSNotepad
{
    public class UniqueObservableCollection<T> : ObservableCollection<T>
    {
        #region ---Public Properties---
        public Guid CollectionId { get; private set; } 
        #endregion

        #region ---Constructors---
        public UniqueObservableCollection() : base() => SetId();
        public UniqueObservableCollection(IList<T> collection) : base(collection) => SetId();
        public UniqueObservableCollection(IEnumerable<T> enumerable) : base(enumerable) => SetId();
        #endregion

        #region ---Overrides---
        public override int GetHashCode() => HashCode.Combine(CollectionId);
        #endregion

        #region ---Private Helpers---
        private void SetId() => CollectionId = Guid.NewGuid();
        #endregion
    }
}
