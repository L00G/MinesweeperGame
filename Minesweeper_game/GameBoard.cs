using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Minesweeper_game
{
    class GameBoard : Grid
    {
        bool isSetEvent;
        public GameBoard() : base()
        {
            isSetEvent = false;
        }

        private void EventMouseLeftClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                Block block = (Block)e.Source;

                if (!block.IsOpened())
                {
                    var state = block.Open();

                    if (state == State.Mine)
                    {
                        if (isSetEvent)
                        {
                            this.PreviewMouseLeftButtonDown -= EventMouseLeftClick;
                            this.PreviewMouseRightButtonDown -= EventMouseRightClick;
                            isSetEvent = !isSetEvent;
                        }

                        Grid grid = ((Grid)sender);

                        foreach(Block nowBlock in grid.Children)
                            nowBlock.OpenMine();
 
                        MessageBox.Show("Failed");
                    }
                    //MessageBox.Show(string.Format("Button clicked at column {0}, row {1}, state{2}", column, row, state));

                }
            }
        }

        private void EventMouseRightClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                Block block = (Block)e.Source;

                if (!block.IsOpened())
                    block.SetFlag();
            }
        }

        public void Initialize(State[,] board)
        {
            if (!isSetEvent)
            {
                this.PreviewMouseLeftButtonDown += EventMouseLeftClick;
                this.PreviewMouseRightButtonDown += EventMouseRightClick;
                isSetEvent = !isSetEvent;
            }

            this.Children.Clear();
            this.ColumnDefinitions.Clear();
            this.RowDefinitions.Clear();

            this.Width = Setting.BlockWidth * Setting.Width;
            this.Height = Setting.BlockHeight * Setting.Height;

            for (int i = 0; i < Setting.Width; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                this.ColumnDefinitions.Add(cd);
            }

            for (int i = 0; i < Setting.Height; i++)
            {
                RowDefinition rd = new RowDefinition();
                this.RowDefinitions.Add(rd);
            }

            for (int j = 0; j < Setting.Height; j++)
                for (int i = 0; i < Setting.Width; i++)
                {
                    Block block = new Block(board[j,i],i,j);
                    Grid.SetColumn(block, i);
                    Grid.SetRow(block, j);
                    this.Children.Add(block);
                }
        }
    }
}
