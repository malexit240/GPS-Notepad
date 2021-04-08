using System.Collections.Generic;

namespace GPSNotepad.Services.PinService
{
    public abstract class ExcludedComparer<T> : IExcludedComparer<T>
    {
        public abstract int Compare(T x, T y);

        public abstract double GetComparation(T item);

        public IList<T> GetExclusion(IList<T> source)
        {
            IList<T> result = new List<T>();

            for (int i = 0; i < source.Count; i++)
            {
                if (GetComparation(source[i]) == 0)
                {
                    break;
                }
                result.Add(source[i]);
            }

            return result;
        }
    }
}
