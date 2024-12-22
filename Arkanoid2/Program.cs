namespace Arkanoid2
{
    /*
 * Autor: Gabriel Hernández Collado
 * Fecha: 19-12-2024
 * Descripción:
 * 
 */

    using System;

    public class Arkanoid
    {
        private static int barX;
        private static int barY;
        private static int ballX;
        private static int ballY;
        private static int dx = 1;
        private static int dy = 1;
        private static bool running = true;

        public static void DrawBrick(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("▓▓▓▓");
        }

        public static void EraseBrick(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("    ");
        }

        public static string[,] MakeBrickCoordinatesArray(int width, int height)
        {
            int plus = 0;
            string[,] coordinates = new string[height, width];
            for (int i = 1; i < coordinates.GetLength(0); i++)
            {
                for (int j = 1; j < coordinates.GetLength(1); j++)
                {
                    DrawBrick(i + plus, j);
                    coordinates[i, j] = i + plus + " " + j;
                }
                plus += 4;
            }
            return coordinates;
        }
        public static bool BrickCollision(int x, int y, int brickX, int brickY)
        {
            if (running)
            {
                if ((brickX >= x && brickX <= x + 4) || (brickY == y))
                {
                    return true;
                }

            }
            return false;
        }
        public static void MoveBar(ConsoleKeyInfo? key)
        {
            if (key.HasValue)
            {
                switch (key.Value.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (barX > 0)
                        {
                            EraseBar(barX + 8, barY);
                            barX -= 2; // Velocidad barra
                            DrawBar(barX, barY);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (barX < Console.WindowWidth - 16)
                        {
                            EraseBar(barX, barY);
                            barX += 2; // Velocidad barra
                            DrawBar(barX, barY);
                        }
                        break;
                }
            }
        }

        public static void DrawBar(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
        }

        public static void EraseBar(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("        ");
        }

        public static void MoveBall()
        {
            EraseBall(ballX, ballY);
            ballX += dx;
            ballY += dy;

            // Colisiones consola
            if (ballX >= Console.WindowWidth - 1 || ballX <= 0)
            {
                dx *= -1;
            }

            if (ballY >= Console.WindowHeight - 1 || ballY <= 0)
            {
                dy *= -1;
            }

            // Colisiones barra
            if (ballY + dy == barY && ballX + dx >= barX && ballX + dx <= barX + 16)
            {
                dy *= -1;
            }

            DrawBall(ballX, ballY);
        }

        public static void DrawBall(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("O");
        }

        public static void EraseBall(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }

        public static void Main()
        {
            barX = Console.WindowWidth / 2;
            barY = Console.WindowHeight - 3;
            ballX = Console.WindowWidth / 2;
            ballY = Console.WindowHeight / 2;

            Console.CursorVisible = false;

            DrawBar(barX, barY);

            while (running)
            {
                // Leer input del usuario si está disponible
                ConsoleKeyInfo? key = Console.KeyAvailable ? Console.ReadKey(true) : (ConsoleKeyInfo?)null;
                MoveBar(key);

                // Mover la bola
                MoveBall();
                if (BrickCollision(ballX, ballY, b))
                {

                }
                // Control de velocidad del juego
                System.Threading.Thread.Sleep(25);
            }
        }
    }
}