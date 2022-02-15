using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
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
            long nMuls = Process(_instructions, new Dictionary<char, long>(_vars));

            Console.WriteLine("Part1: {0}", nMuls);
        }

        public void Part2()
        {
            //Dictionary<char, long> vars = new Dictionary<char, long>(_vars);
            //vars['a'] = 1;
            //Process(_instructions, vars);

            // looking for non-primes between 107900 - 124900 step 17
            long b = 0;
            long c = 0;
            long d = 0;
            long e = 0;
            long f = 0;
            long g = 0;
            long h = 0;

            b = 79;
            c = b;
            b *= 100;
            b += 100000;
            c = b;
            c += 17000;

            while (b <= c)
            {
                for (d = 2; d != b; d++)
                {
                    if (b % d == 0)
                    {
                        h++;
                        break;
                    }
                }
                b += 17;
            }

            Console.WriteLine("Part2: {0}", h);
        }

        public long Process(List<Instruction> instructions, Dictionary<char, long> vars)
        {
            long index = 0;
            long maxIndex = instructions.Count;
            long nMul = 0;
            while (index >= 0 && index < maxIndex)
            {
                Instruction inst = instructions[(int)index];
                switch (inst.InstType)
                {
                    case InstructionType.set:
                        vars[inst.XVar] = inst.YIsValue ? inst.YValue : vars[inst.YVar];
                        index++;
                        break;
                    case InstructionType.sub:
                        vars[inst.XVar] -= inst.YIsValue ? inst.YValue : vars[inst.YVar];
                        if (inst.XVar == 'h')
                        {
                            Console.WriteLine("h = " + vars['h']);
                        }
                        index++;
                        break;
                    case InstructionType.mul:
                        vars[inst.XVar] *= inst.YIsValue ? inst.YValue : vars[inst.YVar];
                        index++;
                        nMul++;
                        break;
                    case InstructionType.jnz:
                        if (inst.XIsValue)
                        {
                            if (inst.XValue != 0)
                            {
                                index += inst.YIsValue ? inst.YValue : vars[inst.YVar];
                            }
                            else
                            {
                                index++;
                            }
                        }
                        else if (vars[inst.XVar] != 0)
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

            return nMul;
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
                        case "sub":
                            xVar = parts[1][0];
                            _vars[xVar] = 0;
                            yIsValue = long.TryParse(parts[2], out yValue);
                            if (yIsValue == false)
                            {
                                yVar = parts[2][0];
                                _vars[yVar] = 0;
                            }
                            inst = new Instruction(InstructionType.sub, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
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
                        case "jnz":
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
                            inst = new Instruction(InstructionType.jnz, xIsValue, xVar, xValue, yIsValue, yVar, yValue);
                            break;
                    }

                    _instructions.Add(inst);
                }

                file.Close();
            }
        }
    }
}
