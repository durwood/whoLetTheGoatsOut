# whoLetTheGoatsOut
Like Minesweeper but with goats


Stories:

X  1. Display 3x3 board with blocks
X  2. Display prompt to clear cell, replacing block with space
X  3. Add bomb to cell, clicking on it loses game.
X  4. clicking on non-bomb clears area up to hidden bombs
X  5. clicking on non-bomb clears area up to revealed adjacency counts
X  6. clicking on bomb and losing game reveals entire board with all bombs revealed.
X  7. Clicking on all non-bomb slots wins game and reveals board
   8. Add command-line option for windows version
X  9. Write out elapsed time at top and number of bombs remaining
x 10. When bomb exposed and board is revealed, reveal ONLY unmarked bombs, 
      leaving unrevealed squares unrevealed.
x 11. Highlight bomb which caused you to lose. Reverse display maybe?
x 12. Clicking on marked cell does nothing. Must unmark to reveal.

Goal of Kata:

The purpose of this Kata is to explore refactoring an existing project to support multiple display targets.

We start with a working console version of "minesweeper."  This version was written with no notion of being able to support multiple displays. There are 3 main objects: The Game object handles collecting user input and looping; the board object handles all of the game logic and board state; and the board is comprised of Cells which can either be hidden or revealed and can have contents.  The board and cell objects know how to draw themselves to the console.

Our customer wants to leverage the popularity of goats to give a new take on minesweeper. Instead of mines, the board contains fenced in goats. The player has to find all the goats without releasing any of them. The game will be re-written using Microsoft Windows Winforms. All of teh resources and a sample board setup are provided.

Your job is to refactor the game logic to continue to support the console version of bombsweeper while writing a fully-working version of WhoLetTheGoatsOut in Winform.







