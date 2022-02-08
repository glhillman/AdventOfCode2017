using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    internal class DayClass
    {
        private List<Instruction> _instructions = new();
        private Dictionary<char, long> _vars = new();
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            long lastSound = RunInstructions(_instructions, _vars);

            Console.WriteLine("Part1: {0}", lastSound);
        }

        public void Part2()
        {
            Queue<long> q0 = new Queue<long>();
            Queue<long> q1 = new Queue<long>();
            foreach (char c in _vars.Keys)
            {
                _vars[c] = 0;
            }
            Dictionary<char, long> vars2 = new Dictionary<char, long>(_vars);
            vars2['p'] = 1;
            Machine m0 = new Machine(_instructions, _vars, q0, q1);
            Machine m1 = new Machine(_instructions, vars2, q1, q0);
            bool stillRunning;
            do
            {
                stillRunning = m0.Process();
                stillRunning = stillRunning || m1.Process();
            } while (stillRunning);

            Console.WriteLine("Part2: {0}", m1.NWrites);
        }

        private long RunInstructions(List<Instruction> instructions, Dictionary<char, long> vars)
        {
            long lastSound = 0;
            long maxIndex = instructions.Count;
            long index = 0;

            while (index >= 0 && index < maxIndex)
            {
                Instruction inst = instructions[(int)index];
                switch (inst.InstType)
                {
                    case InstructionType.snd:
                        lastSound = vars[inst.XVar];
                        index++;
                        break;
                    case InstructionType.set:
                        vars[inst.XVar] = inst.YIsValue ? inst.YValue : vars[inst.YVar];
                        index++;
                        break;
                    case InstructionType.add:
                        vars[inst.XVar] += inst.YIsValue ? inst.YValue : vars[inst.YVar];
                        index++;
                        break;
                    case InstructionType.mul:
                        vars[inst.XVar] *= inst.YIsValue ? inst.YValue : vars[inst.YVar];
                        index++;
                        break;
                    case InstructionType.mod:
                        vars[inst.XVar] %= inst.YIsValue ? inst.YValue : vars[inst.YVar];
                        index++;
                        break;
                    case InstructionType.rcv:
                        if (vars[inst.XVar] != 0)
                        {
                            vars[inst.XVar] = lastSound;
                            return lastSound;
                        }
                        index++;
                        break;
                    case InstructionType.jgz:
                        if (inst.XIsValue)
                        {
                            if (inst.XValue > 0)
                            {
                                index += inst.YIsValue ? inst.YValue : vars[inst.YVar];
                            }
                            else
                            {
                                index++;
                            }
                        }
                        else if (vars[inst.XVar] > 0)
                        {
                            index += inst.YIsValue ? inst.YValue : vars[inst.YVar];
                        }
                        else
                        {
                            index++;
                        }
                        break;
                    default:
                        break;
                }
            }

            return lastSound;
        }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                Instruction? inst = null;
                while ((line = file.ReadLine()) != null)
                {
                    bool xIsValue = false;
                    bool yIsValue = false;
                    char xVar = ' ';
                    long xValue = 0;
                    char yVar = ' ';
                    long yValue = 0;

                    string[] parts = line.Split(' ');
                    switch (parts[0])
                    {
                        case "snd":
                            xVar = parts[1][0];
                            _vars[xVar] = 0;
                            inst = new Instruction(InstructionType.snd, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
                            break;
                        case "set":
                            xVar = parts[1][0];
                            _vars[xVar] = 0;
                            yIsValue = long.TryParse(parts[2], out yValue);
                            if (yIsValue == false)
                            {
                                yVar = parts[2][0];
                                _vars[yVar] = 0;
                            }
                            inst = new Instruction(InstructionType.set, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
                            break;
                        case "add":
                            xVar = parts[1][0];
                            _vars[xVar] = 0;
                            yIsValue = long.TryParse(parts[2], out yValue);
                            if (yIsValue == false)
                            {
                                yVar = parts[2][0];
                                _vars[yVar] = 0;
                            }
                            inst = new Instruction(InstructionType.add, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
                            break;
                        case "mul":
                            xVar = parts[1][0];
                            _vars[xVar] = 0;
                            yIsValue = long.TryParse(parts[2], out yValue);
                            if (yIsValue == false)
                            {
                                yVar = parts[2][0];
                                _vars[yVar] = 0;
                            }
                            inst = new Instruction(InstructionType.mul, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
                            break;
                        case "mod":
                            xVar = parts[1][0];
                            _vars[xVar] = 0;
                            yIsValue = long.TryParse(parts[2], out yValue);
                            if (yIsValue == false)
                            {
                                yVar = parts[2][0];
                                _vars[yVar] = 0;
                            }
                            inst = new Instruction(InstructionType.mod, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
                            break;
                        case "rcv":
                            xVar = parts[1][0];
                            _vars[xVar] = 0;
                            inst = new Instruction(InstructionType.rcv, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
                            break;
                        case "jgz":
                            xIsValue = long.TryParse(parts[1], out xValue);
                            if (xIsValue == false)
                            {
                                xVar = parts[1][0];
                                _vars[xVar] = 0;
                            }
                            yIsValue = long.TryParse(parts[2], out yValue);
                            if (yIsValue == false)
                            {
                                yVar = parts[2][0];
                                _vars[yVar] = 0;
                            }
                            inst = new Instruction(InstructionType.jgz, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
                            break;
                    }

                    _instructions.Add(inst);
                }

                file.Close();
            }
        }

    }
}
