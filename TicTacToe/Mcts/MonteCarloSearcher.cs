using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Extensions;

namespace TicTacToe.Mcts
{
    public class MonteCarloSearcher
    {
        protected readonly Random _random = new Random();

        public INode GetBestMove(INode rootNode, IGameState rootState, int iterations)
        {
            var path = new List<INode>();

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
            return rootNode.SelectPromisingNode();
        }

        public INode SelectPromisingNodes(List<INode> path, IGameState gameState, INode node)
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

        public void ExpandNode(IGameState gameState, INode node)
        {
            node.Children = gameState.FindMoves();
            node.Children.Shuffle(_random);
        }

        public void BackPropogation(List<INode> path, int score, bool whiteToMove)
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

        public int SimulateRandomPlayout(IGameState gameState)
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
