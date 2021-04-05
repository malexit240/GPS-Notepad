using System;
using Xamarin.Essentials;

namespace GPSNotepad.Services.SecureStorageService
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
