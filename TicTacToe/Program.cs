using System;
using System.Diagnostics;
using TicTacToe.Mcts;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var searcher = new MonteCarloSearcher();

            var rootState = new GameState();

            while(!rootState.IsGameFinished(out int score))
            {
                var sw = Stopwatch.StartNew();
                var rootNode = new Node();
                var bestMove = searcher.GetBestMove(rootNode, rootState, 100000);

                Console.WriteLine($"Time used: {sw.Elapsed}");

                rootState.Play(bestMove);

                Console.WriteLine(rootState);

                // byte stoneTo;
                // do
                // {
                //     int c = Console.Read();
                //     stoneTo = (byte)(c - '0');
                    
                // } while(stoneTo >= 9);

                // rootState.Play(new Move { StoneTo = (byte)stoneTo});
            }


        }
    }
}
