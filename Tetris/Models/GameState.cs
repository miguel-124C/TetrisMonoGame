using System;
using System.Linq;
using Tetris.Enums;
using Tetris.Events;
using Tetris.Helpers;

namespace Tetris.Models
{
    public class GameState(Board gameBoard, PieceGenerator pieceGenerator)
    {
        public Board GameBoard { get; private set; } = gameBoard;
        private readonly PieceGenerator PieceGenerator = pieceGenerator;
        
        public event Action<int> OnScoreChanged;
        public event Action<int> OnLevelChanged;
        public event Action<int> OnLinesChanged;
        public event Action<Piece> OnNextPieceChanged;
        public event Action<GameStatus> OnStatusChanged;

        public event Action<double> OnChangedTimeDrop;

        public Piece CurrentPiece { get; private set; }

        private Piece _nextPiece;
        public Piece NextPiece
        {
            get => _nextPiece;
            private set
            {
                _nextPiece = value;
                OnNextPieceChanged?.Invoke(_nextPiece);
            }
        }

        private int _score;
        public int Score
        {
            get => _score;
            private set
            {
                _score = value;
                OnScoreChanged?.Invoke(_score);
            }
        }

        private int _level = 1;
        public int Level
        {
            get => _level;
            private set
            {
                _level = value;
                var timeDrop = GetTimeDrop();
                OnChangedTimeDrop?.Invoke(timeDrop);
                OnLevelChanged?.Invoke(_level);
                if (_level > 1)
                    GameEvents.TriggerLevelUp();
            }
        }

        private int _lines;
        public int Lines
        {
            get => _lines;
            private set
            {
                _lines = value;
                OnLinesChanged?.Invoke(_lines);
            }
        }

        private GameStatus _currentStatus = GameStatus.Menu;
        public GameStatus CurrentStatus
        {
            get => _currentStatus;
            private set
            {
                _currentStatus = value;
                OnStatusChanged?.Invoke(_currentStatus);
            }
        }

        public void StartGame()
        {
            GameBoard.Clear();
            Score = 0;
            Level = 1;
            Lines = 0;
            PieceGenerator.ClearBag();

            ChangePiece();
            CurrentStatus = GameStatus.Countdown;
            
            GameEvents.TriggerTetrisThemePlay();
            GameEvents.TriggerGameStarted();
        }

        public void TogglePause()
        {
            if (CurrentStatus == GameStatus.Playing)
            {
                CurrentStatus = GameStatus.Paused;
                GameEvents.TriggerTetrisThemeTogglePause();
            }
            else if (CurrentStatus == GameStatus.Paused)
                CurrentStatus = GameStatus.Countdown;
            else if (CurrentStatus == GameStatus.Countdown)
            {
                CurrentStatus = GameStatus.Playing;
                GameEvents.TriggerTetrisThemeTogglePause();
            }
        }

        public void TriggerGameOver() => CurrentStatus = GameStatus.GameOver;

        public void ChangePiece()
        {
            CurrentPiece = _nextPiece ?? PieceGenerator.GetNextPiece();
            NextPiece = PieceGenerator.GetNextPiece();
            GameBoard.GhostCoords = CalculateGhostPiece();
        }

        public void AddScore(int amount) => Score += amount;

        public void AddLines(int amount)
        {
            Lines += amount;

            int newLevel = (Lines / 10) + 1;
            if (newLevel > Level && Level < 15)
                Level = newLevel;
        }

        public Coordinate[] CalculateGhostPiece()
        {
            Coordinate[] currentCoords = CurrentPiece.Cords;
            Coordinate[] ghostCoords = currentCoords; // Nuestro clon temporal

            bool collision = false;

            while (!collision)
            {
                Coordinate[] nextDrop = [..ghostCoords.Select(c => new Coordinate(c.X, c.Y + 1))];

                if (GameBoard.HasCollision(nextDrop))
                    collision = true;
                else
                    ghostCoords = nextDrop;
            }

            return ghostCoords;
        }

        public double GetTimeDrop() => Math.Pow((0.8 - ((_level - 1) * 0.007)), (_level - 1));
    }
}