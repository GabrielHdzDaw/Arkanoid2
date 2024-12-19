namespace Arkanoid2
{
    /*
 * Autor: Gabriel Hernández Collado
 * Fecha: 19-12-2024
 * Descripción:
 * 
 */

using System;
using System.Threading;

public class Test
{
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
	
	public static void MoveBar(ref int barX)
	{
		
	}
	
	public static void MoveBall(ref int x, ref int dx, ref int y, ref int dy)
	{
		x += dx;
		y += dy;
	}
	
	public static void ChangeBallDirection(ref int x, ref int dx, ref int y, ref int dy, ref int barX, ref int barY)
	{
            if (x + dx >= Console.WindowWidth || x + dx <= 0)
            {
                dx *= -1;
            }

            if (y + dy >= Console.WindowHeight || y + dy <= 0)
            {
                dy *= -1;
            }

            
            if (y + dy == barY && x + dx >= barX && x + dx <= barX + 16)
            {
                dy *= -1; 
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
		
		//~ Console.SetBufferSize(130, 20);
		
		int x = Console.WindowWidth / 2;
		int y = Console.WindowHeight / 2;
		
		int barX = Console.WindowWidth / 2;
		int barY = Console.WindowHeight - 3;
		
		int dx = 1;
		int dy = 1;
		
		Console.CursorVisible = false;
		while (true)
        {
			if (Console.KeyAvailable)
			{
				ConsoleKeyInfo key = Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.LeftArrow:
						if (!(barX <= 0))
						{
							EraseBar(barX + 8, barY);
							barX -= 1;
							DrawBar(barX, barY);
						}
					break;
					case ConsoleKey.RightArrow:
						if (!(barX >= Console.WindowWidth - 16))
						{
							EraseBar(barX, barY);
							barX += 1;
							DrawBar(barX, barY);
						}
					break;
				}
			}
			
			DrawBar(barX, barY);
            DrawBall(x, y);
            Thread.Sleep(10);
            EraseBall(x, y);
            MoveBall(ref x, ref dx, ref y, ref dy);
            ChangeBallDirection(ref x, ref dx, ref y, ref dy, ref barX, ref barY);
        }
		
		
	}
}

}
