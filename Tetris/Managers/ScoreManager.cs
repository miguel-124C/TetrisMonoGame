using Tetris.Events;
using Tetris.Models;

namespace Tetris.Managers
{
    public class ScoreManager
    {
        private readonly GameState GameState;
        public ScoreManager(GameState gameState)
        {
            GameState = gameState;
            gameState.GameBoard.OnLinesCleared += CalculateScore;
        }

        private void CalculateScore(int amountLines)
        {
            GameState.AddLines(amountLines);

            int basePoints = amountLines switch
            {
                1 => 100,
                2 => 300,
                3 => 500,
                4 => 1200,
                _ => 0
            };

            if (basePoints > 0)
                GameState.AddScore(basePoints * GameState.Level);
            
            if (amountLines == 4)
                GameEvents.TriggerTetris4Lines();
            else if ( amountLines >= 1)
                GameEvents.TriggerLineClear();
        }
    }
}
