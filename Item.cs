using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalBattle
{
    public class Item
    {
        public string Name { get; init; }
        //public bool HeroItem { get; init; } //True = Player item. False = Enemy item.
        //public int WaveID { get; init; } //Wave of enemies that can access item.

        virtual public void Usage(Actor user, Actor target) { }
        virtual public void Message(Actor target) { }
        public void ItemEffects(Actor user, List<Actor> actors, Action<Actor, Actor> usage)
        {
            if (user.PlayerControlled)
            {
                bool validChoice = false;
                while (!validChoice)
                {
                    List<Actor> targets = user.TargetingForPotions(actors);
                    string selection = Console.ReadLine();

                    if (selection == "B" || selection == "b") { validChoice = true; user.TurnSequence(actors); }
                    else if (int.TryParse(selection, out int choice) && choice < targets.Count && choice >= 0)
                    { //                                                  ^^Make sure the choice is a valid target.
                      //                                                    Otherwise, OutOfRange exception.
                        try
                        {
                            Console.WriteLine("");
                            if (targets[choice] != user)
                            {
                                user.DisplayName();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write($" used {Name} on ");
                                targets[choice].DisplayName();
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("! ");
                                usage(user, targets[choice]);
                                Message(targets[choice]);
                                user.Items.Remove(this);
                            }
                            else
                            {
                                usage(user, user);
                                Message(user);
                                user.Items.Remove(this);
                            }

                            validChoice = true;
                            break;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid entry.\n");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid entry.\n");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            else
            {
                user.DisplayName();
                Console.WriteLine(" is choosing a target.");
                List<Actor> targets = user.PlayerControlledTargeting(actors);
                Random random = new Random();
                Actor target = targets[random.Next(targets.Count)];
                Console.WriteLine("");
                user.DisplayName();
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (target != user)
                {
                    Console.Write($" used {Name} on ");
                    target.DisplayName();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("! ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    usage(user, target);
                    Message(target);
                    user.Items.Remove(this);
                }
                else
                {
                    usage(user, user);
                    Message(user);
                    user.Items.Remove(this);
                }
            }

        }
    }
}
