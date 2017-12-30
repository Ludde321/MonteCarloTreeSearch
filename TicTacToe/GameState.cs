using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Mcts;

namespace TicTacToe
{
    public class GameState : IGameState<GameState, Node>
    {
        public bool WhiteToMove {get;set;}
        public ushort BlackStones;
        public ushort WhiteStones;

        public GameState()
        {
            WhiteToMove = true;
        }

        public GameState Copy()
        {
            return new GameState { BlackStones = BlackStones, WhiteStones = WhiteStones, WhiteToMove = WhiteToMove };
        }

        public void Play(Node node)
        {
            if (WhiteToMove)
                WhiteStones |= (ushort)(1 << node.StoneTo);
            else
                BlackStones |= (ushort)(1 << node.StoneTo);

            WhiteToMove = !WhiteToMove;
        }

        public Node[] FindMoves()
        {
            var moves = new List<Node>(9);
            for (byte i = 0; i < 9; i++)
            {
                if (((BlackStones | WhiteStones) & (1 << i)) == 0)
                    moves.Add(new Node { StoneTo = i });
            }
            return moves.ToArray();
        }

        public void RandomPlay(Random rnd)
        {
            var moves = FindMoves();
            var move = moves[rnd.Next(moves.Length)];
            Play(move);
        }

        public bool IsGameFinished(out int score)
        {
            // CheckForWhiteWin
            if (_winPatterns.Any(w => (WhiteStones & w) == w))
            {
                score = 1;
                return true;
            }
            // CheckForBlackWin
            if (_winPatterns.Any(w => (BlackStones & w) == w))
            {
                score = -1;
                return true;
            }
            // CheckIfDraw
            score = 0;
            return (BlackStones | WhiteStones) == 0x1ff;
        }

        private static readonly ushort[] _winPatterns = { 0x7, 0x7<<3, 0x7<<6,
            0x1|(0x1<<3)|(0x1<<6),
            0x2|(0x2<<3)|(0x2<<6),
            0x4|(0x4<<3)|(0x4<<6),
            0x1|(0x2<<3)|(0x4<<6),
            0x4|(0x2<<3)|(0x1<<6),
        };

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (byte i = 0; i < 9; i++)
            {
                if ((WhiteStones & (1 << i)) != 0)
                    sb.Append("W");
                else if ((BlackStones & (1 << i)) != 0)
                    sb.Append("B");
                else
                    sb.Append(".");

                if (i % 3 == 2)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}