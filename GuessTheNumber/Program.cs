using System;

class GuessTheNumber
{
    static void Main()
    {
        Random random = new Random();
        int numberToGuess = random.Next(1, 101);
        int userGuess = 0;
        int attempts = 0;

        Console.WriteLine("Bem-vindo ao jogo 'Adivinhe o Número'!");
        Console.WriteLine("Tente adivinhar o número que estou pensando entre 1 e 100.");
        Console.WriteLine("Digite seu palpite:");

        while (userGuess != numberToGuess)
        {
            try
            {
                string input = Console.ReadLine();
                userGuess = int.Parse(input);
                attempts++;

                if (userGuess > numberToGuess)
                {
                    Console.WriteLine("Muito alto! Tente novamente.");
                }
                else if (userGuess < numberToGuess)
                {
                    Console.WriteLine("Muito baixo! Tente novamente.");
                }
                else
                {
                    Console.WriteLine($"Parabéns! Você acertou o número em {attempts} tentativas.");
                }
            }
            catch
            {
                Console.WriteLine("Por favor, insira um número válido.");
            }
        }

        Console.WriteLine("Obrigado por jogar! Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }
}