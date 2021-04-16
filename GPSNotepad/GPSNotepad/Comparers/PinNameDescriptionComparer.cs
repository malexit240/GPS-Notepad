using System;
using GPSNotepad.Entities;

namespace GPSNotepad.Comparers
{
    public class PinNameDescriptionComparer : ExcludedComparer<Pin>
    {
        private string Ethalon { get; set; }
        public PinNameDescriptionComparer(string ethalon) => this.Ethalon = ethalon;

        private double CompareStringContent(string source)
        {
            double result = 0;

            if (source.Length != 0 && Ethalon.Length != 0)
            {
                string ethalon = Ethalon.ToLower();
                string compared = source.ToLower();

                result = compared.Contains(ethalon) ? 1 : 0;

                result *= Math.Abs(ethalon.Length / (compared.Length * 1.0));
            }

            return result;
        }

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
    }
}
