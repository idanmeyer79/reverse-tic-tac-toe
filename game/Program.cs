using System;

namespace game
{
    class Program
    {
        public static void Main()
        {
            
            Console.WriteLine("Welcome to Reverse Tic Tac Toe!");
            int numOfPlayers = ConsoleIo.GetNumOfPlayers();

            // Create the players
            Player player1 = ConsoleIo.GetPlayerDetailsFromUser('X');
            Player player2;

            if(numOfPlayers == 1)
            {
                player2 = new Player("Computer", 'O')
                              {
                                  IsComputer = true
                              };
            }
            else
            {
              player2 = ConsoleIo.GetPlayerDetailsFromUser('O');
            }

            int boardSize = ConsoleIo.GetBoardSizeFromPlayer();
            // Create the game
            Game game = new Game(player1, player2, boardSize);
            
            // Play the game
            while (game.IsGameOver == false)
            {
                while(game.IsRoundOver == false)
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    ConsoleIo.DisplayBoard(game.Board);

                    Player currentPlayer = game.CurrentPlayer;

                    if (currentPlayer.IsComputer == false)
                    {
                        // Human player's turn
                        
                        Console.WriteLine($"It's your turn, {currentPlayer.Name} ({currentPlayer.Symbol})");
                        int row, col;
                        ConsoleIo.GetMoveFromPlayer(game, out row, out col);
                        if(game.IsRoundOver)
                        {
                            break;
                        }

                        game.PlayTurn(row - 1, col - 1);
                    }
                    else
                    {
                        // Computer player's turn
                        Console.WriteLine($"It's the computer's turn ({currentPlayer.Symbol})");
                        game.PlayComputerTurn();
                    }
                }
                // Display the final board and winner (if any)
                Ex02.ConsoleUtils.Screen.Clear();
                ConsoleIo.DisplayBoard(game.Board);
                ConsoleIo.DisplaySummery(game);

                if(ConsoleIo.AskForUserToPlayNextRound() == false)
                {
                    game.IsGameOver = true;
                    break;
                }

                game.ResetGame();
            }
        }
    }
}
