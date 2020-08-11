using System;

namespace DiscordRichPresence
{
    class Utils
    {
        public static long GetUnixTime()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static string FormatDistance(double distance)
        {
            if (distance > 2000000)
            {
                return string.Format("{0:F0}Mm", distance / 1000000.0);
            }
            else if (distance > 2000)
            {
                return string.Format("{0:F0}km", distance / 1000.0);
            }
            else
            {
                return string.Format("{0:F0}m", distance);
            }
        }
    }
}
