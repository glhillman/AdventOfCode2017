using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    enum InstructionType
    {
        snd,
        set,
        add,
        mul,
        mod,
        rcv,
        jgz
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
            string toString = InstType.ToString() + " " + XVar.ToString() + " ";
            if (!(InstType == InstructionType.snd || InstType == InstructionType.rcv))
            {
                toString += YIsValue ? YValue : YVar.ToString();
            }
            return toString;
        }
    }

    internal class Machine
    {
        public Machine(List<Instruction> instructions, Dictionary<char, long> vars, Queue<long> inputQ, Queue<long> outputQ)
        {
            Instructions = instructions;
            Vars = vars;
            InputQ = inputQ;
            OutputQ = outputQ;
            Index = 0;
            NWrites = 0;
            MaxIndex = Instructions.Count;
        }

        public List<Instruction> Instructions { get; private set; }
        public Dictionary<char, long> Vars { get; private set; }
        public long Index { get; set; }
        public long NWrites { get; set; }
        public Queue<long> InputQ { get; private set; }
        public Queue<long> OutputQ { get; private set; }
        private long MaxIndex { get; set; }
        public bool Process()
        {
            int nReads = 0;
            int nWrites = 0;
            while (Index >= 0 && Index < MaxIndex)
            {
                Instruction inst = Instructions[(int)Index];
                switch (inst.InstType)
                {
                    case InstructionType.snd:
                        OutputQ.Enqueue(Vars[inst.XVar]);
                        NWrites++;
                        nWrites++;
                        nReads++;
                        Index++;
                        break;
                    case InstructionType.set:
                        Vars[inst.XVar] = inst.YIsValue ? inst.YValue : Vars[inst.YVar];
                        Index++;
                        break;
                    case InstructionType.add:
                        Vars[inst.XVar] += inst.YIsValue ? inst.YValue : Vars[inst.YVar];
                        Index++;
                        break;
                    case InstructionType.mul:
                        Vars[inst.XVar] *= inst.YIsValue ? inst.YValue : Vars[inst.YVar];
                        Index++;
                        break;
                    case InstructionType.mod:
                        Vars[inst.XVar] %= inst.YIsValue ? inst.YValue : Vars[inst.YVar];
                        Index++;
                        break;
                    case InstructionType.rcv:
                        if (InputQ.Count > 0)
                        {
                            Vars[inst.XVar] = InputQ.Dequeue();
                            Index++;
                        }
                        else
                        {
                            return nWrites > 0 || nReads > 0;
                        }
                        break;
                    case InstructionType.jgz:
                        if (inst.XIsValue)
                        {
                            if (inst.XValue > 0)
                            {
                                Index += inst.YIsValue ? inst.YValue : Vars[inst.YVar];
                            }
                            else
                            {
                                Index++;
                            }
                        }
                        else if (Vars[inst.XVar] > 0)
                        {
                            Index += inst.YIsValue ? inst.YValue : Vars[inst.YVar];
                        }
                        else
                        {
                            Index++;
                        }
                        break;
                    default:
                        break;
                }
            }

            return false;
        }
    }
}
