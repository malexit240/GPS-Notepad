using Xamarin.Essentials;
using System;
using static Xamarin.Essentials.Permissions;
using System.Threading.Tasks;

namespace GPSNotepad.Services.PermissionManager
{
    public class PermissionManager : IPermissionManager
    {
        public async Task<bool> RunWithPermission<TPermission>(Func<Task> action) where TPermission : BasePermission, new()
        {
            bool result = false;
            var status = await CheckStatusAsync<TPermission>();

            if (status != PermissionStatus.Granted)
                status = await RequestAsync<TPermission>();

            if (status == PermissionStatus.Granted)
            {
                await action();
                result = true;
            }

            return result;

        }
    }
}
