using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace Hangman
{
    internal class Program
    {
        public static readonly Random rng = new Random();

        const int MAXIMUM_GUESSES = 5;
        const int YOU_LOSE_GALLOWS = 6;
        const char USER_YES_CHOICE = 'Y';

        static void Main(string[] args)
        {
            List<String> wordList = new List<string>()
            {
             "APPLE",
             "ORANGE",
             "LEMON",
             "LIME",
             "PEAR",
             "STRAWBERRY",
             "KIWI",
             "GRAPEFRUIT",
             "BLUEBERRY",
             "CHERRY",
             "RASBERRY",
            };

            //List of user guesses that changes based on correct guesses. It starts with all underscores.
            List<Char> correctLetters = new List<char>();

            //Gallows drawings
            List<String> userGallows = new List<string>()
            {
                //0
                 ("\t          _____     \n\t         |     |     \n\t         |     |     \n\t" +
                "         |          \n\t         |          \n\t         |          \n\t         |          \n\t_________|___________\n"),
                //1
                  ("\t          _____     \n\t         |     |     \n\t         |     |     \n\t" +
                "         |     O     \n\t         |          \n\t         |          \n\t         |          \n\t_________|___________\n"),
                //2
                   ("\t          _____     \n\t         |     |     \n\t         |     |     \n\t" +
                "         |     O     \n\t         |     |     \n\t         |          \n\t         |          \n\t_________|___________\n"),
                //3
                   ("\t          _____     \n\t         |     |     \n\t         |     |     \n\t" +
                "         |     O     \n\t         |    /|     \n\t         |          \n\t         |          \n\t_________|___________\n"),
                //4
                ("\t          _____     \n\t         |     |     \n\t         |     |     \n\t" +
                "         |     O     \n\t         |    /|\\     \n\t         |          \n\t         |          \n\t_________|___________\n"),
                //5
                ("\t          _____     \n\t         |     |     \n\t         |     |     \n\t" +
                "         |     O     \n\t         |    /|\\     \n\t         |    /      \n\t         |          \n\t_________|___________\n"),
                //6
                ("\t          _____     \n\t         |     |     \n\t         |     |     \n\t" +
                "         |     O     \n\t         |    /|\\     \n\t         |    / \\     \n\t         |  You lose!        \n\t_________|___________\n")
            };

            char playAgain = USER_YES_CHOICE;

            do
            {
                int listPosition = Program.rng.Next(wordList.Count);
                string wordToGuess = wordList[listPosition];
                Console.WriteLine("Today we are going to play Hangman!");
                Console.WriteLine("Guess individual letters to figure out what the word is.");
                Console.WriteLine("For every incorrect guess 1 limb will appear.");
                Console.WriteLine("When the figure is complete, you are out of guesses and you lose.\n");
                Console.WriteLine(userGallows[0]);

                correctLetters.Clear();
                foreach (char letter in wordToGuess)
                {
                    correctLetters.Add('_');
                    Console.Write(" _");
                }

                for (int guessNumber = 0; guessNumber <= MAXIMUM_GUESSES; guessNumber++)
                {
                    Console.WriteLine("\n\n");
                    Console.WriteLine("What letter would you like to guess?\n");
                    // Allows user to enter a guess
                    char userGuess = Char.ToUpper(Console.ReadKey(true).KeyChar);

                    //Checks if user's guess is right
                    if (wordToGuess.Contains(userGuess))
                    {
                        //If player gets one of the letters, then that guess isn't counted against their maximum alotted guesses
                        guessNumber = guessNumber - 1;
                        //Replacing the initial set of underscores with the new set including the correctly guessed letters
                        for (int letterIndex = 0; letterIndex < wordToGuess.Length; letterIndex++)
                        {
                            if (userGuess == wordToGuess[letterIndex])
                            {
                                correctLetters[letterIndex] = userGuess;
                            }
                        }
                    }

                    Console.Clear();
                    Console.WriteLine(userGallows[guessNumber + 1]);
                    foreach (char letter in correctLetters)
                    {
                        Console.Write($" {letter}");
                    }

                    if (!correctLetters.Contains('_'))
                    {
                        Console.WriteLine("\n\n");
                        Console.WriteLine("Great job! You got the word!\n");
                        break;
                    }
                }

                if (correctLetters.Contains('_'))
                {
                    Console.Clear();
                    Console.WriteLine(userGallows[YOU_LOSE_GALLOWS]);
                    foreach (char letter in correctLetters)
                    {
                        Console.Write($" {letter}");
                    }
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Better luck next time.");
                }

                Console.WriteLine("\n");
                Console.WriteLine($"Would you like to play again?({USER_YES_CHOICE} or press any other key to exit the program)\n");
                playAgain = Char.ToUpper(Console.ReadKey(true).KeyChar);
                Console.Clear();

            } while (playAgain == USER_YES_CHOICE);
            Console.WriteLine("Thanks for playing!");
        }
    }
}
