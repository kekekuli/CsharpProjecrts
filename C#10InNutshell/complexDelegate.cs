using System;
namespace multiDelegate
{
    //------------Dividing line----------------
    // If a multicast delegate returns a value, only the value from the last delegate invocation is returned to the caller.
    public delegate void ProgressReporter(int percentComplete);
    public class MultiDelegate
    {
        public static void HardWork(ProgressReporter p)
        {
            for (int i = 0; i < 10; i++)
            {
                p(i * 10);
                System.Threading.Thread.Sleep(100);
            }
        }
        public void MultiMethod()
        {
            ProgressReporter p = WriteProgressToConsole;
            p += WriteProgressToFile;
            MultiDelegate.HardWork(p);
        }
        void WriteProgressToConsole(int percentComplete)
            => Console.WriteLine(percentComplete);
        void WriteProgressToFile(int percentComplete)
            => System.IO.File.WriteAllText("progress.txt", percentComplete.ToString());
    }
    //------------Dividing line----------------
    public class GenericDelegate
    {
        public delegate T Transformer<T>(T arg);

        public static void GenericMethod()
        {
            int[] values = { 1, 2, 3 };
            Transform(values, Square);
            foreach (int i in values)
                System.Console.WriteLine(i + " ");
        }
        public static void Transform<T>(T[] values, Transformer<T> t)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = t(values[i]);
        }
        static int Square(int x) => x * x;
    }
    //------------Dividing line----------------
}