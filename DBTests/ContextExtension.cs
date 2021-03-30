using GPSNotepad.Database;

namespace DBTests
{
    public static class ContextExtension
    {
        public static void ClearDatabase(this Context context)
        {
            context.Database.EnsureDeleted();
        }
    }
}
