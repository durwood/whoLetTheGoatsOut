# whoLetTheGoatsOut
Like Minesweeper but with goats

GOAL OF KATA:

The purpose of this Kata is to explore refactoring an existing project to support multiple display targets.

We start with a fully working console version of "minesweeper."  This version was written with no notion of being able to support multiple displays. There are 3 main objects: The Game object handles collecting user input and looping; the board object handles all of the game logic and board state; and the board is comprised of Cells which can either be hidden, marked, or revealed and can have contents.  The board and cell objects know how to draw themselves to the console.

FYI, cells in the board are stored as a two-dimensional C# array which means they will be in row-major order (i.e. all the elements of the first row come first) and will be referenced by "cell[row, col]".

Our customer wants to leverage the popularity of goats to give a new take on minesweeper. Instead of mines, the board contains fenced in goats. The player has to find all the goats without releasing any of them by tearing down the fences which contain no goats. The game will be re-written using Microsoft Windows Winforms. All of the resources and a sample board setup are provided.

Your job is to refactor the game logic to continue to support the console version of bombsweeper while writing a fully-working version of WhoLetTheGoatsOut in Winform. While it is not strictly necessary for this kata, when planning your strategy you might want to consider how a web-based version might also be written.







