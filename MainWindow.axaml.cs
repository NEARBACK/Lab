using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Avalonia.VisualTree;
using System.Linq;


namespace Lab1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void BtnHello_OnClick(object? sender, RoutedEventArgs e)
    {
        await MessageBoxManager
            .GetMessageBoxStandard("Сообщение", "Hello World C# (Avalonia)!")
            .ShowAsync();
    }

    // Visible=false аналог: в Avalonia проще делать IsVisible=false
    private void BtnHide_OnClick(object? sender, RoutedEventArgs e)
    {
        BtnHide.IsVisible = false;
    }

    // Enabled=false аналог: IsEnabled=false
    private async void BtnDisable_OnClick(object? sender, RoutedEventArgs e)
    {
        BtnDisable.IsEnabled = false;

        // проверка как в твоей лабе: if (button1.Visible == false) ...
        if (!BtnHide.IsVisible)
        {
            await MessageBoxManager
                .GetMessageBoxStandard("Проверка", "Кнопку 'Спрятать' уже не видно.")
                .ShowAsync();
        }
    }

    // В WinForms был TextChanged у Button.
    // В Avalonia у Button текст — это Content, и отдельного TextChanged нет.
    // Поэтому: меняем Content и сразу показываем сообщение.
    private async void BtnChangeText_OnClick(object? sender, RoutedEventArgs e)
    {
        BtnChangeText.Content = "Нажали на кнопку";

        await MessageBoxManager
            .GetMessageBoxStandard("Событие", "Текст кнопки изменился (аналог TextChanged).")
            .ShowAsync();
    }

    private async void BtnMultiply_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(InputNumber.Text, out var number))
        {
            await MessageBoxManager
                .GetMessageBoxStandard("Ошибка", "Введи целое число!")
                .ShowAsync();
            return;
        }

        var result = number * 5;
        ResultLabel.Text = $"Введенное число, умноженное на 5 = {result}";
    }

    // Упражнение 1: по нажатию кнопки значение выводится в TextBox
    private void BtnExercise1_OnClick(object? sender, RoutedEventArgs e)
    {
        Exercise1TextBox.Text = "Привет! Я вывел значение по кнопке ✅";
    }

    // Упражнение 2: знак операции вводится с клавиатуры, используем if
    private async void BtnCalc_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!double.TryParse(CalcA.Text, out var a) || !double.TryParse(CalcB.Text, out var b))
        {
            await MessageBoxManager
                .GetMessageBoxStandard("Ошибка", "Введи числа A и B корректно.")
                .ShowAsync();
            return;
        }

        var op = (CalcOp.Text ?? "").Trim();
        double res;

        if (op == "+") res = a + b;
        else if (op == "-") res = a - b;
        else if (op == "*") res = a * b;
        else if (op == "/")
        {
            if (b == 0)
            {
                await MessageBoxManager
                    .GetMessageBoxStandard("Ошибка", "На ноль делить нельзя.")
                    .ShowAsync();
                return;
            }
            res = a / b;
        }
        else
        {
            await MessageBoxManager
                .GetMessageBoxStandard("Ошибка", "Операция должна быть: +  -  *  /")
                .ShowAsync();
            return;
        }

        CalcResult.Text = $"Результат: {res}";
    }
    protected override void OnOpened(EventArgs e)
        {
        base.OnOpened(e);

        foreach (var btn in this.GetVisualDescendants().OfType<Button>())
        {
            MessageBoxManager
                .GetMessageBoxStandard(
                    "Какие есть кнопки?",
                    btn.Name ?? btn.Content?.ToString() ?? "Без имени")
                .ShowAsync();
        }
    }
}
