using Xamarin.Essentials;
using GPSNotepad.Entities;
using GPSNotepad.Extensions;

namespace GPSNotepad.Comparers
{
    public class PinPositionComparer : ExcludedComparer<Pin>
    {
        readonly Xamarin.Forms.GoogleMaps.Position ethalon;
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
