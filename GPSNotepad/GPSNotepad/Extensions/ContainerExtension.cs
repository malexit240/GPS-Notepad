using Prism.Ioc;

namespace GPSNotepad
{
    public static class ContainerExtension
    {
        #region ---Extension Methods---
        public static T Resolve<T>(this IContainerProvider container)
        {
            return (T)container.Resolve(typeof(T));
        }
        #endregion
    }
}
