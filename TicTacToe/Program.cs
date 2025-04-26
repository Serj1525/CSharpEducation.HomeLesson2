using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.Remoting.Channels;

class TicTacToe
{
       static char[] tab = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
       static int player = 1;

       static void Main()
       {
           int mark;
           bool IsEnd=false;

           do
           {
               Console.Clear();
               Tabcreate();

               Console.WriteLine($"Игрок {player}, введите номер ячейки:");

               // Проверяем корректность ввода: число от 1 до 9, и ячейка не должна быть занята
               try
               {
                   mark = int.Parse(Console.ReadLine());
                   if (mark >= 1 & mark <= 9 & tab[mark - 1] != 'X' & tab[mark - 1] != '0')
                   {
                       Console.ResetColor();
                       tab[mark - 1] = (player == 1) ? 'X' : '0';                 

                       if (wincheck())
                       {
                           Console.Clear();
                           Tabcreate();
                           Console.WriteLine($"Победил игрок {player}!");
                           IsEnd = true;
                           break;
                       }
                       if (drawcheck())
                       {
                           Console.Clear();
                           Tabcreate();
                           Console.WriteLine("Ничья!");
                           IsEnd = true;
                           break;
                       }
                       
                    player = (player == 1) ? 2 : 1;
                   }
               }
               catch
               {
                   Console.WriteLine("Введено неверное значение.");
                   Console.ReadLine();
               }

           } while (IsEnd=true);
       }
  
    // Выводим текущее состояние игрового поля
    static void Tabcreate()
    {
        int i = 0;
        while (i < 9)

        {
            if ((tab[i] == 'X' && i <= 2) || (tab[i] == 'X' && i > 2 && i <= 5) || (tab[i] == 'X' && i > 5 && i <= 8))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" |" + tab[i] + "| ");
                
             
            }
            else if ((tab[i] == '0' && i <= 2) || (tab[i] == '0' && i > 2 && i <= 5) || (tab[i] == '0' && i > 5 && i <= 8))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($" |" + tab[i] + "| ");
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" |" + tab[i] + "| ");
               
            }
            if (i == 2 | i == 5)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");
                Console.WriteLine($"---------------");

            }
            i++; 
        }
        Console.WriteLine("");
        Console.ResetColor();
    }


    // Проверяем на выигрыш
    static bool wincheck()
        {

            return (tab[0] == tab[1] && tab[1] == tab[2]) ||
                   (tab[3] == tab[4] && tab[4] == tab[5]) ||
                   (tab[6] == tab[7] && tab[7] == tab[8]) ||
                   (tab[0] == tab[3] && tab[3] == tab[6]) ||
                   (tab[1] == tab[4] && tab[4] == tab[7]) ||
                   (tab[2] == tab[5] && tab[5] == tab[8]) ||
                   (tab[0] == tab[4] && tab[4] == tab[8]) ||
                   (tab[2] == tab[4] && tab[4] == tab[6]);
        }

        // Проверяем на ничью
        static bool drawcheck()
        {
            // Проверяем, остались ли свободные ячейки
            foreach (char i in tab)
            {
                if (i != 'X' && i != 'O')
                    return false;
            }
            return true;
        }
  
}    