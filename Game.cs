using System;


namespace HashGame
{   
    // Controls the execution of the game.
    class Game
    {
        // Board.
        private Board board = new Board(); 

        // Players of the game (2).
        private Player[] players = new Player[2]; 

        // Active player index (starts with -1).
        private int activePlayerIndex = -1; 

        // Inicia o jogo. 
        public void Play() 
        {
            // Muda as cores do console. 
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear(); 

            // Reads player names via console.
            ReadPlayerNames(); 

            // Game loop, which runs while the game is not finished.
            BoardItem? winnerBoardItem;
            while (!board.IsFinished(out winnerBoardItem)) 
            {
                Console.Clear(); 

                // Shows the board. 
                ShowBoard(); 

                // Get the next player.
                Player activePlayer = NextPlayer();

                Console.ForegroundColor = activePlayer.Color;
                Console.WriteLine("Current player: {0}", activePlayer.Name);

                while (true) 
                {
                    // Requests the move.
                    Console.Write("\nEnter the move: ");
                    string play = Console.ReadLine();

                    try 
                    {
                        // Process the move. If processing works, exits the loop.
                        ProcessPlay(play, activePlayer);
                        break;
                    } 
                    catch (PlayException e) 
                    {
                        // In case of exception, it shows the error and asks for the move again.
                        Console.WriteLine("Error: {0}", e.Message);
                    }
                }

            } 

            // Arriving here, the game is over. 

            Console.Clear(); 

            // Shows how the board looked after the game.
            ShowBoard(); 

            Console.WriteLine("The game is over!\n"); 

            Player winnerPlayer = null;
            if (winnerBoardItem != null) 
            {
                // If winnerBoardItem is not null, then someone else has won the game.
                if (players[0].BoardItem == winnerBoardItem) 
                {
                    // Player 1 won the game. 
                    winnerPlayer = players[0];
                } 
                else 
                {
                    // Player 2 won the game. 
                    winnerPlayer = players[1];
                } 

                Console.WriteLine("The winner is player {0}! Congratulations!", winnerPlayer.Name);
            } 
            else 
            {
                // Se winnerBoardItem for null, ninguém ganhou.
                Console.WriteLine("There was no winner this time!");
            }
        } 

        // Reads the names of the players.
        private void ReadPlayerNames() 
        {
            string player1Name;
            while (true) 
            {
                // Requests the name of the player.
                Console.Write("Player 1 name: ");
                player1Name = Console.ReadLine(); 

                if (string.IsNullOrWhiteSpace(player1Name)) 
                {
                    // If is empty, requests again. 
                    Console.WriteLine("Player 1 name is invalid\n");
                } 
                else 
                {
                    // The name is valid. 
                    break;
                }
            } 

            string Player2Name; 
            while (true) 
            {
                // Requests the name of the player.
                Console.Write("Player 2 name: ");
                Player2Name = Console.ReadLine(); 

                if (string.IsNullOrWhiteSpace(Player2Name)) 
                {
                    // If is empty, requests again. 
                    Console.WriteLine("Player 1 name is invalid\n");
                } 
                else if (player1Name.ToUpper() == Player2Name.ToUpper()) 
                {
                    // If it is the same as the name of the first player, request again.
                    Console.WriteLine("Players cannot have the same name\n");
                } 
                else 
                {
                    // The name is valid. 
                    break;
                }
            } 

            // Creats new players. 
            players[0] = new Player(player1Name, ConsoleColor.Green, BoardItem.X);
            players[1] = new Player(Player2Name, ConsoleColor.White, BoardItem.O);
        } 

        // Shows the board. 
        private void ShowBoard()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(board.GetFormattedBoard());
        } 

        // Switch to the next player. 
        private Player NextPlayer() 
        {
            // Read the next index as if the list were circular. 
            activePlayerIndex = (activePlayerIndex + 1) % 2; 

            // Returns the player associated with the new index. 
            return players[activePlayerIndex];
        } 

        // Processes the move made by the player. 
        private void ProcessPlay(string play, Player player) 
        {
            int row;
            int col; 

            try 
            {
                // Makes the move parse. If parse doesn't work, throw exception. 
                if (!int.TryParse(play.Substring(0, 1), out row) || !int.TryParse(play.Substring(1, 1), out col)) 
                {
                    throw new PlayException("The given play is invalid");
                } 

                // Check whether the row and column are between 0 and 2. 
                if (row < 0 || row > 2 || col < 0 || col > 2) 
                {
                    throw new PlayException("Indices used are out of the the allowed range");
                } 

                // Makes the move. 
                player.Play(board, row, col);
            } 
            catch (ArgumentOutOfRangeException e) 
            {
                // Throws exception if any Substring() error occurs. 
                throw new PlayException("The play provided is invalid", e);
            }
        }
    }
}