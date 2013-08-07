﻿namespace Runner
{
    using System;

    class Statistics
    {
        public static DateTime? First;
        public static DateTime Last;
        public static DateTime StartTime;
        public static Int64 NumberOfMessages;
        public static Int64 NumberOfRetries;
        public static TimeSpan SendTimeNoTx = TimeSpan.Zero;
        public static TimeSpan SendTimeWithTx = TimeSpan.Zero;

        public static void Dump()
        {
            Console.Out.WriteLine("");
            Console.Out.WriteLine("---------------- Statistics ----------------");

            var durationSeconds = (Statistics.Last - Statistics.First.Value).TotalSeconds;

            PrintStats("NumberOfMessages", NumberOfMessages, "#"); 
            PrintStats("Throughput", Convert.ToDouble(NumberOfMessages) / durationSeconds, "msg/s");
            PrintStats("NumberOfRetries", NumberOfRetries, "#");
            PrintStats("TimeToFirstMessage", (Statistics.First - StartTime).Value.TotalSeconds, "s");

            if (SendTimeNoTx != TimeSpan.Zero)
                PrintStats("Sending", Convert.ToDouble(NumberOfMessages / 2) / SendTimeNoTx.TotalSeconds, "msg/s");

            if (SendTimeWithTx != TimeSpan.Zero)
                PrintStats("SendingInsideTX", Convert.ToDouble(NumberOfMessages / 2) / SendTimeWithTx.TotalSeconds, "msg/s");
        }

        static void PrintStats(string key, double value, string unit)
        {
            Console.Out.WriteLine("{0}: {1:0.0} ({2})", key, value, unit);
        }
    }
}