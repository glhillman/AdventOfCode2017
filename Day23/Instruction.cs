using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    enum InstructionType
    {
        set,
        sub,
        mul,
        jnz
    };

    internal class Instruction
    {
        public Instruction(InstructionType instType, bool xIsValue, char xVar, long xValue, bool yIsValue, char yVar, long yValue)
        {
            InstType = instType;
            XIsValue = xIsValue;
            XVar = xVar;
            XValue = xValue;
            YIsValue = yIsValue;
            YVar = yVar;
            YValue = yValue;
        }
        public InstructionType InstType { get; private set; }
        public bool XIsValue { get; private set; }
        public char XVar { get; private set; }
        public long XValue { get; private set; }
        public bool YIsValue { get; private set; }
        public char YVar { get; private set; }
        public long YValue { get; private set; }
        public override string ToString()
        {
            return InstType.ToString() + " " + XVar.ToString() + " " + (YIsValue ? YValue : YVar.ToString());
        }
    }
}
