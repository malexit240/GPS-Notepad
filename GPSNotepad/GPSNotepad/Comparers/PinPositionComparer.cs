using Xamarin.Essentials;
using GPSNotepad.Model.Entities;
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
            double oneDistance = GetComparation(one);
            double twoDistance = GetComparation(two);

            return oneDistance.CompareTo(twoDistance);
        }

        public override double GetComparation(Pin item)
        {
            return item.Position.CalculateDistance(ethalon);
        }
    }
}
