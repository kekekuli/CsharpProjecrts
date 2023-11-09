using System;
using System.ComponentModel;
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
    public class FuncAndAction
    {
        // Func and Action is delegate that already implemented in .NET
        // no difference between other custom delegate
        delegate TResult Func<out TResult>();
        delegate TResult Func<in T, out TResult>(T arg);
        delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
        //... so on

        delegate void Action();
        delegate void Action<in T>(T arg);
        delegate void Action<in T1, in T2>(T1 arg1, T2 arg2);
        //... so on
    }
    //------------Dividing line----------------
    public class VersusInterfaces
    {
        // Interface can solve which delegate solved. If one of follow conditions true, choice delegate better.
        // 1. The interface defines only a single method.
        // 2. Multicast capabiliy is needed.
        // 3. The subscriber needs to implement the interface multiple times.
    }

    //------------Dividing line----------------
    public class Compatibility
    {
        // Delegate types are all incompatible with each other, even if they have the same signature.
        public void CompatibilityMethod()
        {
            {
                D1 d1 = Method1;
                D2 d2 = Method1;
                // d1 = d2; // Error
                // but this is special
                D2 d3 = new D2(d1); // valid
            }
            {
                // Multicast delegates are considered equal if they referenct the same methods in the same order.
                D d1 = Method1;
                D d2 = Method1;
                Console.WriteLine(d1 == d2); // True
            }
        }
        delegate void D1();
        delegate void D2();
        delegate void D();
        void Method1() { }
    }
    //------------Dividing line----------------
    public class ParameterCompatibility
    {
        // A delegate can have more specific parameter types than the delegate it is assigned to.
        // Method call this thing "ordinary polymorphic behavior", while delegate call this thing "contravariance".
        public void ParameterCompatibilityMethod()
        {
            StringAction sa = new StringAction(ActOnObject);
            sa("Parameter Compatibility"); 
        }
        void ActOnObject(object o) => Console.WriteLine(o);
        delegate void StringAction(String s);
    }
    //------------Dividing line----------------
    public class ReturnCompatibility
    {
        // A delegate can have a more specific return type than the delegate it is assigned to.
        // Method call this thing "ordinary polymorphic behavior", while delegate call this thing "covariance".
        public void ReturnCompatibilityMethod()
        {
            ObjectRetriever or = new ObjectRetriever(GetString);
            object o = or();
            Console.WriteLine(o);
        }
        string GetString() => "Return Compatibility";
        delegate object ObjectRetriever();
    }
    //------------Dividing line----------------
    // Generic delegate type parameter variance
    // ...
    /*
        Func<string> x = ...;
        Func<object> y = x; // valid

        Action<object> x = ...;
        Action<string> y = x; // valid
    */
}