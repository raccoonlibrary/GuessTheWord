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
    static void Main(string[] args) // Player can choose an option from a list
    {

    }

    private static void OptionsMenu() // List all options the user can choose
    {

    }

    private static void SortAndDisplayLeaderboard() // Sort the leaderboard by score and display all names + scores
    {

    }

    private static void RegisterPlayer() // Register the current user's name to a static string, check that the name is not empty
    {

    }

    private static void GuessTheWord() // Main method for the game GuessTheWord
    {

    }

    private static void SaveScoreToLeaderboard() // Save the current user's name and score to the leaderboard
    {

    }

    private static void OfferToPlayAgain() // Give the user the option to play again
    {

    }
}