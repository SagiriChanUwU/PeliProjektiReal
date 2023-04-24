using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace PeliProjekti
{
    class Program
    {

        const ConsoleColor eColor = ConsoleColor.Magenta;
        const ConsoleColor pColor = ConsoleColor.Green;
        public static void Main(string[] args)
        {
            System.Random r = new System.Random();

            List<AttackAction> actions = new List<AttackAction>();

            List<Units> mc_kuns = new List<Units>();
            mc_kuns.Add(new Units("Deathblade", 50, r.Next(1, 30)));
            mc_kuns.Add(new Units("Shadowhunter", 50, r.Next(1, 30)));
            mc_kuns.Add(new Units("Reaper", 50, r.Next(1, 30)));

            List<Units> villain_kuns = new List<Units>();
            villain_kuns.Add(new Units("Valtan", 30, r.Next(1, 30)));
            villain_kuns.Add(new Units("Vykas", 30, r.Next(1, 30)));
            villain_kuns.Add(new Units("Brelshaza", 30, r.Next(1, 30)));

            Console.WriteLine("Battle begins" + "\n"
                    + "\n"
                    + "Player army:");

            Console.ForegroundColor = pColor;
            printArmy(mc_kuns, pColor);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n" + "AI army:");

            Console.ForegroundColor = eColor;
            printArmy(villain_kuns, eColor);
            Console.ForegroundColor = ConsoleColor.White;



            while (true)
            {

                Console.WriteLine("\n" + "Player's turn: Choose unit by giving a number:");

                Console.ForegroundColor = pColor;
                printArmy(mc_kuns, pColor);
                Console.ForegroundColor = ConsoleColor.White;


                int num1 = ChooseFrom(mc_kuns);
                if (num1 == -10)
                {
                    UndoAttack(actions);
                    UndoAttack(actions);
                    continue;
                }
                Console.Write("\n");

                Console.WriteLine("Choose a target:");

                Console.ForegroundColor = eColor;
                printArmy(villain_kuns, eColor);
                Console.ForegroundColor = ConsoleColor.White;


                int num2 = ChooseFrom(villain_kuns);
                if (num2 == -10)
                {
                    continue;
                }
                Console.Write("\n");


                ColorWrite(mc_kuns[num1].name, ConsoleColor.Green);
                Console.Write(" attacks ");
                ColorWrite(villain_kuns[num2].name, ConsoleColor.Magenta);
                Console.Write(" dealing ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(mc_kuns[num1].dmg);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" damage.");
                Console.Write("\n");
                Console.Write("\n");


                Units attacker = mc_kuns[num1];
                Units defender = villain_kuns[num2];
                actions.Add(Attack(attacker, defender));

                if (IsArmyDead(villain_kuns))
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Congratulations on your victory");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }


                Console.WriteLine("AI's turn:");
                num1 = r.Next(0, mc_kuns.Count);
                num2 = r.Next(0, villain_kuns.Count);

                ColorWrite(villain_kuns[num2].name, ConsoleColor.Magenta);
                Console.Write(" attacks ");
                ColorWrite(mc_kuns[num1].name, ConsoleColor.Green);
                Console.Write(" dealing ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(villain_kuns[num2].dmg);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" damage.");
                Console.Write("\n");
                Console.Write("\n");


                attacker = villain_kuns[num2];
                defender = mc_kuns[num1];
                actions.Add(Attack(attacker, defender));

                if(IsArmyDead(mc_kuns))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Oh no, your army has been killed. Try again next time pleb -_-.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
            }

        }

        public static void printArmy(List<Units> army, ConsoleColor color)
        {
            int index = 1;

            foreach (Units u in army)
            {
                if (u.hp >= 0)
                {
                    Console.ForegroundColor = color;
                    Console.Write(Convert.ToString(index) + ": " + u.name);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" " + u.hp);
                }
                index++;
            }
        }

        public static void ColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static int ChooseFrom(List<Units> army)
        {
            while (true)
            {
                Console.WriteLine("Give a number:");
                string? input = Console.ReadLine();
                if (input == "z")
                {
                    return -10;
                }
                int num1 = -1;
                if (int.TryParse(input, out num1))
                {
                    if (army.Count >= num1)
                    {
                        num1--;
                        if (army[num1].hp <= 0 )
                        {
                            Console.WriteLine("Only living beings can be selected!");
                        }
                        else
                        {
                            return num1;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid");
                    }
                }
            }
        }

        public static AttackAction Attack(Units attacker, Units defender)
        {
            defender.hp -= attacker.dmg;
            if (defender.hp < 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(attacker.name + " has killed " + defender.name + "." + "\n");
                Console.ForegroundColor = ConsoleColor.White;
            }

            AttackAction action = new AttackAction(attacker, defender);
            return action;
        }

        public static void UndoAttack(List<AttackAction> action)
        {
            if (action.Count > 0)
            {
                int lastAttack = action.Count - 1;
                AttackAction last = action[lastAttack];
                last.Undo();
                action.RemoveAt(lastAttack);
            }
        }

        public static bool IsArmyDead(List<Units> army)
        {
            int deadCount = 0;
            foreach (Units unit in army)
            {
                if (unit.hp <= 0)
                {
                    deadCount++;
                }
            }
            if (army.Count == deadCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}