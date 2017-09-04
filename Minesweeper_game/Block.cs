using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minesweeper_game
{
    class Block : Button
    {
        private static int[] xPos = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
        private static int[] yPos = new int[] { -1, -1, -1, 0, 1, 1, 1, 0 };

        private static ImageBrush closedBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/closedBlock.jpg", UriKind.Relative)));
        private static ImageBrush zeroBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/0.jpg", UriKind.Relative)));
        private static ImageBrush oneBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/1.jpg", UriKind.Relative)));
        private static ImageBrush twoBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/2.jpg", UriKind.Relative)));
        private static ImageBrush threeBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/3.jpg", UriKind.Relative)));
        private static ImageBrush fourBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/4.jpg", UriKind.Relative)));
        private static ImageBrush fiveBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/5.jpg", UriKind.Relative)));
        private static ImageBrush sixBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/6.jpg", UriKind.Relative)));
        private static ImageBrush sevenBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/7.jpg", UriKind.Relative)));
        private static ImageBrush eighteBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/8.jpg", UriKind.Relative)));
        private static ImageBrush successMine = new ImageBrush(new BitmapImage(new Uri("../../resource/successMine.jpg", UriKind.Relative)));
        private static ImageBrush failedMine = new ImageBrush(new BitmapImage(new Uri("../../resource/failedMine.jpg", UriKind.Relative)));
        private static ImageBrush clickMine = new ImageBrush(new BitmapImage(new Uri("../../resource/clickMine.jpg", UriKind.Relative)));
        private static ImageBrush flagBlock = new ImageBrush(new BitmapImage(new Uri("../../resource/flag.jpg", UriKind.Relative)));

        private State m_state;
        private bool m_isOpened;
        private bool m_isFlaged;
        private int m_col, m_row;

        public Block(State num,int col,int row) : base()
        {
            m_isOpened = false;
            m_state = num;
            m_col = col;
            m_row = row;

            Initialize();
        }

        public void Initialize()
        {
            this.Width = Setting.BlockWidth;
            this.Height = Setting.BlockHeight;

            this.Background= closedBlock;
        }

        public bool IsOpened()
        {
            return m_isOpened;
        }

        public void OpenMine()
        {
            if (!m_isOpened)
            {
                if (m_state == State.Mine)
                {
                    if (m_isFlaged)
                        this.Background = successMine;
                    else
                        this.Background = failedMine;
                }
            }
        }

        public State Open()
        {
            if (!m_isOpened&&!m_isFlaged)
            {
                switch (m_state)
                {
                    case State.Zero: this.Background = zeroBlock; break;
                    case State.One: this.Background = oneBlock; break;
                    case State.Two: this.Background = twoBlock; break;
                    case State.Three: this.Background = threeBlock; break;
                    case State.Four: this.Background = fourBlock; break;
                    case State.Five: this.Background = fiveBlock; break;
                    case State.Six: this.Background = sixBlock; break;
                    case State.Seven: this.Background = sevenBlock; break;
                    case State.Eight: this.Background = eighteBlock; break;
                    case State.Mine: this.Background = clickMine; break;
                }

                m_isOpened = true;
                Game.IncreaseOpenedBlockCount();

                if (m_state == State.Zero)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        int tx = m_col + xPos[j], ty = m_row + yPos[j];
                        if (0 <= tx && tx < Setting.Width)
                            if (0 <= ty && ty < Setting.Height)
                                ((Block)((Grid)Parent).Children[ty * Setting.Width + tx]).Open();
                    }

                }
                else if (m_state == State.Mine)
                    return State.Mine;
            }
            return State.Opened;
        }

        public void SetFlag()
        {
            if (!m_isOpened)
            {
                if (!m_isFlaged)
                {
                    this.Background = flagBlock;
                    Game.IncreaseFlagBlockCount();
                }
                else
                {
                    this.Background = closedBlock;
                    Game.DecreaseFlagBlockCount();
                }
                m_isFlaged = !m_isFlaged;
            }
        }
    }
    
}
