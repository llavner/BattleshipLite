
using BattleshipLite;
using BattleshipLiteLibrary;
using BattleshipLiteLibrary.Models;

ConsoleMessages.WelcomeMessage();

PlayerModel activePlayer = ConsoleLogic.CreatePlayer("Player 1");

PlayerModel opponent = ConsoleLogic.CreatePlayer("Player 2");

PlayerModel winner = null;

do
{
    ConsoleLogic.DisplayShotGrid(activePlayer);

    ConsoleLogic.RecordPlayerShot(activePlayer, opponent);

    bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

    if (doesGameContinue == true)
    {
        // Swap positions, using tuple .Net 7+
        (activePlayer, opponent) = (opponent, activePlayer);
    }
    else
    {
        winner = activePlayer;
    }

    ConsoleLogic.IdentifyWinner(winner);


} while (winner == null);

Console.ReadLine();

