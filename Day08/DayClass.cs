using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    internal class DayClass
    {
        List<string> _instructions = new();
        public DayClass()
        {
            LoadData();
        }

        public void Part1()
        {
            Dictionary<string, int> registers = new();

            (int highestFinal, int highestEver) = ProcessInstructions(_instructions, registers);

            Console.WriteLine("Part1: {0}", highestFinal);
        }

        public void Part2()
        {
            Dictionary<string, int> registers = new();

            (int highestFinal, int highestEver) = ProcessInstructions(_instructions, registers);

            Console.WriteLine("Part1: {0}", highestEver);
        }

        private (int, int) ProcessInstructions(List<string> instructions, Dictionary<string, int> registers)
        {
            int highestEver = int.MinValue;

            foreach (string instruction in instructions)
            {
                int targetValue = 0;

                (string targetRegister, string inst, int offset, string compRegister, string condition, int comp) = ParseInstruction(instruction);

                // check to see if we want to do anything by evaluating expression
                int compValue = 0;
                if (registers.TryGetValue(compRegister, out compValue) == false)
                {
                    registers[compRegister] = 0;
                    compValue = 0;
                }
                bool conditionTrue = false;

                switch (condition)
                {
                    case ">":
                        conditionTrue = registers[compRegister] > comp;
                        break;          
                    case "<":           
                        conditionTrue = registers[compRegister] < comp;
                        break;           
                    case ">=":          
                        conditionTrue = registers[compRegister] >= comp;
                        break;          
                    case "==":          
                        conditionTrue = registers[compRegister] == comp;
                        break;          
                    case "<=":          
                        conditionTrue = registers[compRegister] <= comp;
                        break;          
                    case "!=":          
                        conditionTrue = registers[compRegister] != comp;
                        break;
                }

                if (conditionTrue)
                {
                    if (registers.TryGetValue(targetRegister, out targetValue) == false)
                    {
                        registers[targetRegister] = 0;
                        targetValue = 0;
                    }
                    if (inst == "inc")
                    {
                        registers[targetRegister] += offset;
                    }
                    else
                    {
                        registers[targetRegister] -= offset;
                    }

                    highestEver = Math.Max(highestEver, registers.Max(x => x.Value));
                }
            }

            return (registers.Max(x => x.Value), highestEver);
        }

        private (string, string, int, string, string, int) ParseInstruction(string instruction)
        {
            string targetRegister;
            string inst;
            int offset;
            string compRegister;
            string condition;
            int compValue;

            string[] parts = instruction.Split(' ');
            targetRegister = parts[0];
            inst = parts[1];
            offset = int.Parse(parts[2]);
            compRegister = parts[4];
            condition = parts[5];
            compValue = int.Parse(parts[6]);

            return (targetRegister, inst, offset, compRegister, condition, compValue);
        }
        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string? line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _instructions.Add(line);
                }

                file.Close();
            }
        }

    }
}
