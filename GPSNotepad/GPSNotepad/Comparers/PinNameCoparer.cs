using System.Collections.Generic;
using GPSNotepad.Model.Entities;
using Xamarin.Essentials;
using GPSNotepad.Extensions;


namespace GPSNotepad.Services.PinService
{
    public class PinNameCoparer : IComparer<Pin>
    {
        private string Ethalon { get; set; }
        public PinNameCoparer(string ethalon) => this.Ethalon = ethalon;

        private double CompareStringContent(string source)
        {
            double result = 0;
            var counts = 0;

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

            result = ethalon.Length == 0 ? 0 : counts / ethalon.Length;

            return 0;
        }

        public int Compare(Pin one, Pin two)
        {
            int result = 0;

            double oneToEthalon = CompareStringContent(one.Name);
            double twoToEthalon = CompareStringContent(two.Name);


            one.Description = oneToEthalon.ToString();
            two.Description = twoToEthalon.ToString();

            result = oneToEthalon.CompareTo(twoToEthalon);

            return result;
        }
    }
}
