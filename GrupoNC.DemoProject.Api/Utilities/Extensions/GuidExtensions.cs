namespace GrupoNC.DemoProject.Api.Utilities.Extensions
{
    using System;

    public static class GuidExtensions
    {
        public static bool IsNullOrEmpty(this Guid guid) => guid == null || guid == default || guid == Guid.Empty;

        public static bool IsNullOrEmpty(this Guid? guid) => !guid.HasValue || guid.Value == default || guid.Value == Guid.Empty;

        public static Guid? ToGuid(this Guid? content, Func<Guid?> defaultReturnAction)
        {
            if (content.IsNullOrEmpty()) return defaultReturnAction.Invoke();
            return content;
        }
    }
}