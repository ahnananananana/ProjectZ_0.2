using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV
{
    public enum Comparator
    {
        LessThan = -1,
        Equals,
        GreaterThan,
        NotEquals,
        LessThanEquals,
        GreaterThanEquals,
    }

    public enum Operation
    {
        SUM,
        MUL,
        SUB,
        DIV,
    }

    public class Calculator
    {
        public static bool Compare<T>(T a, T b, Comparator comparator) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
        {
            switch(comparator)
            {
                case Comparator.Equals:
                    return a.Equals(b);
                case Comparator.NotEquals:
                    return !a.Equals(b);
                case Comparator.GreaterThan:
                    return a.CompareTo(b) > 0;
                case Comparator.GreaterThanEquals:
                    return a.CompareTo(b) >= 0;
                case Comparator.LessThan:
                    return a.CompareTo(b) < 0;
                case Comparator.LessThanEquals:
                    return a.CompareTo(b) <= 0;
                default:
                    return false;
            }
        }

        public static float Calculate(Operation operation, params float[] numbers)
        {
            float result = numbers[0];

            switch(operation)
            {
                case Operation.SUM:
                    {
                        for (int i = 1; i < numbers.Length; ++i)
                            result += numbers[i];
                        break;
                    }
                case Operation.MUL:
                    {
                        for (int i = 1; i < numbers.Length; ++i)
                            result *= numbers[i];
                        break;
                    }
                case Operation.SUB:
                    {
                        for (int i = 1; i < numbers.Length; ++i)
                            result -= numbers[i];
                        break;
                    }
                case Operation.DIV:
                    {
                        for (int i = 1; i < numbers.Length; ++i)
                            result /= numbers[i];
                        break;
                    }
            }

            return result;
        }
    }
}
