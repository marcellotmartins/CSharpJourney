using System;
using System.Collections.Generic;

class TextBasedRPG
{
    static void Main()
    {
        // Game variables
        int playerHealth = 100;
        int playerGold = 0;
        List<string> inventory = new List<string>();
        Random random = new Random();
        
        Console.WriteLine("Welcome to the Text-Based RPG Adventure!");
        Console.WriteLine("Your journey begins...\n");

        // Game loop
        while (playerHealth > 0)
        {
            Console.WriteLine("You are exploring the forest...");
            System.Threading.Thread.Sleep(1500);

            // Encounter event
            int encounter = random.Next(1, 4); // 1 to 3

            switch (encounter)
            {
                case 1: // Monster encounter
                    Console.WriteLine("A wild monster appears!");
                    int monsterHealth = random.Next(20, 50);

                    while (monsterHealth > 0 && playerHealth > 0)
                    {
                        Console.WriteLine($"Monster HP: {monsterHealth} | Your HP: {playerHealth}");
                        Console.WriteLine("What do you want to do?");
                        Console.WriteLine("1. Attack\n2. Defend\n3. Run");
                        string choice = Console.ReadLine();

                        if (choice == "1") // Attack
                        {
                            int playerDamage = random.Next(10, 25);
                            int monsterDamage = random.Next(5, 15);
                            monsterHealth -= playerDamage;
                            playerHealth -= monsterDamage;
                            Console.WriteLine($"You dealt {playerDamage} damage to the monster.");
                            Console.WriteLine($"The monster dealt {monsterDamage} damage to you.");
                        }
                        else if (choice == "2") // Defend
                        {
                            int reducedDamage = random.Next(0, 5);
                            Console.WriteLine($"You defend and reduce incoming damage by {reducedDamage}.");
                            playerHealth -= Math.Max(random.Next(5, 15) - reducedDamage, 0);
                        }
                        else if (choice == "3") // Run
                        {
                            Console.WriteLine("You managed to escape!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. The monster attacks you!");
                            playerHealth -= random.Next(5, 15);
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
                    break;

                case 2: // Treasure encounter
                    Console.WriteLine("You found a treasure chest!");
                    int treasureGold = random.Next(20, 100);
                    Console.WriteLine($"You open it and find {treasureGold} gold.");
                    playerGold += treasureGold;
                    inventory.Add("Treasure");
                    break;

                case 3: // Rest encounter
                    Console.WriteLine("You find a peaceful clearing and rest for a while.");
                    int healthRecovered = random.Next(10, 30);
                    playerHealth = Math.Min(playerHealth + healthRecovered, 100);
                    Console.WriteLine($"You recovered {healthRecovered} HP.");
                    break;
            }

            // Display stats
            Console.WriteLine($"\nYour stats: HP = {playerHealth}, Gold = {playerGold}");
            Console.WriteLine("Inventory: " + string.Join(", ", inventory));
            Console.WriteLine("-----------------------------------------");

            // Check if player wants to continue
            Console.WriteLine("Do you want to keep exploring? (yes/no)");
            string continueGame = Console.ReadLine().ToLower();
            if (continueGame != "yes")
            {
                Console.WriteLine("You decide to end your adventure.");
                break;
            }
        }

        // Game over
        Console.WriteLine("\nGame Over. Thanks for playing!");
        Console.WriteLine($"Final Stats: HP = {playerHealth}, Gold = {playerGold}");
        Console.WriteLine("Inventory: " + string.Join(", ", inventory));
    }
}