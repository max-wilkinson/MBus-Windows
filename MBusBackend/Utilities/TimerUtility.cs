using System;

namespace MBusBackend.Utilities
{
    internal interface ITimerUtility
    {
        long GetCurrentTime();
        bool IntervalHasPassed(long startTime, long endTime, int interval);
    }

    internal class TimerUtility : ITimerUtility
    {
        private long TicksInASecond = 10000000;

        public long GetCurrentTime()
        {
            return DateTime.Now.Ticks;
        }

        public bool IntervalHasPassed(long startTime, long endTime, int intervalInSeconds)
        {
            return Math.Abs(endTime - startTime) > (intervalInSeconds * TicksInASecond);
        }
    }

    internal static class TimerUtilityFactory
    {
        private static TimerUtility Timer = new TimerUtility();

        public static ITimerUtility GetTimer()
        {
            return Timer;
        }
    }
}
