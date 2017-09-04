using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Minesweeper_game
{
    enum State
    {
        Zero = 0,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,

        Opened = 90,

        Mine = 99,
    };

    class Game
    {
        static int[] xPos = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
        static int[] yPos = new int[] { -1, -1, -1, 0, 1, 1, 1, 0 };

        static int c_openedBlockCount;
        static int c_flagBlockCount;

        private GameBoard m_gameBoard;

        public Game()
        {
            m_gameBoard = new GameBoard();
        }

        public void Initialize()
        {
            State [,]m_gameBoardData = new State[Setting.Height, Setting.Width];

            c_openedBlockCount = 0;
            c_flagBlockCount = 0;

            Random rand = new Random();

            int w, h;
            for (int i = 0; i < Setting.MineCount;)
            {
                w = rand.Next(Setting.Width);
                h = rand.Next(Setting.Height);

                if (m_gameBoardData[h, w] != State.Mine)
                {
                    m_gameBoardData[h,w] = State.Mine;
                    for (int j = 0; j < 8; j++)
                    {
                        int tx = w + xPos[j], ty = h + yPos[j];
                        if (0 <= tx && tx <  Setting.Width)
                            if (0 <= ty && ty < Setting.Height)
                                if (m_gameBoardData[ty, tx] != State.Mine)
                                    m_gameBoardData[ty, tx]++;
                    }
                    i++;
                }
            }

            m_gameBoard.Initialize(m_gameBoardData);
        }

        public GameBoard GetGameBoard()
        {
            return m_gameBoard;
        }

        public static void IncreaseOpenedBlockCount()
        {
            c_openedBlockCount++;
            CheckClear();
        }

        public static void IncreaseFlagBlockCount()
        {
            c_flagBlockCount++;
            CheckClear();
        }

        public static void DecreaseFlagBlockCount()
        {
            c_flagBlockCount--;
            CheckClear();
        }

        public static void CheckClear()
        {
            if(c_flagBlockCount+c_openedBlockCount == Setting.Width * Setting.Height)
            {
                MessageBox.Show("Clear");
            }
        }
    }
}