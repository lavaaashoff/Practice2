using Berry;
using Form;
using Garden;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;

namespace Practice2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Garden.Garden garden = new Garden.Garden();

            var watermelon1 = new Watermelon(new Weight(5.5m), WatermelonSort.CrimosnSweet, 1);
            var watermelon2 = new Watermelon(new Weight(9.6m), WatermelonSort.Sorento, 1);
            garden.AddFruit(watermelon1);
            garden.AddFruit(watermelon2);

            string choice;
            do
            {
                Console.WriteLine("Выберитей действие (1 - Добавить фрукт (пока только арбуз) в сад, 2 - Взаимодействие с фруктом, 0 - Выход): ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.WriteLine("Введите вес арбуза: ");
                            Weight weightInput = new Weight(Convert.ToDecimal(Console.ReadLine()));
                            Console.WriteLine("Выберите сорт арбуза (1 - CrimosnSweet, 2 - YellowMellow, 3 - SugarBaby, 4 - CharlestonGray, 5 - Kai, 6 - Sorento): ");
                            string input = Console.ReadLine();

                            if (!int.TryParse(input, out int number) || !Enum.IsDefined(typeof(WatermelonSort), number))
                            {
                                Console.WriteLine("Неверный ввод сорта арбуза.");
                                break;
                            }

                            WatermelonSort sortInput = (WatermelonSort)number;
                            Console.WriteLine("Введите кол-во кусочков: ");
                            int quantityInput = Convert.ToInt32(Console.ReadLine());

                            var newWatermelon = new Watermelon(weightInput, sortInput, quantityInput);
                            garden.AddFruit(newWatermelon);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    case "2":
                        try
                        {
                            Console.WriteLine("Выберите фрукт для взаимодействия (введите индекс): ");

                            garden.ShowFruits();

                            int fruitChoice = Convert.ToInt32(Console.ReadLine());
                            if (fruitChoice < 1 || fruitChoice > garden.Fruits.Count)
                            {
                                Console.WriteLine("Неверный индекс фрукта.");
                                break;
                            }
                            Console.WriteLine("Выберите действие (1 - Постучать, 2 - Порезать, 3 - Съесть): ");
                            int actionChoice = Convert.ToInt32(Console.ReadLine());
                            var selectedWatermelon = garden.Fruits[fruitChoice - 1] as Watermelon;
                            if (selectedWatermelon != null) { 
                                switch (actionChoice)
                                {
                                    case 1:
                                        selectedWatermelon.Knock();
                                        break;
                                    case 2:
                                        Console.WriteLine("Введите кол-во кусочков для разреза: ");
                                        int cutPieces = Convert.ToInt32(Console.ReadLine());
                                        selectedWatermelon.Cut(cutPieces);
                                        break;
                                    case 3:
                                        Console.WriteLine("Введите кол-во кусочков для съедения: ");
                                        int eatPieces = Convert.ToInt32(Console.ReadLine());
                                        selectedWatermelon.Eat(eatPieces);
                                        break;
                                    default:
                                        Console.WriteLine("Неверный выбор действия.");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Выбранный фрукт не является арбузом.");

                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    default:
                        if (choice != "0") Console.WriteLine("Неверный ввод, попробуйте снова");
                        break;
                }

            } while (choice != "0");

            garden.ShowFruits();

        }
    }
}
