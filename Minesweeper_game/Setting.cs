using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_game
{
    class Setting
    {
        private static int c_width = 20;
        private static int c_height = 20;
        private static int c_mineCount = 53;

        const int c_blockWidth = 20;
        const int c_blockHeight = 20;
        const int c_menuHeight = 20;
        const int c_resetButtonWidth = 30;
        const int c_resetButtonHeight = 30;
        
        public static int Width { get => c_width; set => c_width = value; }
        public static int Height { get => c_height; set => c_height = value; }
        public static int MineCount { get => c_mineCount; set => c_mineCount = value; }

        public static int BlockWidth => c_blockWidth;
        public static int BlockHeight => c_blockHeight;
        public static int MenuHeight => c_menuHeight;
        public static int ResetButtonWidth => c_resetButtonWidth;
        public static int ResetButtonHeight => c_resetButtonHeight;
    }
}
