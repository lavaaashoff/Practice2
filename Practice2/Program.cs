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

            var watermelon1 = new Watermelon(new Weight(5.5m), WatermelonSort.CrimsonSweet, 1, Colors.Red);
            var watermelon2 = new Watermelon(new Weight(9.6m), WatermelonSort.Sorento, 1, Colors.Yellow);
            garden.AddFruit(watermelon1);
            garden.AddFruit(watermelon2);

            string choice;
            do
            {
                Console.WriteLine("Выберите действие \n 1 - Добавить фрукт (пока только арбуз) в сад \n 2 - Взаимодействие с фруктом \n 3 - cмешать 2 фрукта (арбузы) \n 0 - Выход");
                choice = Console.ReadLine(); // добавил выбор смешивания

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.WriteLine("\nВведите вес арбуза: ");
                            Weight weightInput = new Weight(Convert.ToDecimal(Console.ReadLine())); 
                            Console.WriteLine("\nВыберите сорт арбуза (1 - CrimosnSweet, 2 - YellowMellow, 3 - SugarBaby, 4 - CharlestonGray, 5 - Kai, 6 - Sorento): ");
                            string input = Console.ReadLine();

                            if (!int.TryParse(input, out int number) || !Enum.IsDefined(typeof(WatermelonSort), number))
                            {
                                Console.WriteLine("Неверный ввод сорта арбуза.");
                                break;
                            }

                            WatermelonSort sortInput = (WatermelonSort)number;
                            Console.WriteLine("\nВведите кол-во кусочков: ");
                            int quantityInput = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("\nВыберите цвет арбуза (1 - Red, 2 - Yellow): "); // добавил выбор цвета
                            string colorInput = Console.ReadLine();
                            if (!int.TryParse(colorInput, out int colorNumber) || !Enum.IsDefined(typeof(Colors), colorNumber - 1))
                            {
                                Console.WriteLine("Неверный ввод цвета арбуза.");
                                break;
                            }

                            var newWatermelon = new Watermelon(weightInput, sortInput, quantityInput, (Colors)(colorNumber - 1));
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
                            Console.WriteLine("\nВыберите фрукт для взаимодействия (введите индекс): ");

                            garden.ShowFruits();

                            int fruitChoice = Convert.ToInt32(Console.ReadLine());
                            if (fruitChoice < 1 || fruitChoice > garden.Fruits.Count)
                            {
                                Console.WriteLine("Неверный индекс фрукта.");
                                break;
                            }
                            Console.WriteLine("\nВыберите действие (1 - Постучать, 2 - Порезать, 3 - Съесть): ");
                            int actionChoice = Convert.ToInt32(Console.ReadLine());
                            var selectedWatermelon = garden.Fruits[fruitChoice - 1] as Watermelon;
                            if (selectedWatermelon != null) { 
                                switch (actionChoice)
                                {
                                    case 1:
                                        selectedWatermelon.Knock();
                                        break;
                                    case 2:
                                        Console.WriteLine("\nВведите кол-во кусочков для разреза: ");
                                        int cutPieces = Convert.ToInt32(Console.ReadLine());
                                        selectedWatermelon.Cut(cutPieces);
                                        break;
                                    case 3:
                                        Console.WriteLine("\nВведите кол-во кусочков для съедения: ");
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

                    case ("3"): // тут вот смешивается
                        try
                        {
                            Console.WriteLine("\nВыберите два арбуза для смешивания (введите их индексы через Enter): ");
                            garden.ShowFruits();

                            int choiceOne = Convert.ToInt32(Console.ReadLine());
                            int choiceTwo = Convert.ToInt32(Console.ReadLine());

                            if(choiceOne == choiceTwo)
                            {
                                Console.WriteLine("Нельзя смешивать один и тот же арбуз.");
                                break;
                            }

                            if (choiceOne < 1 || choiceOne > garden.Fruits.Count || choiceTwo < 1 || choiceTwo > garden.Fruits.Count)
                            {
                                Console.WriteLine("Неверный индекс фрукта.");
                                break;
                            }

                            var watermelonA = garden.Fruits[choiceOne - 1] as Watermelon;
                            var watermelonB = garden.Fruits[choiceTwo - 1] as Watermelon;
                            if (watermelonA == null || watermelonB == null)
                            {
                                Console.WriteLine("Один из выбранных фруктов не является арбузом.");
                                break;
                            }

                            Watermelon mixedWatermelon = watermelonA + watermelonB;

                            Console.WriteLine($"\nПолучился новый арбуз: {mixedWatermelon}");
                            garden.AddFruit(mixedWatermelon);

                            garden.DelFruit(watermelonA);
                            garden.DelFruit(watermelonB);
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
                Console.WriteLine();
            } while (choice != "0");

            garden.ShowFruits();

        }
    }
}
