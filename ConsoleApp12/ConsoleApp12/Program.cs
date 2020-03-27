﻿using System;
using System.Threading;

namespace ConsoleApp12
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 60);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Добро пожаловать в змейку! Веселитесь!");
            Console.WriteLine("Выберите и напишите размер поля N x N");
            Console.WriteLine("Управление WASD или стрелочки");
            Console.WriteLine("Всего три уровня сложности: ");
            Console.WriteLine("Легкий уровень - [поле10х10, скорость медленная]");
            Console.WriteLine("Средний уровень - [поле15х15, скорость нормальная]");
            Console.WriteLine("Сложный уровень - [поле20х20, скорость быстрая]");         
            Console.WriteLine("Введите размер поля:");
            Joker koker = new Joker();
            koker.Setup(koker.gameOver);
            while (!koker.gameOver)
            {
                koker.Privet();
                koker.Draw();
                koker.Logic();
                koker.Move();
                koker.itog();
            }
            Console.ReadLine();
        }
    }
}
class Joker
{
    public bool gameOver = false;
    int schet = 0, x, y, celX, celY, nomerhv;
    public int S = Convert.ToInt32(Console.ReadLine());
    public int V = Convert.ToInt32(Console.ReadLine());
    int[] hvostX = new int[1000];
    int[] hvostY = new int[1000];
    Random R = new Random();
    ConsoleKeyInfo knopki;
    public bool Setup(bool gameOver)
    {
        gameOver = false;
        if (S == V)
        {
            x = S / 2 - 1;
            y = V / 2 - 1;
        }
        //Фрукт появиться в рандомной точке
        celX = R.Next(3, S - 2);
        celY = R.Next(3, S - 2);
        Console.WriteLine();
        return gameOver;
    }

    public void Privet()
    {
        Console.SetCursorPosition(30, 10);
        Console.WriteLine("||||||||Выполнил : Бихтеев Петр гр.682||||||||||");
    }
    public void Draw()
    {
        //скорость движения зависит от размера поля!!!
        if (S == 10 && V == 10)
            Thread.Sleep(200);
        if (S == 15 && V == 15)
            Thread.Sleep(100);
        if (S >= 20 && V >= 20)
            Thread.Sleep(50);
        Console.WriteLine();
        Console.SetCursorPosition(0, 8);// Позиция курсора
        for (int i = 0; i < V + 1; i++)
            Console.Write("=");// Граница сверху
        Console.WriteLine();

        for (int i = 0; i < S; i++)
        {
            for (int j = 0; j < S; j++)
            {
                if (j == 0 || j == S - 1)
                    Console.Write((char)24);// Границы с боку
                if (i == y && j == x)
                    Console.Write((char)79); // голова змеи
                else if (i == celY && j == celX)
                    Console.Write((char)70); // фрукт
                else
                {
                    bool print = false;

                    for (int k = 0; k < nomerhv; k++)
                    {
                        if (hvostX[k] == j && hvostY[k] == i)
                        {
                            print = true;
                            Console.Write((char)79); // добавлятся кружок к тельцу
                        }
                    }
                    if (!print)
                        Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        for (int j = 0; j < S + 1; j++)
            Console.Write("=");// Граница с низу
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Счёт: " + schet);
        Console.WriteLine();
    }
    public void Logic()
    {
        int predznX = hvostX[0];
        int predznY = hvostY[0];
        int pred2X;
        int pred2Y;
        hvostX[0] = x;
        hvostY[0] = y;
        for (int i = 1; i < nomerhv; i++)
        {
            pred2X = hvostX[i];
            pred2Y = hvostY[i];
            hvostX[i] = predznX;
            hvostY[i] = predznY;
            predznX = pred2X;
            predznY = pred2Y;
        }
    }
    // Движение змейки
    public void Move()
    {
        if (Console.KeyAvailable == true)
        { knopki = Console.ReadKey(true); }
        switch (knopki.Key)
        {
            case ConsoleKey.W:
                y--;// Вверх
                break;
            case ConsoleKey.UpArrow:
                y--;// Вверх
                break;

            case ConsoleKey.S:
                y++;// Вниз
                break;

            case ConsoleKey.DownArrow:
                y++;// Вниз
                break;

            case ConsoleKey.D:
                x++;// Вправо
                break;

            case ConsoleKey.RightArrow:
                x++;// Вправо
                break;

            case ConsoleKey.A:
                x--;// Влево
                break;

            case ConsoleKey.LeftArrow:
                x--;// Влево
                break;
        }
    }
    public void itog()
    {
        //змейка не умирает когда выходит за границу поля, она появляется с другой (зеркальной)  стороны
        if (x > S)
            x = 0;
        else if (x < 0)
            x = S - 2;
        if (y > V)
            y = 0;
        else if (y < 0)
            y = V - 2;
        for (int g = 0; g < nomerhv; g++)
        {
            if (hvostX[g] == x && hvostY[g] == y)
            {
                gameOver = true;
                Console.WriteLine("Game Over");
                Console.WriteLine("YOU IDIOT!!!");             
                Console.WriteLine("Введите размерность нового поля: ");
                Console.WriteLine("Нажмите клавишу 'ENTER' ");
                Console.ReadKey();
            }
        }
        //перезапуск игры в случае смерти
        if (gameOver != false)
        {
            Console.Clear();
            {
                Joker class1 = new Joker();
                class1.Setup(class1.gameOver);
                while (!class1.gameOver)
                {
                    class1.Privet();
                    class1.Draw();
                    class1.Logic();
                    class1.Move();
                    class1.itog();
                }
                Console.ReadKey(true);
            }
        }
        if (x == celX && y == celY)
        {
            //Подсчет очков
            schet += 10;
            celX = R.Next(3, S - 2);
            celY = R.Next(3, S - 2);
            nomerhv++;
        }
    }
}
    
