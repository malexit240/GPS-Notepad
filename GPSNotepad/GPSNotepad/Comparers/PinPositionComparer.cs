using System.Collections.Generic;
using GPSNotepad.Model.Entities;
using Xamarin.Essentials;
using GPSNotepad.Extensions;

namespace GPSNotepad.Services.PinService
{
    public class PinPositionComparer : IComparer<Pin>
    {
        Xamarin.Forms.GoogleMaps.Position ethalon;
        public PinPositionComparer(Xamarin.Forms.GoogleMaps.Position ethalon)
        {
            this.ethalon = ethalon;
        }

        public int Compare(Pin one, Pin two)
        {
            int result = 0;

            double oneDistance = one.Position.CalculateDistance(ethalon);
            double twoDistance = two.Position.CalculateDistance(ethalon);

            if (oneDistance > twoDistance)
                result = 1;
            if (oneDistance < twoDistance)
                result = -1;

            return result;

        }
    }
}
