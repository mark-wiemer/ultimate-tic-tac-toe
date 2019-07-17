# Ultimate Tic-Tac-Toe

I've made Ultimate Tic-Tac-Toe! [Download it from Google Play here](https://play.google.com/store/apps/details?id=com.Curdflappers.Ultimate_Tic_Tac_Toe)!

## Current Features

- Play the unique strategy game against friends or any of **several AIs** on one device!
  - The dumbest AIs barely know what they're doing, but the smartest AIs use **Monte Carlo Tree Search** to outwit their opponents every turn!
  - Maybe you want to relax? Watch two AIs duke it out on any difficulty! You might learn a thing or two...
- Easily **reset** the entire board, **preview** your next move, or **undo** the most recent move for a smooth gameplay experience!
  - Accidentally hit undo? Don't worry, I've got a **redo** button just for you!
- Learn how to play with an in-depth, animated **instructions** sequence!
  - Watch an entire game unfold before your eyes, every step annotated with help text!

## Planned Features

- Multithreaded artificial intelligence! Running in the background and out-thinking players constantly!
- Online functionality! I've never done anything with networks; it'd be a great learning experience!
- Sounds and animation! For added immersion.
- Custom pieces! Change colors and shapes to make the game match you.

## Changelog

## Instructions Update (0.9.0, 2017-08-27)

- Added "Instructions" scene
  - Walkthrough game mechanics and rules
  - Learn how to play and learn win conditions

### Bugfix Patch (0.8.2, 2017-07-07)

- Can no longer undo throughout the whole game, only once
  - Undo removes preview move or last confirmed move

Bugfixes:

- Fix undo glitch with Monte Carlo AI (MCTS AI)
  - Now possible to undo once in a row on games with MCTS AI
- Fix reset glitch with MCTS AI

### Status Patch (0.8.1, 2017-06-30)

- Centered Player panes on Settings scene
- Better organization of buttons on Game scene
- Status text
  - Display whose turn it is (using player names)
  - Display winner or tie when game is over

## Embrace the Overhaul Update (0.8.0, 2017-06-21)

- Completely rewrite architecture: MVC model with event support
- Better artificial intelligence
  - Monte Carlo Tree Search with variable timing (more time = more difficult)
  - Benchmark runs about 650 simulations per second on a blank game
- Settings scene
  - Removed unnecessary "Easy/Hard" buttons
  - Changed slider handles to circles
  - Choose how long to give the AI to think: 0 to 5 seconds
  - Change color scheme to black/white to match other scenes
  - View now matches previous settings when coming back to this scene
- Menu scene: Add animated game between two random AIs
- Changed coloring scheme, global game wins are now darker

Known bugs:

- Undo/redo/reset with Monte Carlo AI does not work

## Gameplay Update (0.7.0, 2017-05-06)

- Rule change
  - If a local game is over, that board is always disabled
  - Sending a player to a game-over board activates all other valid boards
- Added status bar
  - Gives info on the last turn (who made it, where it was)
  - Gives info on whose turn it is (X or O, AI or human)
- AI improvements
  - AI now looks some number of moves into the future (currently 5 by default)
  - AI now weighs sending its opponent to a completed board, allowing its opponent to play on any board

### Bugfix Patch and AI Update (0.6.2, 2017-04-26)

- AI now weighs value of winning a local board
- AI now weighs value of blocking opponent from winning local board

Bugfixes:

- Fix for game not recoloring when a tie occurred
- Fix for game not allowing undo when game was ended in single player
- Fix for undo being possible after AI previewed its move
- Other misc. bugfixes probably

### Heuristic Patch (0.6.1, 2017-04-17)

- Improved artificial intelligence
  - Working name of AI is now Little Hug. Little because it doesn't think ahead, so its mind is little.
  - Little Hug now considers its moves based on an heuristic that weighs whether each spot is a local corner, side, or center.

## AI Framework Update (0.6.0, 2017-04-10)

- Added Artificial Intelligence player!
  - Its working name is "Hug" (as its symbol is an 'O')
  - It moves randomly as of right now
  - Hug waits a certain amount of time to preview its move
  - Hug waits a bit more to confirm its move.
- Added Menu
  - Access One Player or Two Player game modes from the new simple menu
  - Access the Menu from the Game using the new "Menu" button

## Intuitive Update (0.5.0, 2017-04-02)

- Added redo functionality
  - Replay any confirmed move that you undo
  - Replay any number of moves that are undone
  - Once a new move is played, redo stack is cleared
    - Can only redo up to most recent new move
- Cleaned up the board image (now symmetrical)
- Animated reset
  - Pieces are removed every 0.1 seconds
  - Can be stopped by tapping reset again
- Reworked undo functionality
  - Clicking undo first undoes "preview" move
  - If no preview move, functions as before
- Reworked preview functionality
  - No longer highlights the next playable board(s)
  - Instead outlines them in the color of the next player

Android:

- Increased default quality to "fantastic" from "simple"

## Confirm Update (0.4.0, 2017-03-28)

- Added confirmation functionality!
  - Clicking a spot now previews the next move by highlighting the next active board
  - The clicked spot changes image to reflect the active player (as though the player has moved there)
  - The local board of the clicked spot may change color to reflect a new winner (if the previewed move wins the board)
  - The global board may change color to reflect the new winner (if the previewed move wins the entire game)
  - Click confirm to make your move official (don't worry, this can still be undone with "Undo")
- Rearranged UI for increased useability and readability

## Undo Update (0.3.0, 2017-03-25)

- Implemented undo functionality!
- Recolored completed boards a bit more to make difference between claimed & enabled, claimed & disabled, and unclaimed & enabled clearer
- Lots of backend restructuring that doesn't concern the end-user
- Recolored background to black to match board lines and also to look cooler of course

### Background Patch (0.2.1, 2017-03-14)

- Changed completed board background colors to make enabled board clearer

## Colors Update (0.2.0, 2017-03-14)

- Changed background board image to reflect traditional tic-tac-toe, visualize local/global boards easier
- Empty spaces are now invisible by default
- Highlighting empty spaces shows player color
- Enabled and completed local board now "lighten" to show that they are active
- Global board color resets upon reset
- Changed icon color to red for better contrast

Android:

- Switched to landscape left instead of landscape right

Bugfixes:

- Fixed #1 (local games can still be played even when global game is over)

### Android Patch (0.1.1, 2017-03-XX)

Android:

- Minor fixes (honestly not quite sure what)

## Initial Build (0.1.0, 2017-03-11)

- Create game of Ultimate Tic-Tac-Toe with red 'x's and blue 'o's
- Reset upon right click or click of reset button
- Highlight winner of local boards and global board

Found bugs:

- #1: Local games can still be played even when global game is over (global winner cannot change)
