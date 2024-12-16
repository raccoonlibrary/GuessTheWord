# GuessTheWord

A small guessing game akin to the classic game of hangman. 

Within this game you progress through three difficulty levels, in which you will guess a randomly selected word, all while being limited to 10 attempts per level.

## Features

**Leaderboard:** Display all players and their scores

**Play the game:** Play GuessTheWord

**3 Difficulty Levels:** Go through the 3 included difficulty levels EASY, MEDIUM and HARD

**Dynamic Scoring:** Points are earned based on the amount of attempts remaining per level

**Score Saving:** Your score is saved and displayed in a sorted manner within the leaderboard

**Replay option:** After a game you can choose to play again as a different user as many times as you want

## How to play

1. Start the game
   On startup, choose between 3 options:
   - Show the leaderboard
   - Play the game
   - Exit the program
2. Show the leaderboard
   - You can view the leaderboard with past players either before or after playing
3. Play the game
   - Register yourself with a username
   - Play through the 3 provided difficulty levels
   - See your total score after every level and after finishing the game
   - Play again if you wish to do so
4. Exit the game

## File Structure

The game relies on two text files within GuessTheWord/bin/Debug/net3.0

1. leaderboard.txt
   - Store the names and scores of all players
   - Sorted in descending order based on the score
   - The formatting within the file is **negligible** and a such can be written like this:
```
Ada , 270
Wesker, 30
Leon ,200
Jill,220
```
**Please note that every time the leaderboard is called upon to be displayed, the formatting within the file is adjusted and sorted**

2. wordstoguess.txt
   - Contains the words used within the game, sorted by difficulty
   - The formatting within the file is **non-negligible** and has to follow this blueprint
```
EASY : word1 , word2 , word3
MEDIUM : word1 , word2 , word3
HARD : word1 , word2 , word3
```







