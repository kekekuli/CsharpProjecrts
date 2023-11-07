using System;
namespace singleDelegate
{
    //------------Generic----------------
    public static class Methods
    {
        public static int Square(int x)
        {
            return x * x;
        }

        public static int Same(int x)
        {
            return x;
        }
        public static int Cube(int x)
        {
            return x * x * x;
        }
    }
    //------------Dividing line----------------
    public class SimpleDelegate
    {
        delegate int Transformer(int x);
        public void SimpleMethod()
        {
            // Same as Transformer t = new Transformer(Square);
            Transformer t = Methods.Square;
            // t(3) same as t.Invoke(3)
            Console.WriteLine(t(3));
            t = Methods.Same;
            Console.WriteLine(t(5));
        }
    }
    //------------Dividing line----------------
    public class PluginDelegate
    {
        delegate int Transformer(int x);
        public void PluginMethod()
        {
            int[] values = { 1, 2, 3 };
            Transform(values, Methods.Same);

            foreach (int i in values)
                Console.WriteLine(i);
        }
        void Transform(int[] values, Transformer t)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = t(values[i]);
        }
    }
    //------------Dividing line----------------
    public class StaticDelegate
    {
        delegate int Transformer(int x);
        public void StaticMethod()
        {
            Transformer t = Methods.Square;
            Console.WriteLine(t(10));
        }
    }
    //------------Dividing line----------------
    public class InstanceDelegate
    {
        delegate int Transformer(int x);
        public void InstanceMethod()
        {
            Transformer t = this.Square;
            Console.WriteLine(t(10));
        }
        int Square(int x)
        {
            return x * x;
        }
    }
    //------------Dividing line----------------
    class MyReporter
    {
        public string Prefix = "";

        public void ReportProgress(int percentComplete)
            => Console.WriteLine(Prefix + percentComplete);
    }
    public class TargetDelegate
    {
        // The System.Delegate class's Target property represents this instance which the method belongs.
        // and will be null for a delegate referencing a static method.
        public void TargetMethod()
        {
            MyReporter r = new MyReporter();
            r.Prefix = "Progress: ";
            ProgressReporter p = r.ReportProgress;
            p(99);
            Console.WriteLine(p.Target == r);
            Console.WriteLine(p.Method);
            r.Prefix = "";
            p(99);
        }
        // Lifetime of Target property instance is extended to (at least as long as) the delegate's lifetime.
        delegate void ProgressReporter(int percentComplete);
    }
    //------------Dividing line----------------
}