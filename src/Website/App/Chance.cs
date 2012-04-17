using System;

namespace Website.App
{
    public static class Chance
    {
        public static string FlipACoin()
        {
            var random = new Random();
            var d = random.NextDouble();

            var oneOrZero = Math.Floor(d * 2);
            return (oneOrZero % 2 == 0) ? "heads" : "tails";
        }
    }
}