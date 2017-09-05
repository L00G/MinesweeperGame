using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MinesweeperGame
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    
    enum Level
    {
        Easy,
        Normal,
        Hard
    }

    public partial class MainWindow : Window
    {
        private static ImageBrush resetButtonNormal = new ImageBrush(new BitmapImage(new Uri("../../resource/resetButtonNormal.jpg", UriKind.Relative)));

        static Game oGame;
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize | ResizeMode.CanMinimize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Menu menu = new Menu();
            menu.Height = Setting.MenuHeight;
            menu.VerticalAlignment = VerticalAlignment.Top;

            MenuItem menuItem1 = new MenuItem();
            menuItem1.Header = "menu";

            MenuItem levelEasy = new MenuItem();
            levelEasy.Header = "Easy";
            levelEasy.Click += EventLevelSelect;
            menuItem1.Items.Add(levelEasy);

            MenuItem levelNormal = new MenuItem();
            levelNormal.Header = "Normal";
            levelNormal.Click += EventLevelSelect;
            menuItem1.Items.Add(levelNormal);

            MenuItem levelHard = new MenuItem();
            levelHard.Header = "Hard";
            levelHard.Click += EventLevelSelect;
            menuItem1.Items.Add(levelHard);

            menu.Items.Add(menuItem1);

            Button resetButton = new Button();
            resetButton.PreviewMouseLeftButtonDown += EventResetButtonClick;
            resetButton.Height = Setting.ResetButtonHeight;
            resetButton.Width = Setting.ResetButtonWidth;
            resetButton.Background = resetButtonNormal;

            oGame = new Game();
            oGame.Initialize();
            StackPanel sp = new StackPanel();
            sp.Children.Add(menu);
            sp.Children.Add(resetButton);
            sp.Children.Add(oGame.GetGameBoard());

            this.Width = Setting.Width * Setting.BlockWidth + 15;
            this.Height = Setting.Height * Setting.BlockHeight + Setting.MenuHeight + Setting.ResetButtonHeight + 40;

            this.Content = sp;
        }

        private void EventResetButtonClick(object sender, MouseButtonEventArgs e)
        {
            oGame.Initialize();
        }

        private void EventLevelSelect(object sender, RoutedEventArgs e)
        {
            string header = ((MenuItem)e.Source).Header.ToString();
            if (header == "Easy")
            {
                Setting.Width = 9;
                Setting.Height = 9;
                Setting.MineCount = 10;
            }
            else if (header == "Normal")
            {
                Setting.Width = 15;
                Setting.Height = 15;
                Setting.MineCount = 48;      
            }
            else if(header == "Hard")
            {
                Setting.Width = 30;
                Setting.Height = 30;
                Setting.MineCount = 210;
            }
            this.Width = Setting.Width * Setting.BlockWidth+15;
            this.Height = Setting.Height * Setting.BlockHeight + Setting.MenuHeight + Setting.ResetButtonHeight+40;
            oGame.Initialize();
        }
    }
}
