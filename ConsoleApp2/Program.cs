using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            string[] archetypeArray = new string[] { "Goblin", "Human", "Elf" };
            string[] playerCardTypeChoiceArray = new string[] { "RNA", "RA", "RE" };
            Trigger trigger = new Trigger();
            do
            {
                int genAmount = 1;
                int genAmountStore = 1;

                Console.WriteLine();
                Console.WriteLine("Please choose an archetype; Goblin, Human, Elf or Random.");
                string playerChoice = Console.ReadLine();

                bool stringValid = false;
                do
                {
                    stringValid = StringValid(playerChoice);
                    while (!stringValid)
                    {
                        Console.WriteLine("Please Choose Again: Goblin, Human, Elf or Random.");
                        playerChoice = Console.ReadLine();
                        stringValid = StringValid(playerChoice);
                    }
                } while (!stringValid);
                bool playerChoiceRandom = playerChoice == "Random";

                Archetype choice = ArchetypeAssign(playerChoice, archetypeArray, playerChoiceRandom, out playerChoice);

                Console.WriteLine();
                Console.WriteLine("Please choose the card you wish to generate: RNA (Random No Abilities), RA (Random Abilities), RE (Random Effect) or RR (Random Card Type)");
                string playerCardTypeChoice = Console.ReadLine();

                bool cardTypeValid = false;
                do
                {
                    cardTypeValid = CardTypeValid(playerCardTypeChoice);
                    while (!cardTypeValid)
                    {
                        Console.WriteLine("Please Choose Again: RNA (Random No Abilities), RA (Random Abilities), RE (Random Effect) or RR (Random Card Type)");
                        playerCardTypeChoice = Console.ReadLine();
                        cardTypeValid = CardTypeValid(playerCardTypeChoice);
                    }
                } while (!cardTypeValid);
                bool playerCardTypeChoiceRandom = playerCardTypeChoice == "RR";


                CardType card = CardTypeAssign(playerCardTypeChoice, choice, playerCardTypeChoiceArray, playerCardTypeChoiceRandom, out playerCardTypeChoice);

                Console.WriteLine("How many iterations would you like to generate of this card at once? (If just one, press 1)");
                string genAmountInput = Console.ReadLine();
                bool genAmountTrue = int.TryParse(genAmountInput, out genAmount);
                if (genAmount == 0)
                {
                    genAmount++;
                }
                genAmountStore = genAmount;
                Console.WriteLine($"Generating {genAmount} cards with current perameters.");

                Console.WriteLine($"Press Y to generate Card(s)");
                while (char.ToLower(Console.ReadKey().KeyChar) == 'y')
                {
                    for (genAmount = genAmountStore; genAmount > 0; genAmount--)
                    {
                        choice = ArchetypeAssign(playerChoice, archetypeArray, playerChoiceRandom, out playerChoice);
                        card = CardTypeAssign(playerCardTypeChoice, choice, playerCardTypeChoiceArray, playerCardTypeChoiceRandom, out playerCardTypeChoice);
                        CardGeneration(card, choice, trigger, playerChoice, playerCardTypeChoice, genAmount);
                    }
                    Console.WriteLine("Press Y to generate with same settings");
                }
                Console.WriteLine("Press Y to start generator again");
            } while (char.ToLower(Console.ReadKey().KeyChar) == 'y');

        }
        static string CardGeneration(CardType card, Archetype choice, Trigger trigger, string playerChoice, string playerCardTypeChoice, int genAmount)
        {
            Random random = new Random();
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
            return $"{playerChoice} {choice.prefix[prefixLength]} {card.affix[affixLength]} ({playerCardTypeChoice})";
        }

        static Archetype ArchetypeAssign(string playerChoice, string[] archetypeArray, bool playerChoiceRandom, out string playerChoiceReturn)
        {
            Random random = new Random();
            Archetype choice = ArchetypeChoice(playerChoice);
            if (playerChoiceRandom == true)
            {
                int playerChoiceLength = random.Next(0, archetypeArray.Length);
                playerChoice = (archetypeArray[playerChoiceLength]);
                choice = ArchetypeChoice(playerChoice);
            }
            playerChoiceReturn = playerChoice;
            return choice;

        }
        static Archetype ArchetypeChoice(string playerChoice)
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

        static bool StringValid(string playerChoice)
        {
            string[] validArchetypes = new string[] { "Goblin", "Elf", "Human", "Random" };
            return Array.Exists(validArchetypes, element => element == playerChoice);
        }

        static bool CardTypeValid(string playerCardTypeChoice)
        {
            string[] validCardTypes = new string[] { "RNA", "RA", "RE", "RR" };
            return Array.Exists(validCardTypes, element => element == playerCardTypeChoice);
        }
        static CardType CardTypeAssign(string playerCardTypeChoice, Archetype choice, string[] playerCardTypeChoiceArray, bool playerCardTypeChoiceRandom, out string playerCardTypeChoiceReturn)
        {
            Random random = new Random();
            CardType card = CardTypeChoice(playerCardTypeChoice, choice);
            if (playerCardTypeChoiceRandom == true)
            {
                int playerCardTypeChoiceLength = random.Next(0, playerCardTypeChoiceArray.Length);
                playerCardTypeChoice = (playerCardTypeChoiceArray[playerCardTypeChoiceLength]);
                card = CardTypeChoice(playerCardTypeChoice, choice);
            }
            playerCardTypeChoiceReturn = playerCardTypeChoice;
            return card;

        }
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
                    while (card.eff == 0)
                    {
                        card.eff++;
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

        static Archetype goblin = new Archetype
        {
            pointsLow = 2,
            pointsHigh = 7,
            hpLow = 1,
            hpHigh = 3,
            dmLow = 1,
            dmHigh = 4,
            effLow = 1,
            effHigh = 7,
            effects = new string[] { "Rally", "Summon", "Bolt" },
            prefix = new string[] { "Grob", "Gnarled", "Squalid", "Gross", "Grotesque", "Wicked", "Aggravated", "Goblin", "Impish", }
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
            effHigh = 8,
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
            effLow = 4,
            effHigh = 10,
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


    }
}
