using System;

namespace GPSNotepad.Services.Authentication
{
    public static class TokenGenerator
    {
        public static string Get() => Guid.NewGuid().ToString();
    }
}
