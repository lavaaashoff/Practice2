using System;
using Form;
using Berry;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garden
{

    public interface IGarden
    {
        void ShowFruits();
        void AddFruit(Fruit fruit);
        void RemoveEatenFruits();
    }

    public class Garden : IGarden
    {
        public List<Fruit> Fruits { set; get; } = new();

        public void ShowFruits()
        {
            RemoveEatenFruits();

            if (Fruits.Count == 0)
            {
                Console.WriteLine("Сад пуст.");
                return;
            }
            Console.WriteLine("Фрукты в саду:");
            for (int i = 0; i < Fruits.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {Fruits[i]}");
            }
        }

        public void AddFruit(Fruit fruit)
        {
            Fruits.Add(fruit);
        }

        public void RemoveEatenFruits()
        {
            for (int i = Fruits.Count - 1; i >= 0; i--) // идём с конца списка
            {
                if (Fruits[i] is Watermelon watermelon)
                    if (watermelon.Quantity == 0 || watermelon.weight.value < 0.001m)
                        Fruits.RemoveAt(i); // удаляем арбуз
            }
        }
    }
}

