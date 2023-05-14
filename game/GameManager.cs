namespace game
{
    internal class GameManager
    {
        private GameLogic m_GameCore;
        private ConsoleIO m_UserInterface;

        public GameManager()
        {
            m_UserInterface = new ConsoleIO();
            int numOfPlayers = m_UserInterface.WelcomeUserAndGetNumOfPlayers();
            Player player1 = m_UserInterface.GetPlayerDetailsFromUser('X');
            Player player2;

            if (numOfPlayers == 1)
            {
                player2 = new Player("Computer", 'O')
                {
                    IsComputer = true
                };
            }
            else
            {
                player2 = m_UserInterface.GetPlayerDetailsFromUser('O');
            }

            int boardSize = m_UserInterface.GetBoardSizeFromPlayer();
            m_GameCore = new GameLogic(player1, player2, boardSize);
        }

        public void RunGame()
        {
            do
            {
                m_GameCore.SetGameForNewRound();

                while (!m_GameCore.IsRoundOver)
                {
                    PlayTurn();
                }

                m_UserInterface.DisplayTheFinalBoardAndSummary(m_GameCore);
            }
            while (m_UserInterface.DoesPlayerWantToPlayAnotherRound());
        }

        public void PlayTurn()
        {
            if (m_GameCore.CurrentPlayer.IsComputer)
            {
                m_GameCore.PlayComputerTurn();
            }
            else
            {
                m_UserInterface.DisplayBoard(m_GameCore.Board);
                m_UserInterface.DisplayWhoseTurn(m_GameCore.CurrentPlayer.Name, m_GameCore.CurrentPlayer.Symbol);
                ApplyHumanPlayerTurn();
            }
        }
        public void ApplyHumanPlayerTurn()
        {
            bool isMoveApplied = false;
            do
            {
                bool doesPlayerWantToQuit = m_UserInterface.GetMoveFromPlayer(m_GameCore.Board.BoardSize, out int row, out int col);

                if (doesPlayerWantToQuit)
                {
                    m_GameCore.PrepareGameForQuitting();
                }
                else if (m_GameCore.IsCellOnBoardNotEmpty(row, col))
                {
                    m_UserInterface.DisplayCellIsOccupiedMsg();
                }
                else
                {
                    m_GameCore.ApplyMove(row - 1, col - 1);
                    isMoveApplied = true;
                }
            }
            while (!isMoveApplied && !m_GameCore.IsRoundOver);
        }
    }
}
