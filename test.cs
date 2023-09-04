using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class FlappyBird
{
    static int birdY = 10;
    static int birdX = 10;
    static int gravity = 1;
    static int score = 0;
    static List<int> pipeX = new List<int>();
    static List<int> pipeY = new List<int>();

    static void Main()
    {
        Console.Title = "Flappy Bird";
        Console.WindowHeight = 20;
        Console.WindowWidth = 40;
        Console.BufferHeight = 20;
        Console.BufferWidth = 40;
        Console.CursorVisible = false;

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;
                if (key == ConsoleKey.Spacebar)
                {
                    if (birdY > 0)
                    {
                        birdY -= 2; // Jump by 2 units
                    }
                }
            }

            UpdateGame();
            DrawGame();
            Thread.Sleep(50);
        }
    }

    static void UpdateGame()
    {
        // Update bird position
        birdY += gravity;

        // Update pipe positions
        for (int i = 0; i < pipeX.Count; i++)
        {
            pipeX[i]--;
            if (pipeX[i] == 0)
            {
                pipeX.RemoveAt(i);
                pipeY.RemoveAt(i);
            }
        }

        // Add new pipes
        if (score % 10 == 0)
        {
            int pipeHeight = new Random().Next(1, 10);
            pipeX.Add(Console.WindowWidth - 1);
            pipeY.Add(pipeHeight);
        }

        // Check for collisions
        for (int i = 0; i < pipeX.Count; i++)
        {
            if (birdX >= pipeX[i] && birdX <= pipeX[i] + 1 && (birdY < pipeY[i] || birdY > pipeY[i] + 5))
            {
                // Game over
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
                Console.Write("Game Over");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2 + 1);
                Console.Write("Score: " + score);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        // Increase score
        if (pipeX.Count > 0 && birdX == pipeX[0] + 2)
        {
            score++;
        }
    }

    static void DrawGame()
    {
        Console.Clear();

        // Draw bird
        Console.SetCursorPosition(birdX, birdY);
        Console.Write(">");
        
        // Draw pipes
        for (int i = 0; i < pipeX.Count; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Console.SetCursorPosition(pipeX[i], pipeY[i] + j);
                Console.Write("■");
            }
        }

        // Draw ground
        Console.SetCursorPosition(0, Console.WindowHeight - 1);
        Console.Write(new string('=', Console.WindowWidth));

        // Display score
        Console.SetCursorPosition(2, 0);
        Console.Write("Score: " + score);
    }
}
