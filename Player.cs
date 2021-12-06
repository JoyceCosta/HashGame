using System;

namespace HashGame
{   
    // Player of the game.
    class Player
    {   
        // Player name.
        public string Name { get; private set; }

        // Color that represents the player.
        public ConsoleColor Color { get; private set; }

        // Item associated with the player ('X' or 'O').
        public BoardItem BoardItem { get; private set; } 

        // Constructor.
        public Player(string name, ConsoleColor color, BoardItem boardItem) 
        {
            this.Name = name; 
            this.Color = color;
            this.BoardItem = boardItem;
        } 

        // Performs a move on the board.
        public void Play(Board board, int row, int col) 
        {
            board.Play(row, col, BoardItem);
        }
    }
}