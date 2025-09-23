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
        void ShowPlants();
    }

    public class Garden : IGarden
    {
        public List<Fruit> Fruits { set; get; } = new();
        
        public void ShowPlants()
        {
            if (Fruits.Count == 0)
            {
                Console.WriteLine("The garden is empty.");
                return;
            }
            Console.WriteLine("Plants in the garden:");
            foreach (var fruit in Fruits)
            {
                Console.WriteLine(fruit + "  * ");
            }
        }
    }
}

