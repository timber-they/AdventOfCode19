using System;
using System.Collections.Generic;
using AdventOfCode.Day05;

namespace AdventOfCode.Day07
{
    public class PipedComputer : TestComputer
    {
        public Action<long> Reader { private get; set; }
        public Func<long> Writer { protected get; set; }

        /// <inheritdoc />
        public PipedComputer (IEnumerable <int> code, Action<long> reader, Func <long> writer) : base (code)
        {
            Reader = reader;
            Writer = writer;
        }

        protected PipedComputer (IEnumerable <int> code) : base (code) {}

        /// <inheritdoc />
        public PipedComputer (IEnumerable <long> code, Action<long> reader, Func <long> writer) : base (code)
        {
            Reader = reader;
            Writer = writer;
        }

        protected PipedComputer (IEnumerable <long> code) : base (code) {}

        /// <inheritdoc />
        protected override int PerformOperation (int position, int length, int opCode, long [] parameters)
        {
            switch (Code [position] % 100)
            {
                case 1:
                case 2:
                case 99:
                case 5:
                case 6:
                case 7:
                case 8:
                    return base.PerformOperation (position, length, opCode, parameters);
                case 3:
                    WriteResult (position, Writer.Invoke (), length);
                    return position + length + 1;
                case 4:
                    Reader.Invoke (parameters [0]);
                    return position + length + 1;
                default:
                    Console.WriteLine ("Something went wrong.");
                    return position + length - 1;
            }
        }
    }
}