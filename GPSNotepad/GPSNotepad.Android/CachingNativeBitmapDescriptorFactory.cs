using System.Collections.Concurrent;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android.Factories;
using AndroidBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;

namespace GPSNotepad.Droid
{
    public sealed class CachingNativeBitmapDescriptorFactory : IBitmapDescriptorFactory
    {
        #region ---Constructors---
        public CachingNativeBitmapDescriptorFactory()
        {
            _cache = new ConcurrentDictionary<string, AndroidBitmapDescriptor>();
        }
        #endregion

        #region ---Private Files---
        private readonly ConcurrentDictionary<string, AndroidBitmapDescriptor> _cache;
        #endregion

        #region ---Public Methods---
        public AndroidBitmapDescriptor ToNative(BitmapDescriptor descriptor)
        {
            var defaultFactory = DefaultBitmapDescriptorFactory.Instance;
            AndroidBitmapDescriptor result;

            if (!string.IsNullOrEmpty(descriptor.Id))
            {
                result = _cache.GetOrAdd(descriptor.Id, _ => defaultFactory.ToNative(descriptor));

            }
            else
            {
                result = defaultFactory.ToNative(descriptor);
            }

            return result;
        }
        #endregion
    }
}

