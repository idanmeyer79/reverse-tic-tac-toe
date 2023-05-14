namespace game
{
    internal class GameManager
    {
        internal GameLogic GameCore;
        internal ConsoleIO UserInterface;

        public GameManager()
        {
            UserInterface = new ConsoleIO();
            int numOfPlayers = UserInterface.WelcomeUserAndGetNumOfPlayers();
            Player Player1 = UserInterface.GetPlayerDetailsFromUser('X');
            Player Player2;

            if (numOfPlayers == 1)
            {
                Player2 = new Player("Computer", 'O')
                {
                    IsComputer = true
                };
            }
            else
            {
                Player2 = UserInterface.GetPlayerDetailsFromUser('O');
            }

            int boardSize = UserInterface.GetBoardSizeFromPlayer();
            GameCore = new GameLogic(Player1, Player2, boardSize);
        }

        public void RunGame()
        {
            do
            {
                GameCore.SetGameForNewRound();

                while (!GameCore.IsRoundOver)
                {
                    UserInterface.DisplayBoard(GameCore.Board);
                    UserInterface.DisplayWhoseTurn(GameCore.CurrentPlayer.Name, GameCore.CurrentPlayer.Symbol);
                    PlayTurn();
                }
                UserInterface.DisplayTheFinalBoardAndSummary(GameCore);
            }
            while (UserInterface.DoesPlayerWantToPlayAnotherRound());
        }

        public void PlayTurn()
        {
            if (GameCore.CurrentPlayer.IsComputer)
            {
                GameCore.PlayComputerTurn();
            }
            else // Human player's turn
            {
                ApplyHumanPlayerTurn();
            }
        }
        public void ApplyHumanPlayerTurn()
        {
            bool isMoveApplied = false;
            do
            {
                bool doesPlayerWantToQuit = UserInterface.GetMoveFromPlayer(GameCore.Board.BoardSize, out int row, out int col);

                if (doesPlayerWantToQuit)
                {
                    GameCore.PrepareGameForQuitting();
                }
                else if (GameCore.isCellOnBoardNotEmpty(row, col))
                {
                    UserInterface.DisplayCellIsOccupiedMsg();
                }
                else
                {
                    GameCore.ApplyMove(row - 1, col - 1);
                    isMoveApplied = true;
                }
            }
            while (!isMoveApplied && !GameCore.IsRoundOver);
        }
    }
}
