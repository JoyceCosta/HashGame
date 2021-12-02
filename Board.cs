using System;
using System.Text;

namespace HashGame
{   
    // Items that can be part of the board.
    enum boardItem 
    {
        X,
        O,
        EMPTY // Represents the empty space.
    }
    class Board
    {
        // Game matrix.
        private BoardItem[,] matrix = new BoardItem[3, 3];

        // Constructor.
        public Board() 
        {
            // Initialize matrix positions with EMPTY.
            for (int i = 0; i < 3; i++) 
            {
                for (int j = 0; j < 3; j++) 
                {
                    matrix[i, j] = boardItem.EMPTY;
                }
            }
        }

        // Returns the formatted board.
        public string GetFormattedBoard() 
        {
            // Use a StringBuilder to avoid string concatenation.
            StringBuilder sb = new StringBuilder(); 

            for (int i = 0; i < 3; i++) 
            {
                for (int j = 0; j < 3; j++) 
                {
                    // Converts a BoardItem to a char to display on screen.
                    sb.Append(" ").Append(ConvertBoardItemToChar(matrix[i, j]));
                    
                    if (j < 2) 
                    {
                        sb.Append(" | ");
                    }
                } 

                sb.AppendLine();

                if (i < 2) 
                {
                    sb.Append("------------").AppendLine();
                }
            } 

            return sb.ToString();
        } 

        // Converts a BoardItem to a char.
        private char ConvertBoardItemToChar(boardItem boardItem) 
        {
            switch (boardItem) 
            {
                case boardItem.X: return 'X';
                case boardItem.O: return 'O';
                default: return ' ';
            }
        } 

        // Checks if the game is over.
        public bool IsFinished(out boardItem? /*(question mark to be able to assign null to winnerBoardItem)*/ winnerBoardItem) 
        {
            winnerBoardItem = CheckSequence();

            if (winnerBoardItem != null) 
            {
                // If there is a sequel, the game is over.
                // winnerBoardItem will contain the winning item.
                return true;
            } 

            // If there are no straights, it checks if the board can still receive moves.
            for (int i = 0; i <3; i++) 
            {
                for (int j = 0; j < 3; j++) 
                {
                    if (matrix[i, j] == boardItem.EMPTY) 
                    {
                        return false;
                    }
                }
            }
        }

    }
}