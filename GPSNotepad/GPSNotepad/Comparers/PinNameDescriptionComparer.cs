using System.Collections.Generic;
using GPSNotepad.Model.Entities;

namespace GPSNotepad.Services.PinService
{
    public class PinNameDescriptionComparer : IComparer<Pin>
    {
        private string Ethalon { get; set; }
        public PinNameDescriptionComparer(string ethalon) => this.Ethalon = ethalon;

        private double CompareStringContent(string source)
        {
            double result = 0;
            double counts = 0;

            string ethalon = Ethalon.ToLower();
            string compared = source.ToLower();

            foreach (var c in ethalon)
            {
                foreach (var ac in compared)
                {
                    if (c == ac)
                        counts++;
                }
            }

            result = ethalon.Length == 0 ? 0 : counts / ethalon.Length / compared.Length;

            return result;
        }

        public int Compare(Pin one, Pin two)
        {
            int result = 0;

            double oneToEthalon = CompareStringContent(one.Name);
            double twoToEthalon = CompareStringContent(two.Name);

            oneToEthalon += CompareStringContent(one.Description) * 0.5;
            twoToEthalon += CompareStringContent(two.Description) * 0.5;

            result = twoToEthalon.CompareTo(oneToEthalon);

            return result;
        }
    }
}
