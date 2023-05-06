using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game
{
    class Program
    {
        public static void Main()
        {
            
            Console.WriteLine("Welcome to Reverse Tic Tac Toe!");


            // Create the players
            Player humanPlayer = new Player("Human", 'X');
            Player computerPlayer = new Player("Computer", 'O')
            {
                IsComputer = true
            };

            int boardSize = ConsoleIo.GetBoardSizeFromPlayer();
            // Create the game
            Game game = new Game(humanPlayer, computerPlayer, boardSize);
            
            // Play the game
            while (!game.IsGameOver)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine(ConsoleIo.DisplayBoard(game.Board));

                Player currentPlayer = game.CurrentPlayer;

                if (currentPlayer.IsComputer == false)
                {
                    // Human player's turn

                    Console.WriteLine($"It's your turn, {humanPlayer.Name} ({humanPlayer.Symbol})");

                    Console.Write($"Enter the row number of your move (1-{boardSize}): ");
                    int row = ConsoleIo.GetMoveFromPlayer(boardSize);
                    Console.Write($"Enter the col number of your move (1-{boardSize}): ");
                    int col = ConsoleIo.GetMoveFromPlayer(boardSize);

                    try
                    {
                        game.PlayTurn(row - 1, col - 1);
                    }
                    catch(InvalidOperationException)
                    {
                        Console.WriteLine("That cell is already occupied. press any key to try again\n");
                        Console.ReadKey();
                    }
                    
                }
                else
                {
                    // Computer player's turn
                    Console.WriteLine($"It's the computer's turn ({computerPlayer.Symbol})");
                    game.PlayComputerTurn();
                }
            }

            // Display the final board and winner (if any)
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(ConsoleIo.DisplayBoard(game.Board));
            if (game.Winner == null)
            {
                Console.WriteLine("It's a tie!");
            }
            else
            {
                Console.WriteLine($"Congratulations, {game.Winner.Name} ({game.Winner.Symbol})! You won!");
            }
        }
    }

}
