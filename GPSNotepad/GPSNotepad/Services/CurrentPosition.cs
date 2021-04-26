using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using System.Threading.Tasks;
using GPSNotepad.Services.PermissionManager;

namespace GPSNotepad.Model
{
    public static class CurrentPosition
    {
        #region ---Public Static Properties---
        public static Position LastChecked { get; private set; } = new Position();
        #endregion

        #region ---Public Static Methods---
        public static async Task<Position> GetAsync()
        {
            Position position = LastChecked;

            await App.Current.Container.Resolve<IPermissionManager>().RunWithPermission<Permissions.LocationWhenInUse>(async () =>
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    position = new Position(location.Latitude, location.Longitude);
                }

                LastChecked = position;
            });

            return position;
        }
        #endregion
    }
}
