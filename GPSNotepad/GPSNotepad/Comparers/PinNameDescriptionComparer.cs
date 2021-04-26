using System;
using GPSNotepad.Entities;

namespace GPSNotepad.Comparers
{
    public class PinNameDescriptionComparer : ExcludedComparer<Pin>
    {
        #region ---Constructors---
        public PinNameDescriptionComparer(string ethalon)
        {
            this._ethalon = ethalon;
        }
        #endregion

        #region ---Private Properties
        private readonly string _ethalon;
        #endregion

        #region ---Overrides---
        public override double GetComparation(Pin item)
        {
            return CompareStringContent(item.Name ?? "") + CompareStringContent(item.Description ?? "");
        }

        public override int Compare(Pin one, Pin two)
        {
            double oneToEthalon = GetComparation(one);
            double twoToEthalon = GetComparation(two);

            return twoToEthalon.CompareTo(oneToEthalon);
        }
        #endregion

        #region ---Private Helpers---
        private double CompareStringContent(string source)
        {
            double result = 0;

            if (source.Length != 0 && _ethalon.Length != 0)
            {
                string ethalon = this._ethalon.ToLower();
                string compared = source.ToLower();

                result = compared.Contains(ethalon) ? 1 : 0;//absolute comparsion

                result *= Math.Abs(ethalon.Length / (compared.Length * 1.0));//add relativity to comparsion
            }

            return result;
        }
        #endregion
    }
}
