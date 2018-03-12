using System;
using System.Drawing;
using static System.Math;

namespace AnalogClock
{
    public static class ClockHelper
    {
        private const double DEGREES_PER_HOUR_ON_A_12_HOUR_CLOCK = 30.0;
        private const double DEGREES_PER_HOUR_ON_A_24_HOUR_CLOCK = 15.0;
        private const double DEGREES_PER_MINUTE = 6.0;
        private const double DEGREES_PER_SECOND = 6.0;
        private const double DEGREES_OF_HOUR_HAND_PER_MINUTE_ON_A_12_HOUR_CLOCK = 0.5;
        private const double DEGREES_OF_HOUR_HAND_PER_MINUTE_ON_A_24_HOUR_CLOCK = 0.25;
        private const double HOUR_HAND_ANGLE_AT_MIDNIGHT = 90.0;

        public static Point GetEndPoint(ClockHand clockHand, int handLength, DateTime dateTime, Point origin, bool is24HourClock = true)
        {
            var hour = dateTime.Hour;
            var minute = dateTime.Minute;
            var second = dateTime.Second;

            switch (clockHand)
            {
                case ClockHand.Hour:
                    var degreesPerHour = is24HourClock ? DEGREES_PER_HOUR_ON_A_24_HOUR_CLOCK : DEGREES_PER_HOUR_ON_A_12_HOUR_CLOCK;
                    var degreesPerMinute = is24HourClock ? DEGREES_OF_HOUR_HAND_PER_MINUTE_ON_A_24_HOUR_CLOCK : DEGREES_OF_HOUR_HAND_PER_MINUTE_ON_A_12_HOUR_CLOCK;
                    return AddXYOffset(origin, GetEndPoint(handLength, HOUR_HAND_ANGLE_AT_MIDNIGHT - (hour * degreesPerHour + minute * degreesPerMinute)));
                case ClockHand.Minute:
                    return AddXYOffset(origin, GetEndPoint(handLength, HOUR_HAND_ANGLE_AT_MIDNIGHT - minute * DEGREES_PER_MINUTE));
                case ClockHand.Second:
                    return AddXYOffset(origin, GetEndPoint(handLength, HOUR_HAND_ANGLE_AT_MIDNIGHT - second * DEGREES_PER_SECOND));
                default:
                    throw new ArgumentException("Invalid clock hand selection");
            }
        }

        public static Point GetEndPoint(int handLength, double degrees)
        {
            return new Point((int)Round(handLength * Cos(PI / 180 * degrees), 5), (int)Round(-1 * handLength * Sin(PI / 180 * degrees), 5));
        }

        private static Point AddXYOffset(Point origin, Point point)
        {
            point.X = origin.X + point.X;
            point.Y = origin.Y + point.Y;

            return point;
        }
    }
}