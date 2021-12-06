using System;
using System.Text;

namespace HashGame
{   
    // Items that can be part of the board.
    enum BoardItem 
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
                    matrix[i, j] = BoardItem.EMPTY;
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
        private char ConvertBoardItemToChar(BoardItem boardItem) 
        {
            switch (boardItem) 
            {
                case BoardItem.X: return 'X';
                case BoardItem.O: return 'O';
                default: return ' ';
            }
        } 

        // Checks if the game is over.
        public bool IsFinished(out BoardItem?  winnerBoardItem/*(question mark to be able to assign null to winnerBoardItem)*/) 
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
                    if (matrix[i, j] == BoardItem.EMPTY) 
                    {
                        return false;
                    }
                }
            } 

            return true;
        } 

        // Make a move. 
        public void Play(int row, int col, BoardItem boardItem) 
        {
            if (matrix[row, col] != BoardItem.EMPTY) 
            {
                // If the move has already taken place, throws the exception.
                throw new PlayException("This move has already been done.");
            } 

            // Assigns the item the position of the board.
            matrix[row, col] = boardItem;
        } 

        // Checks if there is a sequence of 3 items.
        private BoardItem? CheckSequence() 
        {
            if (matrix[0, 0] != BoardItem.EMPTY && matrix[0, 0] == matrix[0, 1] && matrix[0, 1] == matrix[0, 2])
			{
				return matrix[0, 0];
			}

			if (matrix[1, 0] != BoardItem.EMPTY && matrix[1, 0] == matrix[1, 1] && matrix[1, 1] == matrix[1, 2])
			{
				return matrix[1, 0];
			}

			if (matrix[2, 0] != BoardItem.EMPTY && matrix[2, 0] == matrix[2, 1] && matrix[2, 1] == matrix[2, 2])
			{
				return matrix[2, 0];
			}

			if (matrix[0, 0] != BoardItem.EMPTY && matrix[0, 0] == matrix[1, 0] && matrix[1, 0] == matrix[2, 0])
			{
				return matrix[0, 0];
			}

			if (matrix[0, 1] != BoardItem.EMPTY && matrix[0, 1] == matrix[1, 1] && matrix[1, 1] == matrix[2, 1])
			{
				return matrix[0, 1];
			}

			if (matrix[0, 2] != BoardItem.EMPTY && matrix[0, 2] == matrix[1, 2] && matrix[1, 2] == matrix[2, 2])
			{
				return matrix[0, 2];
			}

			if (matrix[0, 0] != BoardItem.EMPTY && matrix[0, 0] == matrix[1, 1] && matrix[1, 1] == matrix[2, 2])
			{
				return matrix[0, 0];
			}

			if (matrix[0, 2] != BoardItem.EMPTY && matrix[0, 2] == matrix[1, 1] && matrix[1, 1] == matrix[2, 0])
			{
				return matrix[0, 2];
			}

			return null;
        }

    }
}