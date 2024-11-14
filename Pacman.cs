using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WpfAnimatedGif;
using WpfApp1;

public class Pacman
{
    private MainWindow mainWindow;
    private int row;
    private int col;
    private Grid grid;
    private Image pacmanImage;
    private bool isAnimating;
    private string curPressing = "";
    private int score = 0;
    private bool ferdig = false;

    public Pacman(MainWindow mainWindow, Grid grid, int initialRow, int initialColumn)
    {
        this.mainWindow = mainWindow;
        this.grid = grid;
        row = initialRow;
        col = initialColumn;
        Uri defaultUri = new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\pacmanSpawn.gif");
        InitializePacman(defaultUri);
    }

    private void InitializePacman(Uri defaultUri)
    {
        pacmanImage = new Image();
        ImageBehavior.SetAnimatedSource(pacmanImage, new BitmapImage(defaultUri));
        Grid.SetColumn(pacmanImage, col);
        Grid.SetRow(pacmanImage, row);
        grid.Children.Add(pacmanImage);
    }

    public void MoveLeft()
    {
        if (!ferdig)
        {
            ImageBehavior.SetAnimatedSource(pacmanImage, new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\pacmanLeft.gif")));
            curPressing = "Left";
            if ((MapLoader.mappet[row, col - 1] == 'c' || MapLoader.mappet[row, col - 1] == ' ') && !isAnimating)
            {
                UpdateGridPosition(pacmanImage, col - 1, false);
            }
        }
    }

    public void MoveRight()
    {
        if (!ferdig)
        {
            ImageBehavior.SetAnimatedSource(pacmanImage, new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\pacmanRight.gif")));
            curPressing = "Right";
            if ((MapLoader.mappet[row, col + 1] == 'c' || MapLoader.mappet[row, col + 1] == ' ') && !isAnimating)
            {
                UpdateGridPosition(pacmanImage, col + 1, false);
            }
        }
    }

    public void MoveUp()
    {
        if (!ferdig)
        {
            ImageBehavior.SetAnimatedSource(pacmanImage, new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\pacmanUpFromLeft.gif")));
            curPressing = "Up";
            if ((MapLoader.mappet[row - 1, col] == 'c' || MapLoader.mappet[row - 1, col] == ' ') && !isAnimating)
            {
                UpdateGridPosition(pacmanImage, row - 1, true);
            }
        }
    }

    public void MoveDown()
    {
        if (!ferdig)
        {
            ImageBehavior.SetAnimatedSource(pacmanImage, new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\pacmanDownFromLeft.gif")));
            curPressing = "Down";
            if ((MapLoader.mappet[row + 1, col] == 'c' || MapLoader.mappet[row + 1, col] == ' ') && !isAnimating)
            {
                UpdateGridPosition(pacmanImage, row + 1, true);
            }
        }
    }

    private void UpdateGridPosition(Image pacmanImage, int newPos, bool isVertical)
    {
        isAnimating = true;

        TranslateTransform translateTransform = new TranslateTransform();
        pacmanImage.RenderTransform = translateTransform;

        Grid.SetColumn(pacmanImage, col);
        Grid.SetRow(pacmanImage, row);

        DoubleAnimation animation = new DoubleAnimation
        {
            To = isVertical ? -Konstanter.cellHeight * (row - newPos) : -Konstanter.cellWidth * (col - newPos),
            Duration = TimeSpan.FromSeconds(Konstanter.pacmanSpeed)
        };

        animation.Completed += (sender, e) =>
        {
            translateTransform.Y = 0;
            translateTransform.X = 0;
            if (isVertical)
            {
                row = newPos;
            }
            else
            {
                col = newPos;
            }
            if (MapLoader.mappet[row, col] == 'c')
            {
                RemoveCoin();
            }

            isAnimating = false;
            if (!ferdig)
            {
                if (curPressing == "Left" && (MapLoader.mappet[row, col - 1] == 'c' || MapLoader.mappet[row, col - 1] == ' '))
                {
                    UpdateGridPosition(pacmanImage, col - 1, false);
                }
                if (curPressing == "Right" && (MapLoader.mappet[row, col + 1] == 'c' || MapLoader.mappet[row, col + 1] == ' '))
                {
                    UpdateGridPosition(pacmanImage, col + 1, false);
                }
                if (curPressing == "Up" && (MapLoader.mappet[row - 1, col] == 'c' || MapLoader.mappet[row - 1, col] == ' '))
                {
                    UpdateGridPosition(pacmanImage, row - 1, true);
                }
                if (curPressing == "Down" && (MapLoader.mappet[row + 1, col] == 'c' || MapLoader.mappet[row + 1, col] == ' '))
                {
                    UpdateGridPosition(pacmanImage, row + 1, true);
                }
            }
        };

       translateTransform.BeginAnimation(isVertical ? TranslateTransform.YProperty : TranslateTransform.XProperty, animation);
    }
    private void RemoveCoin()
    {
        MapLoader.mappet[row, col] = ' ';

        Image coinToRemove = MapLoader.coinImages.FirstOrDefault(img => Grid.GetRow(img) == row && Grid.GetColumn(img) == col);
        if (coinToRemove != null)
        {
            grid.Children.Remove(coinToRemove);
            MapLoader.coinImages.Remove(coinToRemove);
            score++;
            mainWindow.UpdateScore(score);
            if (MapLoader.coinImages.Count == 0)
            {
                ImageBehavior.SetAnimatedSource(pacmanImage, new BitmapImage(new Uri("C:\\Users\\endre\\source\\repos\\WpfApp1\\WpfApp1\\Bilder\\pacmanDie.gif")));
                ferdig = true;
            }
        }
    }
}
