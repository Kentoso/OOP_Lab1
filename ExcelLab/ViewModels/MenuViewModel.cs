using System;
using System.Windows.Input;
using ExcelLab.Table;

namespace ExcelLab;

public class MenuViewModel : BaseViewModel
{
    public enum Pages
    {
        Menu,
        Info
    }
    public Pages CurrentPage { get; set; }
    public ICommand SetPageToMenu { get; set; }
    public ICommand SetPageToInfo { get; set; }

    public string Info {get;}
    public MenuViewModel()
    {
        CurrentPage = Pages.Menu;
        SetPageToInfo = new RelayCommand(() => CurrentPage = Pages.Info);
        SetPageToMenu = new RelayCommand(() => CurrentPage = Pages.Menu);
        Info = 
        @"
        Лабораторна робота №1 - Табличний Калькулятор
        Доступні операції:
            Стандартні арифметичні: *, /, +, -, ^, ()
            Факторіал: !
            Посилання на клітинку: |<стовпчик><рядок>|
            Посилання на множину клітинок: |<посилання на початок><посилання на кінець>|
            Функції:
                MAX/MIN - приймає N > 0 аргументів, повертає максимальне/мінімальне число
                AVG - приймає N > 0 аргументів, повертає середнє арифметичне чисел
                IF - приймає 3 аргументи, якщо перший аргумент 0 - повертає другий аргумент, якщо перший аргумент != 0 - повертає перший аргумент
                CMP - приймає 2 аргументи, якщо перший елемент більше другого - повертає 1, якщо вони рівні - 0, в іншому випадку - -1
        Можливі помилки:
            #RECURSION_ERROR# - якщо клітинка тим чи іншим чином залежить сама від себе
            #ADDRESS_ERROR# - якщо клітинка, що задається адресою не існує в таблиці
            #SYNTAX_ERROR# - якщо інтерпретатор у змісті клітинки знаходить невідомий символ 
           
        Виконавець лабораторної роботи - студент групи К-25 Самусь Дем'ян Михайлович
        ";
    }
}