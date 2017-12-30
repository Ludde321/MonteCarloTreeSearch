using System;
using System.Collections.Generic;

namespace TicTacToe.Mcts
{
    public interface IGameState
    {
        bool WhiteToMove {get;set;}

        INode[] FindMoves();
        void Play(INode node);
        void RandomPlay(Random rnd);
        IGameState Copy();
        bool IsGameFinished(out int score);
    }
}