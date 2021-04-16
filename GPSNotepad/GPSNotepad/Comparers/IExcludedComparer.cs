using System.Collections.Generic;

namespace GPSNotepad.Comparers
{
    public interface IExcludedComparer<T> : IComparer<T>
    {
        double GetComparation(T item);

        IList<T> GetExclusion(IList<T> source);
    }
}
