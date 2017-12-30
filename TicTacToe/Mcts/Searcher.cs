using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Mcts
{
    public abstract class Searcher<TGameState, TNode>
        where TGameState : IGameState<TGameState, TNode>
        where TNode : INode<TNode>, new()
    {
        protected readonly Random _random = new Random();

        public void Test(TNode rootNode, TGameState rootState, int iterations)
        {
            var path = new List<TNode>();

            for (int i = 0; i < iterations; i++)
            {
                var gameState = rootState.Copy();

                var node = SelectPromisingNodes(path, gameState, rootNode);

                if (!gameState.IsGameFinished(out int score))
                {
                    ExpandNode(gameState, node);

                    node = node.Children.First();
                    gameState.Play(node);
                    path.Add(node);

                    score = SimulateRandomPlayout(gameState);
                }

                BackPropogation(path, score, rootState.WhiteToMove);
            }
        }

        public TNode SelectPromisingNodes(List<TNode> path, TGameState gameState, TNode node)
        {
            path.Clear();
            path.Add(node);

            while (node.Children != null)
            {
                node = node.SelectPromisingNode();
                gameState.Play(node);
                path.Add(node);
            }
            return node;
        }

        public void ExpandNode(TGameState gameState, TNode node)
        {
            node.Children = gameState.FindMoves();
            node.Children.Shuffle(_random);
        }

        public void BackPropogation(List<TNode> path, int score, bool whiteToMove)
        {
            foreach (var node in path)
            {
                node.NumVisits++;

                if (!whiteToMove)
                    node.Score += score;
                else
                    node.Score -= score;

                whiteToMove = !whiteToMove;
            }
        }

        public int SimulateRandomPlayout(TGameState gameState)
        {
            int score;
            while (!gameState.IsGameFinished(out score))
            {
                gameState.RandomPlay(_random);
            }

            return score;
        }


    }
}
