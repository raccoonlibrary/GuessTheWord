// Guess the Word -> variant of hangman
// (User gives one letter at a time and if it is correct, the letters will be shown. (E.g. _ _ _ A _ _ A _ S)

// 1. Give the option to show the leaderboard before playing and after playing
//    1.1. Leaderboard is sorted by score
// 2. Ask for a username
//    2.1. Register to leaderboard
//    2.2. Duplicate names are allowed but empty names are not
// 3. Load words to guess from an external file
//    3.1. Words are sorted by their difficulty into 3 levels
// 4. Loop once through all 3 levels: Easy, Medium, Hard
//    4.1. Pick one random word per level
//        4.1.1. Six attempts at inputting a letter per level
//    4.2. Show the word that has to be guessed in this format E.g. : T _ _ _ T
//        4.2.1. Ask for and add user input accordingly E.g. : T A _ O T
//        4.2.2. Show what letters were already guessed
//        4.2.3. Show remaining attempts per turn + adjust after losing an attempt 
//        4.2.4. Do not deduct attempts if the letter had already been guessed regardless of if the letter was correct or wrong
//    4.3. Calculate score
//        4.3.1.  10 points per attempt remaining -> 100 max per difficulty
//    4.4. Display game over
//        4.4.1.  Show if the game was won
//        4.4.2.  Show score
//    4.5. Save usernames + scores of all games ever played

class Homework
{
    private static string currentUser;
    private static int currentScore;

    static void Main(string[] args) // Player can choose an option from a list
    {
        bool exit = false;

        do
        {
            OptionsMenu(); // List all options the user can choose
            string selectedOption = Console.ReadLine();
            switch (selectedOption) // Save the users input and compare to the list of options
            {
                case "1":
                case "Leaderboard":
                case "Show the Leaderboard for GuessTheWord":
                    SortAndDisplayLeaderboard(); // Option 1: Display the leaderboard
                    break;
                case "2":
                case "Play":
                case "Play GuessTheWord":
                    GuessTheWord(); // Option 2: Play GuessTheWord
                    break;
                case "3":
                case "Exit":
                    exit = true; // Option 3: Exit the program
                    break;
                default:
                    exit = true; // Default: (Any other input) Exit the program
                    break;
            }
        } while (!exit); // Loop until exit = true
    }

    private static void OptionsMenu() // List all options the user can choose
    {
        Console.WriteLine("\nPlease select an option.");
        Console.WriteLine("Option 1 - Show the Leaderboard for GuessTheWord");
        Console.WriteLine("Option 2 - Play GuessTheWord");
        Console.WriteLine("Option 3 - Exit\n");
    }

    private static void SortAndDisplayLeaderboard() // Sort the leaderboard by score and display all names + scores
    {
        string filepath = "leaderboard.txt"; // Relevant .txt file, define path to the leadeboard file


        var sortedLines = File.ReadAllLines(filepath) // Read all lines from the file
            .Select(line => // Project all lines from the file into a new form ->
            {
                var parts = line.Split(','); // Split each line into 2 partitions
                return new
                {
                    User = parts[0].Trim(), // First partition = user -> trim to remove leading & trailing whitespaces
                    Score = int.Parse(parts[1].Trim()) // Second partition = score -> trim to remove leading & trailing whitespaces
                };
            })
            .OrderByDescending(entry => entry.Score) // Sort the lines by score (highest first)
            .Select(entry => $"{entry.User} , {entry.Score}"); // Blueprint for the format of each line

        File.WriteAllLines(filepath, sortedLines); // Write the newly formatted lines into the file

        Console.WriteLine("\nLeaderboard:");
        Console.WriteLine(File.ReadAllText(filepath)); // List the contents of the file
    }

    private static void RegisterPlayer() // Register the current user's name to a static string, check that the name is not empty
    {
        Console.WriteLine("\nPlease enter your name:");
        currentUser = Console.ReadLine();
        currentScore = 0;

        if (string.IsNullOrEmpty(currentUser)) // Check that the input is not empty
        {
            Console.WriteLine("Name cannot be empty, please register a name.");
            RegisterPlayer(); // Loop until name is not empty
        }
    }

