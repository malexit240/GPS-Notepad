using System.Collections.Generic;

namespace GPSNotepad.Services.PinService
{
    public interface IExcludedComparer<T> : IComparer<T>
    {
        double GetComparation(T item);
        IList<T> GetExclusion(IList<T> source);
    }
}
