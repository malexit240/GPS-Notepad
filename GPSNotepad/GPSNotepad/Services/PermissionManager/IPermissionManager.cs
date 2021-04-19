﻿using System;
using static Xamarin.Essentials.Permissions;
using System.Threading.Tasks;

namespace GPSNotepad.Services.PermissionManager
{
    public interface IPermissionManager
    {
        Task<bool> RunWithPermission<TPermission>(Func<Task> function) where TPermission : BasePermission, new();
    }
}