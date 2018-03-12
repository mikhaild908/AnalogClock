using System;
using System.Drawing;

namespace AnalogClock
{
    class Program
    {
        const int HOUR_HAND_LENGTH = 9;
        const int MINUTE_HAND_LENGTH = 9;
        const int SECOND_HAND_LENGTH = 9;

        const int DEGREES_PER_HOUR = 30;

        static Point _origin;
        static Point _hourPoint;
        static Point _minutePoint;
        static Point _secondPoint;
        static int _width;
        static int _height;
        static System.Timers.Timer _timer;

        static void Main(string[] args)
        {
            //Console.ReadLine();
            Initialize();
            DrawHours();
            UpdateClock();

            Console.ReadLine();
        }

        static void Initialize()
        {
            Console.CursorVisible = false;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();

            _width = Console.WindowWidth / 3;
            _height = Console.WindowHeight / 2;
            _origin = new Point(_width, _height);
            _hourPoint = _origin;
            _minutePoint = _origin;
            _secondPoint = _origin;

            _timer = new System.Timers.Timer(60 * 1000);
            //_timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (sender, e) => UpdateClock();
            _timer.Start();
        }

        static void DrawHours()
        {
            var startingAngle = 90;
            int angle = startingAngle;

            for (int i = 12; i >= 1; i--)
            {
                //Console.WriteLine($"{i}: {angle}");

                var point = ClockHelper.GetEndPoint(MINUTE_HAND_LENGTH + 2, angle);

                Console.SetCursorPosition((int)(1.7 * (_origin.X + point.X)), _origin.Y + point.Y);
                Console.Write(i);

                angle += DEGREES_PER_HOUR;
                angle = angle >= 360 ? angle - 360 : angle;
            }

            Console.SetCursorPosition((int)(1.7 * _origin.X), _origin.Y);
            Console.Write("*");
        }

        static void DrawHourHand()
        {
            Console.SetCursorPosition((int)(1.7 * _hourPoint.X), _hourPoint.Y);
            Console.Write(" ");

            _hourPoint = ClockHelper.GetEndPoint(ClockHand.Hour, HOUR_HAND_LENGTH, DateTime.Now, _origin, false);
            Console.SetCursorPosition((int)(1.7 * _hourPoint.X), _hourPoint.Y);
            Console.Write("h");

            //for (int i = 1; i < HOUR_HAND_LENGTH - 1; i += 2)
            //{
            //    point = ClockHelper.GetEndPoint(ClockHand.Hour, HOUR_HAND_LENGTH - i, DateTime.Now, _origin, false);
            //    Console.SetCursorPosition((int)(1.7 * point.X), point.Y);
            //    Console.Write(".");
            //}
        }

        static void DrawMinuteHand()
        {
            Console.SetCursorPosition((int)(1.7 * _minutePoint.X), _minutePoint.Y);
            Console.Write(" ");
            
            _minutePoint = ClockHelper.GetEndPoint(ClockHand.Minute, MINUTE_HAND_LENGTH, DateTime.Now, _origin, false);
            Console.SetCursorPosition((int)(1.7 * _minutePoint.X), _minutePoint.Y);
            Console.Write("m");

            //for (int i = 1; i < MINUTE_HAND_LENGTH - 1; i += 2)
            //{
            //    point = ClockHelper.GetEndPoint(ClockHand.Minute, MINUTE_HAND_LENGTH - i, DateTime.Now, _origin, false);
            //    Console.SetCursorPosition((int)(1.7 * point.X), point.Y);
            //    Console.Write(".");
            //}
        }

        static void DrawSecondHand()
        {
            Console.SetCursorPosition((int)(1.7 * _secondPoint.X), _secondPoint.Y);
            Console.Write(" ");

            _secondPoint = ClockHelper.GetEndPoint(ClockHand.Second, SECOND_HAND_LENGTH, DateTime.Now, _origin, false);
            Console.SetCursorPosition((int)(1.7 * _secondPoint.X), _secondPoint.Y);
            Console.Write("s");
        }

        static void UpdateClock()
        {
            DrawHourHand();
            DrawMinuteHand();
            //DrawSecondHand();
        }

        static void TestValues()
        {
            Console.WriteLine($"Origin: {_origin}");
            //Console.WriteLine($"{ClockHelper.GetEndPoint(ClockHand.Hour, 5, new DateTime(2018, 1, 1, 12, 0, 0), _origin, false)}");

            Console.WriteLine($"{ClockHelper.GetEndPoint(ClockHand.Hour, HOUR_HAND_LENGTH, DateTime.Now, _origin, false)}");
        }
    }
}
