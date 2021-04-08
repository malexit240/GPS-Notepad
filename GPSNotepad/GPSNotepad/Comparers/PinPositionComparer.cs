using System.Collections.Generic;
using GPSNotepad.Model.Entities;
using Xamarin.Essentials;
using GPSNotepad.Extensions;

namespace GPSNotepad.Services.PinService
{
    public class PinPositionComparer : ExcludedComparer<Pin>
    {
        Xamarin.Forms.GoogleMaps.Position ethalon;
        public PinPositionComparer(Xamarin.Forms.GoogleMaps.Position ethalon)
        {
            this.ethalon = ethalon;
        }

        public override int Compare(Pin one, Pin two)
        {
            int result = 0;

            double oneDistance = GetComparation(one);
            double twoDistance = GetComparation(two);

            if (oneDistance > twoDistance)
                result = 1;
            if (oneDistance < twoDistance)
                result = -1;

            return result;

        }

        public override double GetComparation(Pin item)
        {
            return item.Position.CalculateDistance(ethalon);
        }
    }
}
