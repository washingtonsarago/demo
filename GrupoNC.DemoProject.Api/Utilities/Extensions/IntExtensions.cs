namespace GrupoNC.DemoProject.Api.Utilities.Extensions
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class IntExtensions
    {
        public static string ConvertToHoursAndMinutesString(this int content)
        {
            if (content == null || content < 0) return "00:00";

            int hourFactor = content / 60;

            return $"{hourFactor}:{(content - (hourFactor * 60)).ToString("00")}";
        }

        public static bool IsNullOrEmpty(this int? content) =>
            content.HasValue && content.Value != 0;

        public static Guid? ToGuid(this int? content, Func<Guid?> defaultReturnAction)
        {
            if (content == null || content < 0) return defaultReturnAction.Invoke();

            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(content.ToString()));
                return new Guid(hash);
            }
        }

        public static Guid? ToGuid(this int content, Func<Guid?> defaultReturnAction)
        {
            if (content < 0) return defaultReturnAction.Invoke();

            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(content.ToString()));
                return new Guid(hash);
            }
        }
    }
}