using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Lesson_4
{
    public class ValueString
    {
        public string Value { get; set; }

        public ValueString(string _Value)
        {
            Value = _Value;
        }

        public override bool Equals(object obj)
        {
            var valuestring = obj as ValueString;

            if (valuestring == null)
                return false;

            return Value == valuestring.Value;
        }
        public override int GetHashCode()
        {
            int ValueHashCode = Value?.GetHashCode() ?? 0;
            return ValueHashCode;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
    public class BechmarkClass
    {
        HashSet<ValueString> hashSet = new HashSet<ValueString>();
        int[] array = new int[10000];
        int TargetInt;
        public BechmarkClass()
        {
            for (int i = 0; i < 10000; i++)
            {
                array[i] = i;
                hashSet.Add(new ValueString(i.ToString()));
            }
            TargetInt = new Random().Next(10000);
        }
        [Benchmark]
        public void TestArrayEquals()
        {
            for (int i = 0; i < 10000; i++)
            {
                if (array[i] == TargetInt)
                {
                    return;
                }
            }
        }
        [Benchmark]
        public void TestHashSetEquals()
        {
            hashSet.Contains(new ValueString(TargetInt.ToString()));
        }
    }
}
