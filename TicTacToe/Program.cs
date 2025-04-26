using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.Remoting.Channels;

class TicTacToe
{
       static char[] tab = { '1', '2', '3', '4', '5', '6', '7', '8', '9' }; //Массив, содержащий номера ячеек игрового поля
       static int player = 1; //№ игрока

       static void Main()
       {
           int mark; //Отвечает за отметку положения (номер ячейки) Х или 0 на поле
           bool IsEnd=false; //Отвечает за выход из игры
            
           /*
                В начале игры отрисовывается поле 3х3 с номерами ячеек
                Игрок 1 отмечает номер ячейки на игровом поле.
                Проверяется корректность ввода номера (сброс на начало цикла,в случае неверного ввода, текущий игрок и хода остаются)
                Проверяеются условия победы или ничьи.
                Поле отрисовывается с подсветкой номера ячейки
                В конце номер игрока меняется.
                Цикл работает до тех пор, пока один из игроков (1 или 2) не победит или сыграют в ничью. 
             */
           do
           {
               Console.Clear();// Очистка консоли
               Tabcreate(); // Создание игрового поля
               Console.WriteLine($"Игрок {player}, введите номер ячейки:");
               
               try // на случай ввода чего-либо, кроме целового числа. Иначе цикл выбора ячейка для текущего игрока повторяется
               {
                   mark = int.Parse(Console.ReadLine());
                   if (mark >= 1 & mark <= 9 & tab[mark - 1] != 'X' & tab[mark - 1] != '0') // Проверка корректности ввода (число от 1 до 9, на ячейка не должно быть Х или 0
                   {
                       Console.ResetColor();
                       tab[mark - 1] = (player == 1) ? 'X' : '0'; // Изменение ячейки массива на Х или 0 в зависимости от текущего игрока                 

                       if (wincheck()) // Проверка условий для победы, указание победителя
                       {
                           Console.Clear();
                           Tabcreate();
                           Console.WriteLine($"Победил игрок {player}!");
                           Console.ReadLine();
                           IsEnd = true;
                           break;
                       }
                       if (drawcheck()) // Проверка условия ничьи (выйгрышных комбинаций нет, все ячейки заполнены)
                       {
                           Console.Clear();
                           Tabcreate();
                           Console.WriteLine("Ничья!");
                           Console.ReadLine();
                           IsEnd = true;
                           break;
                       }
                    player = (player == 1) ? 2 : 1; // Смена игрока
                   }
               }
               catch // Вывод сообщения, в случае ошибки
               {
                   Console.WriteLine("Введено неверное значение.");
                   Console.ReadLine();
               }

           } while (IsEnd=true);
       }
  
    // Создание(обновление) игрового поля
    static void Tabcreate()
    {
        int i = 0;
        
        /*
            Цикл с построчной проверкой положения Х или 0 на поле, и отрисовкой на консоли поля с номерами ячеек из массива tab[]
            В случае обнаружения в массиве tab[] Х или 0 цвет ячейки меняется (красный или зеленый),
            Если Х или 0 нет цвет ячейки меняется на серый.
         */
        while (i < 9) 
        {
            if ((tab[i] == 'X' && i <= 2) || (tab[i] == 'X' && i > 2 && i <= 5) || (tab[i] == 'X' && i > 5 && i <= 8)) //если есть Х на 1,2 или 3 строке
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($" |" + tab[i] + "| ");
            }
            else if ((tab[i] == '0' && i <= 2) || (tab[i] == '0' && i > 2 && i <= 5) || (tab[i] == '0' && i > 5 && i <= 8)) //если есть 0 на 1,2 или 3 строке
            {
                Console.ForegroundColor = ConsoleColor.Green; 
                Console.Write($" |" + tab[i] + "| ");
            }
            else // если нет ни Х, ни 0
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" |" + tab[i] + "| ");
            }
            if (i == 2 | i == 5) //переход на следующую строку
            {
                Console.ForegroundColor = ConsoleColor.Gray; 
                Console.WriteLine("");
                Console.WriteLine($"---------------");
            }
            i++; 
        }
        Console.WriteLine("");
        Console.ResetColor(); // сброс цвета текста консоли на серый при вводе следующего текста
    }
    
    // Проверка выигрышных комбинаций. Взвращает true, в случае совпадения значений Х или 0 на одной из строк/столбцов/диагонали
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

        // Проверка на ничью (наличие свободных ячеек). Возвращает false в случае если хотя бы в одной ячейке нет ни Х, ни 0. Иначе true.
        static bool drawcheck()
        {
            foreach (char i in tab)
            {
                if (i != 'X' && i != 'O')
                    return false;
            }
            return true;
        }
}    