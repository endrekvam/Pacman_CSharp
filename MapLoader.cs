using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;
using WpfApp1;
using static System.Formats.Asn1.AsnWriter;

// V er vertikalvegg, H er horisontal
// 1 er topp venstre hjørne, 2 er topp høyre
// 3 er bunn venstre hjørne, 4 er bunn høyre
// c for coins

public static class MapLoader
{
    public static int pRow = 0;
    public static int pCol = 0;
    public static char[,] mappet = { { 'p' } };
    public static Image coinImage;
    public static List<Image> coinImages = new List<Image>();

    public static char[,] LoadMapFromFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Kart ikke funnet.", filePath);
        }

        string[] antLinjer = File.ReadAllLines(filePath);

        int numRows = antLinjer.Length;
        int numCols = antLinjer[0].Length;

        char[,] map = new char[numRows, numCols];

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                map[row, col] = antLinjer[row][col];
            }
        }
        mappet = map;
        return map;
    }

    public static void LoadMap(char[,] map, Grid grid)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                char cellType = map[row, col];
                AddCell(grid, row, col, cellType);
            }
        }
    }

    private static void AddCell(Grid grid, int row, int col, char cellType)
    {

        if (cellType == 'P') // Pac-man
        {
            pRow = row;
            pCol = col;

            mappet[row, col] = ' '; // gjør cellen om til en 'vanlig' celle etter pacman er plassert
        }
        switch(cellType)
        {
            case '1':
                Image topLeftCorner = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\topLeftCorner.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(topLeftCorner, row);
                Grid.SetColumn(topLeftCorner, col);
                grid.Children.Add(topLeftCorner);
                break;
            case '2':
                Image topRightCorner = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\topRightCorner.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(topRightCorner, row);
                Grid.SetColumn(topRightCorner, col);
                grid.Children.Add(topRightCorner);
                break;
            case '3':
                Image bottomLeftCorner = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\bottomLeftCorner.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(bottomLeftCorner, row);
                Grid.SetColumn(bottomLeftCorner, col);
                grid.Children.Add(bottomLeftCorner);
                break;
            case '4':
                Image bottomRightCorner = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\bottomRightCorner.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(bottomRightCorner, row);
                Grid.SetColumn(bottomRightCorner, col);
                grid.Children.Add(bottomRightCorner);
                break;
            case 'H':
                Image wallHorizontal = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\wallHorizontal.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(wallHorizontal, row);
                Grid.SetColumn(wallHorizontal, col);
                grid.Children.Add(wallHorizontal);
                break;
            case 'V':
                Image wallVertical = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\wallVertical.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(wallVertical, row);
                Grid.SetColumn(wallVertical, col);
                grid.Children.Add(wallVertical);
                break;
            case 'c':
                coinImage = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\coin.png")),
                    Width = Konstanter.cellWidth/2,
                    Height = Konstanter.cellHeight/2
                };
                Grid.SetRow(coinImage, row);
                Grid.SetColumn(coinImage, col);
                grid.Children.Add(coinImage);
                coinImages.Add(coinImage);
                break;
        } 
     /*   if (cellType == 'X') // Vegg
        { 
            if (row == 0 && col == 0)
            {
                Image topLeftCorner = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\topLeftCorner.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(topLeftCorner, row);
                Grid.SetColumn(topLeftCorner, col);
                grid.Children.Add(topLeftCorner);
            }

            else if (row == 0 && col == mappet.GetLength(1) - 1)
            {
                Image topRightCorner = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\topRightCorner.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(topRightCorner, 0);
                Grid.SetColumn(topRightCorner, col);
                grid.Children.Add(topRightCorner);
            }
            else if (row == mappet.GetLength(0) - 1 && col == 0)
            {
                Image bottomLeftCorner = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\bottomLeftCorner.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(bottomLeftCorner, row);
                Grid.SetColumn(bottomLeftCorner, col);
                grid.Children.Add(bottomLeftCorner);
            }
            else if (row == mappet.GetLength(0) - 1 && col == mappet.GetLength(1) - 1)
            {
                Image bottomRightCorner = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\bottomRightCorner.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(bottomRightCorner, row);
                Grid.SetColumn(bottomRightCorner, col);
                grid.Children.Add(bottomRightCorner);
            } 
            else if (col == 0 || col == mappet.GetLength(1) -1 || mappet[row, col -1] != 'X' || mappet[row, col + 1] != 'X')
            {
                Image wallVertical = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\wallVertical.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(wallVertical, row);
                Grid.SetColumn(wallVertical, col);
                grid.Children.Add(wallVertical);
            }
            else if (row == 0 || row == mappet.GetLength(0) -1 || mappet[row - 1, col] != 'X' || mappet[row + 1, col] != 'X')
            {
                Image wallHorizontal = new Image
                {
                    Source = new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\wallHorizontal.png")),
                    Width = Konstanter.cellWidth,
                    Height = Konstanter.cellHeight
                };
                Grid.SetRow(wallHorizontal, row);
                Grid.SetColumn(wallHorizontal, col);
                grid.Children.Add(wallHorizontal);
            }

        }
        else
        {
            Grid.SetRow(cell, row);
            Grid.SetColumn(cell, col);
            grid.Children.Add(cell);
        }*/
    }

    /*private static SolidColorBrush GetCellColor(char cellType)
    {
        switch (cellType)
        {
            case 'X': // Vegg
                return Brushes.Blue;
            case 'P':
                    return Brushes.Black;
            case 'c': // Penger
                return Brushes.Black;
            default: // Tomrom
                return Brushes.Transparent;
        }
    }*/
}
