class Player {
    public string? name;
    public int score;
    public int bsScore;
}

class Program {
    public static Player plr1 = new Player(); //i dont remember why i placed these outside of main originally
    public static Player plr2 = new Player();
    public static Player[] winnerLoser = [plr1, plr2];
    public static bool[] gamesPlayed = new bool[5];

    static void Main(string[] args) {
        Random random = new Random();
        if (random.Next(0,2) == 0)
            winnerLoser = [plr2, plr1];

        GetPlayerName(plr1, "player 1");
        GetPlayerName(plr2, "player 2");
        Console.Clear();
    
        bool gameLoop = true;
        while (gameLoop) {
            Console.WriteLine($"\nScores:\n{plr1.name} - {plr1.score}\n{plr2.name} - {plr2.score}");
            Console.Write($"\nMenu:\n1 - Number Guessing Game\n2 - Multiplication Game\n3 - Spelling Game\n4 - Battleship Game\n5 - Rock Paper Siccors\n0 - escape...\n\nEnter a number to play the specified game {winnerLoser[1].name}: ");
            
            int choice;            
            if (!int.TryParse(Console.ReadLine(), out choice) || 0 > choice || choice > 5) {
                Console.Clear();   
                Console.WriteLine("Invalid selection!!");
                continue;
            }

            Console.Clear();
            if (choice != 0 && gamesPlayed[choice-1]) {
                Console.WriteLine($"Game {choice} already played!");
                continue;
            }

            switch (choice) {
                case 1:
                    winnerLoser = NumberGuessingGame.GameMain(winnerLoser[1], winnerLoser[0]);
                    break;
                case 2:
                    winnerLoser = MultiplicationGame.GameMain(winnerLoser[1], winnerLoser[0]);
                    break;
                case 3:
                    winnerLoser = SpellingGame.GameMain(winnerLoser[1], winnerLoser[0]);
                    break;
                case 4:
                    winnerLoser = BattleshipGame.GameMain(winnerLoser[1], winnerLoser[0]);
                    break;
                case 5:
                    winnerLoser = RockPaperSiccors.GameMain(winnerLoser[1], winnerLoser[0]);
                    break;

                case 0:
                    gameLoop = false;
                    continue;
                    
            }

            gamesPlayed[choice-1] = true;

            winnerLoser[0].score++;
            if (winnerLoser[0].score >= 2)
                gameLoop = false;

            Console.Clear();
            Console.WriteLine($"{winnerLoser[0].name} won the game!");
        }

        Console.WriteLine("Goodbye!");
        Thread.Sleep(2000);
    }

    static void GetPlayerName(Player plr, string plrType) {
        while (true) {
            Console.Write($"\nEnter {plrType}'s name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name)) {
                Console.WriteLine("Invalid player name!");
            } else {
                plr.name = name;
                return;
            }
        }
    }
}


class NumberGuessingGame() {
    public static int number;

    public static Player[] GameMain(Player plr1, Player plr2) {
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
        while (true) {
            Console.Write($"\nEnter a guess {plr.name}: ");
            
            int guess;
            if (!int.TryParse(Console.ReadLine(), out guess)) {
                Console.WriteLine("Invalid input!");
                continue;
            }

            if (guess == number)
                return true; //winner
            else if (guess > number)
                Console.WriteLine("Number is lower");
            else if (guess < number)
                Console.WriteLine("Number is higher");

            return false;
        }
    }
}


class MultiplicationGame() {
    public static Random random = new Random();

    public static Player[] GameMain(Player plr1, Player plr2) {
        while (true) {
            if (Guess(plr1))
                return [plr2, plr1];
            if (Guess(plr2))
                return [plr1, plr2];
        }
    }

    public static bool Guess(Player plr) {
        int num1 = random.Next(0, 101); //no constraints specified??
        int num2 = random.Next(0, 101);
        int total = num1 * num2;

        while (true) {
            Console.Write($"\n{plr.name}, what is {num1} * {num2}? ");
            
            int guess;
            if (!int.TryParse(Console.ReadLine(), out guess)) {
                Console.WriteLine("Invalid input!");
                continue;
            }

            if (guess == total)
                return false;
            else
                return true; //person lost
        }
    }
}


class SpellingGame() {
    public static Player[] GameMain(Player plr1, Player plr2) {    
        while (true) {
            if (Guess(plr1, plr2))
                return [plr1, plr2];
            if (Guess(plr2, plr1))
                return [plr2, plr1];
        }
    }

    public static bool Guess(Player plr1, Player plr2) {
        Console.Clear();
        string? word;
        while (true) {
            Console.Write($"Enter word to guess {plr1.name}: ");
            word = Console.ReadLine();
            
            Console.Clear();
            if (string.IsNullOrWhiteSpace(word)) {
                Console.WriteLine("Invalid word entered!");
                continue;
            }
            break;
        }
        word = word.ToLower();

        while (true) {
            Console.Write($"Enter spelling for word {plr2.name}: ");
            
            string? guess = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(guess)) {
                Console.Clear();
                Console.WriteLine("Invalid guess!");
                continue;
            }

            if (string.Equals(word, guess.ToLower()))
                return false;
            else
                return true; //person lost
        }
    }
}


class BattleshipGame() { //"bs"
    public static bool[] grid = new bool[25]; //is the grid meant to be 5x5

    public static Player[] GameMain(Player plr1, Player plr2) {
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
            Console.Write($"\nEnter location (1-25) {plr.name}: ");
            
            int guess;
            if (!int.TryParse(Console.ReadLine(), out guess) || 0 > guess-1 || guess-1 > 24) {
                Console.WriteLine("Invalid location guessed!");
                continue;
            } else if (grid[guess-1]) {
                Console.WriteLine("Hit!");
                
                grid[guess-1] = false;
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


class RockPaperSiccors() {
    public static Player[] GameMain(Player plr1, Player plr2) {
        while (true) {
            int s1 = Select(plr1);
            int s2 = Select(plr2);
            
            Console.Clear();
            if (s1 == s2) {
                Console.WriteLine("Draw!");
                Thread.Sleep(1000);
            } 
            //   R P S
            // R|- 1 2
            // P|2 - 1
            // S|1 2 -
            else if ((s1 == 1 && s2 == 3) || (s1 == 2 && s2 == 1) || (s1 == 3 && s2 == 2))
                return [plr1, plr2];
            else if ((s2 == 1 && s1 == 3) || (s2 == 2 && s1 == 1) || (s2 == 3 && s1 == 2))
                return [plr2, plr1];
        }
    }

    public static int Select(Player plr) {
        Console.Clear();
        while (true) {
            Console.Write($"\n{plr.name}'s turn\n\n1 - Rock\n2 - Paper\n3 - Siccors\nEnter selection: ");
            
            if (int.TryParse(Console.ReadLine(), out int input) && 1 <= input && input <= 3)
                return input;
            
            else { //the else is unnecessary but i put it here cause why not
                Console.Clear();
                Console.WriteLine("Invalid selection!");
            }
        }
    }
}
