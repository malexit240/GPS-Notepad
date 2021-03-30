using System;
using Xamarin.Essentials;

namespace GPSNotepad.Model.Interfaces
{
    public class SecureStorageService : ISecureStorageService
    {
        public string SessionToken
        {
            get => SecureStorage.GetAsync(nameof(SessionToken)).Result ?? Guid.Empty.ToString();
            set => SecureStorage.SetAsync(nameof(SessionToken), value);
        }
    }

}
