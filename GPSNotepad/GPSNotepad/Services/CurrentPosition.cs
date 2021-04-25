﻿using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using System.Threading.Tasks;
using GPSNotepad.Services.PermissionManager;

namespace GPSNotepad.Model
{
    public static class CurrentPosition
    {
        #region ---Public Static Message---
        public static Position LastChecked { get; private set; } = new Position();
        #endregion

        #region ---Public Static Methods---
        public static async Task<Position> GetAsync()
        {
            Position position = LastChecked;

            await App.Current.Container.Resolve<IPermissionManager>().RunWithPermission<Permissions.LocationWhenInUse>(async () =>
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                position = new Position(location?.Latitude ?? 0, location?.Longitude ?? 0);

                LastChecked = position;
            });

            return position;
        }
        #endregion
    }
}