    private static void GuessTheWord() // Main method for the game GuessTheWord
    {
        RegisterPlayer(); // Register the current player
        Console.WriteLine($"Successfully registered {currentUser}, Welcome to Guess The Word!");

        var allPossibleWords = LoadWordsFromFile();

        for (int difficultyIndex = 0; difficultyIndex <= 2; difficultyIndex++)
        {
            string currentLevel;
            if (difficultyIndex == 0) currentLevel = "EASY";
            else if (difficultyIndex == 1) currentLevel = "MEDIUM";
            else currentLevel = "HARD";
            Console.WriteLine($"\nLevel {difficultyIndex}: {currentLevel}\n");

            var random = new Random();
            string wordToGuess = allPossibleWords[difficultyIndex][random.Next(allPossibleWords[difficultyIndex].Count)].ToUpper(); // Pick a random word from the file

            int currentAttemptCount = 10; // Ten attempts allowed per level
            char[] hiddenWord = new char[wordToGuess.Length];
            for (int i = 0; i < hiddenWord.Length; i++) hiddenWord[i] = '_';

            List<char> guessedLetters = new List<char>();
            bool isGameWon = false;

            while (currentAttemptCount > 0 && !isGameWon)
            {
                Console.WriteLine($"Word to guess: {new string(hiddenWord)}");
                Console.WriteLine($"Guessed letters: {string.Join(", ", guessedLetters)}");
                Console.WriteLine($"Lives remaining: {currentAttemptCount}\n");

                Console.Write("Please enter your guess (just a single letter): ");
                string input = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(input) || input.Length != 1 || !char.IsLetter(input[0])) // When user inputs anything but a single letter
                {
                    Console.WriteLine("Invalid input! Please enter just a single letter.\n");
                    continue;
                }

                char guessedLetter = input[0];

                if (guessedLetters.Contains(guessedLetter))
                {
                    Console.WriteLine("You already guessed that letter!\n");
                    continue;
                }

                guessedLetters.Add(guessedLetter);

                if (wordToGuess.Contains(guessedLetter))
                {
                    Console.WriteLine("Correct guess!\n");
                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuess[i] == guessedLetter)
                        {
                            hiddenWord[i] = guessedLetter;
                        }
                    }
                }

                else
                {
                    Console.WriteLine("Wrong guess!\n");
                    currentAttemptCount--;
                }

                if (new string(hiddenWord) == wordToGuess)
                {
                    isGameWon = true;
                }
            }

            int score;

            if (isGameWon)
            {
                score = currentAttemptCount * 10;
                currentScore += score;
                Console.WriteLine($"Congratulations! You guessed the word: {wordToGuess}\n");
                Console.WriteLine($"Level {difficultyIndex} success! Current score: {currentScore}\n");
            }

            else
            {
                score = 0;
                currentScore += score;
                Console.WriteLine($"Game over! The word was: {wordToGuess}");
                Console.WriteLine($"Level {difficultyIndex} failure! Current score: {currentScore}\n");
            }
        }

        SaveScoreToLeaderboard(); // Save player name and score to leaderboard
        OfferToPlayAgain(); // Offer option to play again
    }

    static public List<List<string>> LoadWordsFromFile() // Reads the wordstoguess.txt, splits the words into additional lists based upon difficulty, returns the list of lists
    {
        string filepath = "wordstoguess.txt"; // Relevant .txt file, define path to the wordstoguess file
        // File HAS to be in this format:
        // DIFFICULTY : word1 ,  word2 , word3 , ...
        // Otherwise the program assumes that e.g. "word4, word5" is a full word
        // -> Softlocks the game because neither "," nor " " is an allowed input


        var difficultyLevel = new List<List<string>> // List (of a list) to store our difficulty levels -> so that in the following, the words can be sorted into them
        {
            new List<string>(), // difficultyLevel[0] = easy
            new List<string>(), // difficultyLevel[1] = medium
            new List<string>()  // difficultyLevel[2] = hard
        };

        foreach (var line in File.ReadAllLines(filepath))
        {
            var partitions = line.Split(" : "); // Partitions have to be separated by :
            if (partitions.Length != 2) continue; // check that there are only 2 "partitions" so only DIFFICULTY : word1 , word2 , ...

            string difficulty = partitions[0]; // First partition = EASY / MEDIUM / HARD
            var words = partitions[1].Split(" , "); // Second partition = list of words = word1 , word2 , ...

            if (difficulty == "EASY") difficultyLevel[0].AddRange(words); // Add all items from "words" into difficultyLevel[0] (EASY)
            else if (difficulty == "MEDIUM") difficultyLevel[1].AddRange(words); // Add all items from "words" into difficultyLevel[1] (MEDIUM)
            else if (difficulty == "HARD") difficultyLevel[2].AddRange(words); // Add all items from "words" into difficultyLevel[2] (HARD)
        }

        return difficultyLevel;
    }

    private static void SaveScoreToLeaderboard() // Save the current user's name and score to the leaderboard
    {
        string filepath = "leaderboard.txt"; 


        File.AppendAllText(filepath, $"{currentUser} , {currentScore}\n"); // Add this blueprint of current username and current score to the leaderboard.txt
        Console.WriteLine("\nScore successfully registered!"); // Show message that the score was registered to the leaderboard
    }

    private static void OfferToPlayAgain() // Give the user the option to play again
    {
        Console.WriteLine("Do you want to play again? (Yes/No)");
        string playAgainInput = Console.ReadLine();
        if (playAgainInput.ToUpper() == "YES")
        {
            GuessTheWord();
        }
        else
        {
            Console.WriteLine("Thank you for playing!");
            return;
        }
    }
}