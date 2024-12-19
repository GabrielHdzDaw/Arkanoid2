namespace Arkanoid2
{
    /*
 * Autor: Gabriel Hernández Collado
 * Fecha: 19-12-2024
 * Descripción:
 * 
 */

    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading;

    public class Arkanoid
    {
        private static int barX;
        private static int barY;
        private static int ballX;
        private static int ballY;
        private static int dx = 1;
        private static int dy = 1;
        private static bool running = true;
        public static void MoveBar()
        {
            while (running)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
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
            while (running)
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
                Thread.Sleep(25); // Velocidad bola
            }
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

            Thread barThread = new Thread(MoveBar);
            Thread ballThread = new Thread(MoveBall);

            barThread.Start();
            ballThread.Start();

            barThread.Join();
            ballThread.Join();
        }
    }

}
