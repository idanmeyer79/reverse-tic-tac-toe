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
                    Console.WriteLine(ConsoleIo.DisplayBoard(game.Board));

                    Player currentPlayer = game.CurrentPlayer;

                    if (currentPlayer.IsComputer == false)
                    {
                        // Human player's turn

                        Console.WriteLine($"It's your turn, {currentPlayer.Name} ({currentPlayer.Symbol})");

                        Console.Write($"Enter the row number of your move (1-{boardSize}): ");
                        int row = ConsoleIo.GetMoveFromPlayer(boardSize);
                        Console.Write($"Enter the col number of your move (1-{boardSize}): ");
                        int col = ConsoleIo.GetMoveFromPlayer(boardSize);

                        try
                        {
                            game.PlayTurn(row - 1, col - 1);
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("That cell is already occupied. press any key to try again\n");
                            Console.ReadKey();
                        }

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
                Console.WriteLine(ConsoleIo.DisplayBoard(game.Board));
                ConsoleIo.DisplaySummery(game);

                if(ConsoleIo.AskForUserToPlayNextRound() == false)
                {
                    game.IsGameOver = true;
                }

                game.ResetGame();
            }
        }
    }
}
