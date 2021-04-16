using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Random random = new Random();
                int genAmount = 1;
                int genAmountStore = 1;
                int playerChoiceLength = 0;
                string[] archetypeArray = new string[] { "Goblin", "Human", "Elf" };
                Console.WriteLine();
                Console.WriteLine("Please choose an archetype; Goblin, Human, Elf or Random.");
                string playerChoice = Console.ReadLine();
                Archetype choice = archetypeChoice(playerChoice);
                while (choice == null)
                {
                    Console.WriteLine("Please Choose Again: Goblin, Human, Elf or Random.");
                    playerChoice = Console.ReadLine();
                    Console.WriteLine($"You have chosen {playerChoice}");
                    choice = archetypeChoice(playerChoice);
                }
                string playerChoiceRandom = playerChoice;
                if (playerChoiceRandom == "Random")
                {
                    playerChoiceLength = random.Next(0, archetypeArray.Length);
                    playerChoice = (archetypeArray[playerChoiceLength]);
                }
                Console.WriteLine();
                Console.WriteLine("Please choose the card you wish to generate: RNA (Random No Abilities), RA (Random Abilities), RE (Random Effect) or RR (Random Card Type)");
                string playerCardTypeChoice = Console.ReadLine();
                string[] playerCardTypeChoiceArray = new string[] { "RNA", "RA", "RE" };
                int playerCardTypeChoiceLength = 0;
                string RRchosen = playerCardTypeChoice;
                if (RRchosen == "RR")
                {
                    playerCardTypeChoiceLength = random.Next(0, playerCardTypeChoiceArray.Length);
                    playerCardTypeChoice = (playerCardTypeChoiceArray[playerCardTypeChoiceLength]);

                }
                CardType card = CardTypeChoice(playerCardTypeChoice, choice);

                while (card == null)
                {
                    Console.WriteLine("Please Choose Again: RNA (Random No Abilities), RA (Random Abilities), RE (Random Effect) or RR (Random Card Type)");
                    playerCardTypeChoice = Console.ReadLine();
                    Console.WriteLine($"You have chosen {playerCardTypeChoice}");
                    RRchosen = playerCardTypeChoice;
                    if (RRchosen == "RR")
                    {
                        playerCardTypeChoiceLength = random.Next(0, playerCardTypeChoiceArray.Length);
                        playerCardTypeChoice = (playerCardTypeChoiceArray[playerCardTypeChoiceLength]);
                    }
                    card = CardTypeChoice(playerCardTypeChoice, choice);

                }


                int prefixLength = random.Next(choice.prefix.Length);
                int affixLength = random.Next(card.affix.Length);
                int effectsLength = random.Next(choice.effects.Length);
                Console.WriteLine();
                Console.WriteLine($"{playerChoice} {choice.prefix[prefixLength]} {card.affix[affixLength]} ({playerCardTypeChoice})");
                Console.WriteLine($"Cost: {card.points}");
                if (card.hp != 0)
                {
                    Console.WriteLine($"Health: {card.hp}");
                }
                if (card.dm != 0)
                {
                    Console.WriteLine($"Damage: {card.dm}");
                }
                if (card.eff != 0)
                {
                    Console.WriteLine($"Effect Power: {Trigger.TriggerEffString} {choice.effects[effectsLength]} {card.eff}");
                }

                Console.WriteLine("Would you like to generate another card? Y/N");
                if (char.ToLower(Console.ReadKey().KeyChar) == 'y')
                {
                    Console.WriteLine();
                    Console.WriteLine("Would you like to use the same archetype and generation method? Y/N");
                    if (char.ToLower(Console.ReadKey().KeyChar) == 'y')
                    {
                        Console.WriteLine("How many iterations would you like to generate of this card at once? (If just one, press 1)");
                        string genAmountInput = Console.ReadLine();
                        bool genAmountTrue = int.TryParse(genAmountInput, out genAmount);
                        if (genAmount == 0)
                        {
                            genAmount++;
                        }
                        genAmountStore = genAmount;
                        Console.WriteLine(genAmount);
                    }
                    Console.WriteLine("Generate card? Y/N");
                    while (char.ToLower(Console.ReadKey().KeyChar) == 'y')
                    {
                        for (genAmount = genAmountStore; genAmount > 0; genAmount--)
                        {
                            if (RRchosen == "RR")
                            {
                                playerCardTypeChoiceLength = random.Next(0, playerCardTypeChoiceArray.Length);
                                playerCardTypeChoice = (playerCardTypeChoiceArray[playerCardTypeChoiceLength]);

                            }
                            if (playerChoiceRandom == "Random")
                            {
                                playerChoiceLength = random.Next(0, archetypeArray.Length);
                                playerChoice = (archetypeArray[playerChoiceLength]);
                            }
                            card = CardTypeChoice(playerCardTypeChoice, choice);
                            prefixLength = random.Next(choice.prefix.Length);
                            affixLength = random.Next(card.affix.Length);
                            effectsLength = random.Next(choice.effects.Length);
                            Console.WriteLine();
                            Console.WriteLine($"{playerChoice} {choice.prefix[prefixLength]} {card.affix[affixLength]} ({playerCardTypeChoice})");
                            Console.WriteLine($"Cost: {card.points}");
                            if (card.hp != 0)
                            {
                                Console.WriteLine($"Health: {card.hp}");
                            }
                            if (card.dm != 0)
                            {
                                Console.WriteLine($"Damage: {card.dm}");
                            }
                            if (card.eff != 0)
                            {
                                Console.WriteLine($"Effect Power: {Trigger.TriggerEffString} {choice.effects[effectsLength]} {card.eff}");
                            }
                            if (genAmount == 1)
                            {
                                Console.WriteLine("Again? Y/N");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Please press Y to generate another card.");
                }
            } while (char.ToLower(Console.ReadKey().KeyChar) == 'y');

        }

        static Archetype archetypeChoice(string playerChoice)
        {
            switch (playerChoice)
            {
                case "Goblin":
                    return goblin;
                case "Human":
                    return human;
                case "Elf":
                    return elf;
                case "Random":
                    return random;
                default:
                    return null;
            }

        }

        static Archetype goblin = new Archetype
        {
            pointsLow = 2,
            pointsHigh = 7,
            hpLow = 1,
            hpHigh = 3,
            dmLow = 1,
            dmHigh = 4,
            effLow = 1,
            effHigh = 4,
            effects = new string[] { "Rally", "Summon", "Bolt" },
            prefix = new string[] { "Gnarled", "Squalid", "Gross", "Grotesque", "Wicked", "Aggravated", "Goblin", "Impish", }
        };

        static Archetype human = new Archetype
        {
            pointsLow = 2,
            pointsHigh = 10,
            hpLow = 2,
            hpHigh = 6,
            dmLow = 2,
            dmHigh = 5,
            effLow = 1,
            effHigh = 4,
            effects = new string[] { "Charge", "Rally", "Heal" },
            prefix = new string[] { "Burly", "Royal", "Tinkering", "Curious", "Hungry", "Loyal", "Human", "Angry" }
        };

        static Archetype elf = new Archetype
        {
            pointsLow = 2,
            pointsHigh = 7,
            hpLow = 2,
            hpHigh = 5,
            dmLow = 1,
            dmHigh = 3,
            effLow = 2,
            effHigh = 6,
            effects = new string[] { "Heal", "Summon", "Mill" },
            prefix = new string[] { "Regal", "Slender", "Elvish", "Inquisitive", "Faerie", "Pixie", "Winged", "Fae", "Entish", "Satyr" }

        };

        static Archetype random = new Archetype
        {
            pointsLow = 1,
            pointsHigh = 11,
            hpLow = 1,
            hpHigh = 11,
            dmLow = 1,
            dmHigh = 10,
            effLow = 1,
            effHigh = 10,
            effects = new string[] { "Rally", "Summon", "Bolt", "Charge", "Mill", "Heal" },
            prefix = new string[] { "Grob", "Gnarled", "Squalid", "Gross", "Grotesque", "Wicked", "Aggravated", "Burly", "Royal", "Tinkering", "Curious", "Hungry", "Loyal", "Regal", "Slender", "Elvish", "Inquisitive", "Faerie", "Pixie" }


        };


        static CardType CardTypeChoice(string playerCardTypeChoice, Archetype choice)
        {


            Random random = new Random();

            CardType card = new CardType();

            switch (playerCardTypeChoice)
            {
                case "RNA":
                    card.hp = random.Next(choice.hpLow, choice.hpHigh);
                    card.dm = random.Next(choice.dmLow, choice.dmHigh);
                    card.points = ((card.hp + card.dm) / 2);
                    card.affix = new string[] { "Runt", "Brute", "Soldier", "Peasant", "Hunter", "Agent", "Farmer", "Miner", "Tribal", "Mob", "Borg" };
                    return card;
                case "RA":
                    card.hp = random.Next(choice.hpLow, choice.hpHigh);
                    card.dm = random.Next(choice.dmLow, choice.dmHigh);
                    card.eff = random.Next(choice.effLow, choice.effHigh) / 3 * 2;
                    card.points = ((card.hp + card.dm + card.eff) / 2);
                    Trigger.TriggerEffString = "";
                    card.effTrigger = random.Next(0, 3);
                    if (card.effTrigger == 1)
                    {
                        Trigger.TriggerEffInt = random.Next(Enum.GetNames(typeof(Trigger.TriggerEff)).Length);
                        Trigger.TriggerEffString = Trigger.TriggerDesc[Trigger.TriggerEffInt];
                        card.eff = (card.eff / 2);
                    }
                    card.affix = new string[] { "Officer", "Crusader", "Marshal", "Captain", "Wizard", "Spellslinger", "Count", "Demon", "Fiend", "Imp", "Mage", "Witch", "Dryad"};
                    return card;
                case "RE":
                    card.eff = random.Next(choice.effLow, choice.effHigh) / 3 * 2;
                    card.points = (card.eff / 2);
                    Trigger.TriggerEffString = "";
                    card.effTrigger = random.Next(0, 2);
                    if (card.effTrigger == 1)
                    {
                        Trigger.TriggerEffInt = random.Next(Enum.GetNames(typeof(Trigger.TriggerEff)).Length);
                        Trigger.TriggerEffString = Trigger.TriggerDesc[Trigger.TriggerEffInt];
                        card.eff = (card.eff / 3);
                        while (card.eff == 0 && card.points == 0)
                        {
                            card.eff++;
                            card.points++;
                        }
                        while (card.eff == 0)
                        {
                            card.eff++;
                        }
                    }
                    while (card.eff == 0)
                    {
                        card.eff++;
                    }
                    card.affix = new string[] { "Bolt", "Chant", "Spell", "Trick", "Enchantment", "Essence", "Arc", "Rush", "Strike", "Gambit" };
                    return card;
                default:
                    return null; 
            }


        }

    }
}
