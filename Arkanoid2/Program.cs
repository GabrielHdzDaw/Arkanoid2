namespace Arkanoid2
{
    /*
 * Autor: Gabriel Hernández Collado
 * Fecha: 19-12-2024
 * Descripción:
 * 
 */

    using System;
    using System.Media;

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
            int brickWidth = 4; 
            int brickHeight = 1; 
            string[,] coordinates = new string[height, width];

            for (int row = 0; row < height; row++) 
            {
                for (int col = 0; col < width; col++) 
                {
                    int x = col * brickWidth;
                    int y = row * brickHeight;
                    
                    DrawBrick(x, y);
                    coordinates[row, col] = $"{x} {y}";
                }
            }
            return coordinates;
        }
        public static bool BrickCollision(int x, int y, int brickX, int brickY)
        {
            if (running)
            {
                if ((brickX >= x && brickX <= x + 4) || (brickY == y + 1 || brickY == y - 1))
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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓");
            Console.ResetColor();
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
                
                ballX = Math.Clamp(ballX, 0, Console.WindowWidth - 1); // Mantener dentro del rango
            }

            if (ballY >= Console.WindowHeight - 1 || ballY <= 0)
            {
                dy *= -1;
                ballY = Math.Clamp(ballY, 0, Console.WindowHeight - 1); // Mantener dentro del rango
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

            
            int rows = 5; 
            int cols = Console.WindowWidth / 4 - 2;
            int brickWidth = 4;
            int brickHeight = 1;

           
            bool[,] bricks = new bool[rows, cols];

           
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int x = col * brickWidth;
                    int y = row * brickHeight;
                    bricks[row, col] = true; 
                    if (row % 2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                    }
                    
                    DrawBrick(x, y);
                    Console.ResetColor();
                }
            }

            while (running)
            {
               
                ConsoleKeyInfo? key = Console.KeyAvailable ? Console.ReadKey(true) : (ConsoleKeyInfo?)null;
                MoveBar(key);

               
                MoveBall();

                
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        if (bricks[row, col]) 
                        {
                            int brickX = col * brickWidth;
                            int brickY = row * brickHeight;

                           
                            if (ballX >= brickX && ballX < brickX + brickWidth &&
                                ballY == brickY)
                            {
                                dy *= -1; 
                                EraseBrick(brickX, brickY); 
                                bricks[row, col] = false; 
                                break; 
                            }
                        }
                    }
                }
                System.Threading.Thread.Sleep(25);
            }
        }

    }
}