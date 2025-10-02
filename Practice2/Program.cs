using System;
using System.Collections.Generic;
using System.Linq;
using Form;
using Garden;
using Berry;

namespace Practice2
{
    internal class Program
    {
        private static List<Watermelon> _melons = new();

        static void Main(string[] args)
        {
            var garden = new Garden.Garden();

            // Стартовые примеры, чтобы не было пусто
            var watermelon1 = new Watermelon(new Weight(5.5m), WatermelonSort.CrimosnSweet, 1);
            var watermelon2 = new Watermelon(new Weight(9.6m), WatermelonSort.Sorento, 1);

            TryAddToGarden(garden, watermelon1);
            TryAddToGarden(garden, watermelon2);

            _melons.Add(watermelon1);
            _melons.Add(watermelon2);

            RunMenu(garden);

            // При выходе показываем, что в саду
            Console.WriteLine("\nИтоговое состояние сада:");
            SafeShowPlants(garden);
            Console.WriteLine("\nПока.");
        }

        static void RunMenu(Garden.Garden garden)
        {
            while (true)
            {
                Console.WriteLine(@"
================== МЕНЮ ==================
1. Показать арбузы в моей коллекции
2. Добавить арбуз в коллекцию и в сад
3. Постучать по арбузу
4. Разрезать арбуз (указать на сколько частей)
5. Съесть часть арбуза (указать количество)
6. Деконструировать арбуз и показать вес/сорт
7. Показать растения в саду (garden.ShowPlants)
0. Выход (показать сад и завершить)
==========================================
");
                int choice = ReadInt("Выбор: ", min: 0, max: 7);

                switch (choice)
                {
                    case 0:
                        return;

                    case 1:
                        ListMelons();
                        break;

                    case 2:
                        AddMelonFlow(garden);
                        break;

                    case 3:
                        WithSelectedMelon(m =>
                        {
                            m.Knock();
                            Console.WriteLine("Готово.");
                        });
                        break;

                    case 4:
                        WithSelectedMelon(m =>
                        {
                            int parts = ReadInt("Сколько частей сделать? ", min: 1);
                            m.Cut(parts);
                            Console.WriteLine("Разрезано.");
                        });
                        break;

                    case 5:
                        WithSelectedMelon(m =>
                        {
                            int amount = ReadInt("Сколько съесть (целое число)? ", min: 1);
                            m.Eat(amount);
                            Console.WriteLine("Приятного аппетита, хищник сахара.");
                        });
                        break;

                    case 6:
                        WithSelectedMelon(m =>
                        {
                            (Weight w, string sort) = m;
                            Console.WriteLine($"Деконструкция: сорт = {sort}, вес = {w}");
                        });
                        break;

                    case 7:
                        SafeShowPlants(garden);
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AddMelonFlow(Garden.Garden garden)
        {
            Console.WriteLine("Добавление арбуза:");

            decimal weight = ReadDecimal("Вес, кг (например 7.3): ", min: 0.1m);
            var sort = ReadEnumChoice<WatermelonSort>("Выбери сорт");
            int param = ReadInt("Третий параметр конструктора (int). Если это спелость/идентификатор — введи число: ", min: 0);

            var wm = new Watermelon(new Weight(weight), sort, param);

            _melons.Add(wm);
            if (TryAddToGarden(garden, wm))
            {
                Console.WriteLine("Арбуз добавлен в коллекцию и в сад.");
            }
            else
            {
                Console.WriteLine("Арбуз добавлен в коллекцию. В сад не удалось добавить: не найден метод AddPlant/Add.");
            }
        }

        static void ListMelons()
        {
            if (_melons.Count == 0)
            {
                Console.WriteLine("Коллекция пуста. Печально как холодильник студента.");
                return;
            }

            Console.WriteLine("Твои арбузы:");
            for (int i = 0; i < _melons.Count; i++)
            {
                var m = _melons[i];
                (Weight w, string sort) = m;
                Console.WriteLine($"{i + 1}. {sort}, вес {w}");
            }
        }

        static void WithSelectedMelon(Action<Watermelon> action)
        {
            if (_melons.Count == 0)
            {
                Console.WriteLine("Нет арбузов. Сначала добавь хоть один.");
                return;
            }

            ListMelons();
            int idx = ReadInt("Выбери номер арбуза: ", min: 1, max: _melons.Count);
            var melon = _melons[idx - 1];
            action(melon);
        }

        static void SafeShowPlants(Garden.Garden garden)
        {
            try
            {
                garden.ShowFruits();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"garden.ShowPlants() упал: {ex.Message}");
            }
        }

        // Пытаемся вызвать garden.AddPlant(plant) или garden.Add(plant), что найдется
        static bool TryAddToGarden(object garden, object plant)
        {
            var gType = garden.GetType();

            var addPlant = gType.GetMethods()
                .FirstOrDefault(m =>
                    m.Name == "AddPlant" &&
                    m.GetParameters().Length == 1);

            if (addPlant != null)
            {
                try
                {
                    addPlant.Invoke(garden, new[] { plant });
                    return true;
                }
                catch
                {
                    // ignore and try Add
                }
            }

            var add = gType.GetMethods()
                .FirstOrDefault(m =>
                    m.Name == "Add" &&
                    m.GetParameters().Length == 1);

            if (add != null)
            {
                try
                {
                    add.Invoke(garden, new[] { plant });
                    return true;
                }
                catch
                {
                    // ignore
                }
            }

            return false;
        }

        // ====== Утилиты ввода ======

        static int ReadInt(string prompt, int? min = null, int? max = null)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (int.TryParse(s, out int v))
                {
                    if (min.HasValue && v < min.Value)
                    {
                        Console.WriteLine($"Число должно быть ≥ {min.Value}.");
                        continue;
                    }
                    if (max.HasValue && v > max.Value)
                    {
                        Console.WriteLine($"Число должно быть ≤ {max.Value}.");
                        continue;
                    }
                    return v;
                }
                Console.WriteLine("Это не похоже на целое число. Попробуй еще.");
            }
        }

        static decimal ReadDecimal(string prompt, decimal? min = null, decimal? max = null)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();

                // Поддержка запятой и точки как разделителя
                s = s?.Replace(',', '.');

                if (decimal.TryParse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out decimal v))
                {
                    if (min.HasValue && v < min.Value)
                    {
                        Console.WriteLine($"Число должно быть ≥ {min.Value}.");
                        continue;
                    }
                    if (max.HasValue && v > max.Value)
                    {
                        Console.WriteLine($"Число должно быть ≤ {max.Value}.");
                        continue;
                    }
                    return v;
                }
                Console.WriteLine("Это не похоже на число. Еще раз.");
            }
        }

        static TEnum ReadEnumChoice<TEnum>(string title) where TEnum : struct, Enum
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();

            Console.WriteLine(title + ":");
            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {values[i]}");
            }

            int pick = ReadInt("Номер: ", min: 1, max: values.Length);
            return values[pick - 1];
        }
    }
}
