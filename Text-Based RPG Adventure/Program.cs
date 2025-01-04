using System;
using System.Collections.Generic;

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

        Console.WriteLine("Welcome to the Text-Based RPG Adventure!");
        Console.WriteLine("Your journey begins...\n");

        while (playerHealth > 0)
        {
            Console.WriteLine("You are exploring the forest...");
            System.Threading.Thread.Sleep(1500);

            EncounterType encounter = (EncounterType)random.Next(1, 4);
            switch (encounter)
            {
                case EncounterType.Monster:
                    HandleMonsterEncounter(ref playerHealth, ref playerGold, inventory, random);
                    break;
                case EncounterType.Treasure:
                    HandleTreasureEncounter(ref playerGold, inventory, random);
                    break;
                case EncounterType.Rest:
                    HandleRestEncounter(ref playerHealth, random);
                    break;
            }

            DisplayStats(playerHealth, playerGold, inventory);

            if (!AskToContinue())
            {
                Console.WriteLine("You decide to end your adventure.");
                break;
            }
        }

        GameOver(playerHealth, playerGold, inventory);
    }

    static void HandleMonsterEncounter(ref int playerHealth, ref int playerGold, List<string> inventory, Random random)
    {
        Console.WriteLine("A wild monster appears!");
        int monsterHealth = random.Next(20, 50);

        while (monsterHealth > 0 && playerHealth > 0)
        {
            Console.WriteLine($"Monster HP: {monsterHealth} | Your HP: {playerHealth}");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Attack\n2. Defend\n3. Run");

            string choice = Console.ReadLine() ?? string.Empty;
            switch (choice)
            {
                case "1":
                    int playerDamage = random.Next(10, 25);
                    int monsterDamage = random.Next(MinDamage, MaxDamage);
                    monsterHealth -= playerDamage;
                    playerHealth -= monsterDamage;
                    Console.WriteLine($"You dealt {playerDamage} damage to the monster.");
                    Console.WriteLine($"The monster dealt {monsterDamage} damage to you.");
                    break;
                case "2":
                    int reducedDamage = random.Next(0, 5);
                    Console.WriteLine($"You defend and reduce incoming damage by {reducedDamage}.");
                    playerHealth -= Math.Max(random.Next(MinDamage, MaxDamage) - reducedDamage, 0);
                    break;
                case "3":
                    Console.WriteLine("You managed to escape!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. The monster attacks you!");
                    playerHealth -= random.Next(MinDamage, MaxDamage);
                    break;
            }
        }

        if (monsterHealth <= 0)
        {
            Console.WriteLine("You defeated the monster!");
            int goldFound = random.Next(10, 50);
            playerGold += goldFound;
            Console.WriteLine($"You found {goldFound} gold.");
            inventory.Add("Monster Loot");
        }
    }

    static void HandleTreasureEncounter(ref int playerGold, List<string> inventory, Random random)
    {
        Console.WriteLine("You found a treasure chest!");
        int treasureGold = random.Next(20, 100);
        Console.WriteLine($"You open it and find {treasureGold} gold.");
        playerGold += treasureGold;
        inventory.Add("Treasure");
    }

    static void HandleRestEncounter(ref int playerHealth, Random random)
    {
        Console.WriteLine("You find a peaceful clearing and rest for a while.");
        int healthRecovered = random.Next(10, 30);
        playerHealth = Math.Min(playerHealth + healthRecovered, MaxHealth);
        Console.WriteLine($"You recovered {healthRecovered} HP.");
    }

    static void DisplayStats(int playerHealth, int playerGold, List<string> inventory)
    {
        Console.WriteLine($"\nYour stats: HP = {playerHealth}, Gold = {playerGold}");
        Console.WriteLine("Inventory: " + (inventory.Count > 0 ? string.Join(", ", inventory) : "Empty"));
        Console.WriteLine("-----------------------------------------");
    }

    static bool AskToContinue()
    {
        Console.WriteLine("Do you want to keep exploring? (yes/no)");
        string continueGame = Console.ReadLine()?.ToLower() ?? "no";
        return continueGame == "yes";
    }

    static void GameOver(int playerHealth, int playerGold, List<string> inventory)
    {
        Console.WriteLine("\nGame Over. Thanks for playing!");
        Console.WriteLine($"Final Stats: HP = {playerHealth}, Gold = {playerGold}");
        Console.WriteLine("Inventory: " + (inventory.Count > 0 ? string.Join(", ", inventory) : "Empty"));
    }

    enum EncounterType
    {
        Monster = 1,
        Treasure = 2,
        Rest = 3
    }
}
