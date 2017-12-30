using System;
using System.Collections.Generic;

namespace TicTacToe.Mcts
{
    public interface IGameState<TGameState, TNode> where TGameState : IGameState<TGameState, TNode>
    {
        bool WhiteToMove {get;set;}

        TNode[] FindMoves();
        void Play(TNode node);
        void RandomPlay(Random rnd);
        TGameState Copy();
        bool IsGameFinished(out int score);
    }
}