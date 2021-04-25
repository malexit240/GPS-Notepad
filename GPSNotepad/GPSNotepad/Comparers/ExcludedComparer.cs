using System.Collections.Generic;

namespace GPSNotepad.Comparers
{
    public abstract class ExcludedComparer<T> : IExcludedComparer<T>
    {
        #region ---IExcludedComparer implementation---
        public abstract int Compare(T x, T y);

        public abstract double GetComparation(T item);

        public IList<T> GetExclusion(IList<T> source)
        {
            IList<T> result = new List<T>();

            for (int i = 0; i < source.Count; i++)
            {
                if (GetComparation(source[i]) == 0)//Exclude elements with zero similarity to ethalon
                {
                    break;
                }
                result.Add(source[i]);
            }

            return result;
        }
        #endregion
    }
}
