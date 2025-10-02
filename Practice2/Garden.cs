using System;
using Form;
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
    }

    public class Garden : IGarden
    {
        public List<Fruit> Fruits { set; get; } = new();

        public void ShowFruits()
        {
            if (Fruits.Count == 0)
            {
                Console.WriteLine("Сад пуст.");
                return;
            }
            Console.WriteLine("Фрукты в саду:");
            foreach (var fruit in Fruits)
            {
                Console.WriteLine(fruit + "  * ");
            }
        }

        public void AddFruit(Fruit fruit)
        {
            Fruits.Add(fruit);
        }
    }
}

