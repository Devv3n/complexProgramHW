﻿//cs8618// cs8604 cs8600 cs8602 cs0028 ignoreeeeeeeeeee these warnings please :)
//#pragma warning disable CS8618
#pragma warning disable CS8604
#pragma warning disable CS8602
#pragma warning disable CS8600
#pragma warning disable CS0028

class Player {
    public string? name;
    public int score;
    public int bsScore;
}

class Program {
    public static Player plr1 = new Player();
    public static Player plr2 = new Player();
    public static Player[] winnerLoser = [plr2, plr1];

    static void Main(string[] args) {
        Console.Write("Enter player 1's name: ");
        plr1.name = Console.ReadLine();
        Console.Write("Enter player 2's name: ");
        plr2.name = Console.ReadLine();
    
        bool gameLoop = true;
        while (gameLoop) {
            Console.WriteLine($"\nScores:\n{plr1.name} - {plr1.score}\n{plr2.name} - {plr2.score}");
            Console.Write($"\nMenu:\n1 - Number Guessing Game\n2 - Multiplication Game\n3 - Spelling Game\n4 - Battleship Game\n5 - the no game\n0 - escape...\n\nEnter a number to play the specified game {winnerLoser[1].name}: ");
            int choice = int.Parse(Console.ReadLine());

            Console.Clear();
            switch (choice) {
                case 1:
                    winnerLoser = NumberGuessingGame.Main(winnerLoser[1], winnerLoser[0]);
                    break;
                case 2:
                    winnerLoser = MultiplicationGame.Main(winnerLoser[1], winnerLoser[0]);
                    break;
                case 3:
                    winnerLoser = SpellingGame.Main(winnerLoser[1], winnerLoser[0]);
                    break;
                case 4:
                    winnerLoser = BattleshipGame.Main(winnerLoser[1], winnerLoser[0]);
                    break;
                
                case 5:
                    Console.WriteLine("haha no.");
                    continue;

                case 0:
                    gameLoop = false;
                    continue;
                default:
                    Console.WriteLine("Invalid selection!!");
                    continue;
            }

            Console.Clear();
            Console.WriteLine($"{winnerLoser[0].name} won the game!");

            winnerLoser[0].score++;
            if (winnerLoser[0].score >= 2)
                gameLoop = false;
        }
    }
}


class NumberGuessingGame() {
    public static int number;

    public static Player[] Main(Player plr1, Player plr2) {
        Random random = new Random();
        number = random.Next(0, 101);
        
        while (true) {
            if (Guess(plr1))
                return [plr1, plr2];
            if (Guess(plr2))
                return [plr2, plr1];
        }
    }

    public static bool Guess(Player plr) {
        Console.Write($"Enter a guess {plr.name}: ");
        int guess = int.Parse(Console.ReadLine());

        if (guess == number)
            return true;
        else if (guess > number)
            Console.WriteLine("Number is lower");
        else if (guess < number)
            Console.WriteLine("Number is higher");

        return false;
    }
}


class MultiplicationGame() {
    public static Random random = new Random();

    public static Player[] Main(Player plr1, Player plr2) {
        while (true) {
            if (Guess(plr1))
                return [plr2, plr1];
            if (Guess(plr2))
                return [plr1, plr2];
        }
    }

    public static bool Guess(Player plr) {
        int num1 = random.Next(0, 101);
        int num2 = random.Next(0, 101);
        int total = num1 * num2;

        Console.Write($"{plr.name}, what is {num1} * {num2}? ");
        int guess = int.Parse(Console.ReadLine());

        if (guess == total)
            return false; //correct
        else
            return true; //wrong
    }
}


class SpellingGame() {
    public static Player[] Main(Player plr1, Player plr2) {    
        while (true) {
            if (Guess(plr1, plr2))
                return [plr1, plr2];
            if (Guess(plr2, plr1))
                return [plr2, plr1];
        }
    }

    public static bool Guess(Player plr1, Player plr2) {
        Console.Write($"Enter word to guess {plr1.name}: ");
        string word = Console.ReadLine();
        Console.Clear();

        Console.Write($"Enter spelling for word {plr2.name}: ");
        if (string.Equals(word, Console.ReadLine().ToLower()))
            return false; //correct
        else
            return true; //wrong
    }
}


class BattleshipGame() {
    public static bool[] grid = new bool[25];

    public static Player[] Main(Player plr1, Player plr2) {
        Random random = new Random();
        int shipCount = 0;

        while (shipCount < 3) {
            int location = random.Next(0, 25);

            if (grid[location] == false) {
                grid[location] = true;
                shipCount++;
            }
        }

        plr1.bsScore = 0;
        plr2.bsScore = 0;

        while (true) {
            if (Guess(plr1))
                return [plr1, plr2];
            if (Guess(plr2))
                return [plr2, plr1];
        }
    }

    public static bool Guess(Player plr) {
        while (true) {
            Console.Write($"Enter location (1-25) {plr.name}: ");
            int guess = int.Parse(Console.ReadLine()) - 1;

            if (0 > guess || guess > 24) {
                Console.WriteLine("Invalid location guessed!");
                continue;
            } else if (grid[guess]) {
                Console.WriteLine("Hit!");
                
                grid[guess] = false;
                plr.bsScore++;

                if (plr.bsScore >= 2)
                    return true; //winner
            } else {
                Console.WriteLine("Miss!");
            }
            
            return false;
        }
    }
}


//game 5 goes here......