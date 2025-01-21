using System;
using System.Collections.Generic;
using System.Linq;

class TextBasedRPG
{
    const int MaxHealth = 100;
    const int MinDamage = 5;
    const int MaxDamage = 15;

    static void Main()
    {
        int playerHealth = MaxHealth;
        int playerGold = 0;
        List<string> inventory = new List<string>();
        Random random = new Random();

        Console.WriteLine("Welcome to the Enhanced RPG Adventure!");
        Console.WriteLine("Brace yourself for a deeper, more challenging experience...\n");

        while (playerHealth > 0)
        {
            Encounter(random.Next(1, 5), ref playerHealth, ref playerGold, inventory, random);
            DisplayStats(playerHealth, playerGold, inventory);

            if (!AskToContinue()) break;
        }

        Console.WriteLine(playerHealth > 0 ? "You survived the adventure!" : "Your journey ends in defeat.");
        Console.WriteLine($"Final Gold: {playerGold}, Final Inventory: {string.Join(", ", inventory.DefaultIfEmpty("Empty"))}");
    }

    static void Encounter(int type, ref int health, ref int gold, List<string> inventory, Random random)
    {
        switch (type)
        {
            case 1: Fight(ref health, ref gold, inventory, random); break;
            case 2: Treasure(ref gold, inventory, random); break;
            case 3: Rest(ref health, random); break;
            case 4: MysteryBox(ref health, ref gold, inventory, random); break;
        }
    }

    static void Fight(ref int health, ref int gold, List<string> inventory, Random random)
    {
        Console.WriteLine("A fierce beast blocks your path!");
        int monsterHealth = random.Next(30, 70);
        while (monsterHealth > 0 && health > 0)
        {
            Console.WriteLine($"Monster: {monsterHealth} HP | You: {health} HP");
            Console.WriteLine("1. Attack  2. Defend  3. Use Item  4. Flee");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                int damage = random.Next(15, 30);
                monsterHealth -= damage;
                Console.WriteLine($"You strike for {damage} damage!");
                health -= random.Next(MinDamage, MaxDamage);
            }
            else if (choice == "2")
            {
                int block = random.Next(5, 15);
                health -= Math.Max(random.Next(MinDamage, MaxDamage) - block, 0);
                Console.WriteLine($"You block, reducing damage by {block}.");
            }
            else if (choice == "3" && inventory.Any(i => i.Contains("Potion")))
            {
                inventory.Remove("Potion");
                health = Math.Min(MaxHealth, health + 30);
                Console.WriteLine("You use a potion and recover 30 HP!");
            }
            else if (choice == "4")
            {
                Console.WriteLine("You retreat safely.");
                return;
            }
            else
            {
                Console.WriteLine("You hesitate and the monster strikes!");
                health -= random.Next(MinDamage, MaxDamage);
            }
        }

        if (monsterHealth <= 0)
        {
            int loot = random.Next(20, 50);
            gold += loot;
            inventory.Add("Beast Trophy");
            Console.WriteLine($"Victory! You collect {loot} gold and a Beast Trophy.");
        }
    }

    static void Treasure(ref int gold, List<string> inventory, Random random)
    {
        Console.WriteLine("You discover a hidden treasure chest!");
        if (random.Next(1, 3) == 1)
        {
            int loot = random.Next(50, 150);
            gold += loot;
            Console.WriteLine($"The chest contains {loot} gold!");
        }
        else
        {
            inventory.Add("Rare Gem");
            Console.WriteLine("Inside, you find a Rare Gem!");
        }
    }

    static void Rest(ref int health, Random random)
    {
        int recovery = random.Next(20, 40);
        health = Math.Min(MaxHealth, health + recovery);
        Console.WriteLine($"You find a safe haven and recover {recovery} HP.");
    }

    static void MysteryBox(ref int health, ref int gold, List<string> inventory, Random random)
    {
        Console.WriteLine("A mysterious figure offers you a sealed box. Will you open it? (yes/no)");
        if (Console.ReadLine()?.ToLower() == "yes")
        {
            int outcome = random.Next(1, 4);
            if (outcome == 1)
            {
                int curse = random.Next(10, 30);
                health -= curse;
                Console.WriteLine($"The box was cursed! You lose {curse} HP.");
            }
            else if (outcome == 2)
            {
                int blessing = random.Next(50, 100);
                gold += blessing;
                Console.WriteLine($"The box contains a blessing! You gain {blessing} gold.");
            }
            else
            {
                inventory.Add("Ancient Relic");
                Console.WriteLine("The box contains an Ancient Relic! A priceless artifact.");
            }
        }
        else
        {
            Console.WriteLine("You walk away, leaving its secrets untold.");
        }
    }

    static void DisplayStats(int health, int gold, List<string> inventory)
    {
        Console.WriteLine($"\nHP: {health}, Gold: {gold}, Inventory: {string.Join(", ", inventory.DefaultIfEmpty("Empty"))}");
        Console.WriteLine(new string('-', 30));
    }

    static bool AskToContinue()
    {
        Console.WriteLine("Continue your journey? (yes/no)");
        return Console.ReadLine()?.Trim().ToLower() == "yes";
    }
}
