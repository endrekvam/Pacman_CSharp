using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static System.Formats.Asn1.AsnWriter;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public Pacman pacman;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            this.KeyDown += MainWindow_KeyDown;
        }

        private void InitializeGame()
        {
            try
            {
                // Filsti til kartet
                string filePath = "C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\map.txt";
                char[,] map;

                // Sjekker om filsti eksisterer
                if (string.IsNullOrWhiteSpace(filePath) || !System.IO.File.Exists(filePath))
                {
                    Console.WriteLine("Finner ikke kart");
                    return;
                }
                else
                {
                    map = MapLoader.LoadMapFromFile(filePath);
                }

                for (int col = 0; col < map.GetLength(1); col++)
                {
                    gameGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                }

                for (int row = 0; row < map.GetLength(0); row++)
                {
                    gameGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                }

                MapLoader.LoadMap(map, gameGrid);
                pacman = new Pacman(this, gameGrid, MapLoader.pRow, MapLoader.pCol);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kunne ikke starte spill: {ex.Message}", "Feilmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Bevegelser
            switch (e.Key)
            {
                case Key.Left:
                    pacman.MoveLeft();
                    break;
                case Key.Right:
                    pacman.MoveRight();
                    break;
                case Key.Up:
                    pacman.MoveUp();
                    break;
                case Key.Down:
                    pacman.MoveDown();
                    break;
            }
        }
        public void UpdateScore(int newScore)
        {
           
             scoreTextBlock.Text = $"Score: {newScore}";

        }
    }
}