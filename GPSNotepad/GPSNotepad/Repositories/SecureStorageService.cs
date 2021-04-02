using System;
using Xamarin.Essentials;
using GPSNotepad.Model.Interfaces;

namespace GPSNotepad.Repositories
{
    public class SecureStorageService : ISecureStorageService
    {
        #region ---Public Properties---
        public string SessionToken
        {
            get => SecureStorage.GetAsync(nameof(SessionToken)).Result ?? Guid.Empty.ToString();
            set => SecureStorage.SetAsync(nameof(SessionToken), value);
        } 
        #endregion
    }

}
