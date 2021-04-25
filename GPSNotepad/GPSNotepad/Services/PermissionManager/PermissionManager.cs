using Xamarin.Essentials;
using System;
using static Xamarin.Essentials.Permissions;
using System.Threading.Tasks;

namespace GPSNotepad.Services.PermissionManager
{
    public class PermissionManager : IPermissionManager
    {
        #region ---IPermissionManager Implementation---
        public async Task<bool> RunWithPermission<TPermission>(Func<Task> function) where TPermission : BasePermission, new()
        {
            bool result = false;

            var status = await CheckStatusAsync<TPermission>();

            if (status != PermissionStatus.Granted)
                status = await RequestAsync<TPermission>();

            if (status == PermissionStatus.Granted)
            {
                await function();
                result = true;
            }

            return result;
        }
        #endregion
    }
}
