using System;
using Form;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berry
{
    public enum WatermelonSort
    {
        CrimosnSweet,
        YellowMellow,
        SugarBaby,
        CharlestonGray,
        Kai,
        Sorento
    }


    public class Watermelon : Fruit
    {
        public int Quantity { get; private set; }

        public Watermelon(Weight weight, WatermelonSort sort, int quantity) : base(weight, sort.ToString())
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException("Кол-во кусочков не может быть отрицательным.");
            }
            Quantity = quantity;
        }



        public void Cut(int pieces)
        {
            if (pieces <= 0) throw new ArgumentOutOfRangeException("Кол-во кусочков для разреза должно быть положительным.");
            if (Quantity <= 0) throw new InvalidOperationException("Арбузе уже порезан.");
            Quantity = pieces; 
            Console.WriteLine($"Арбуз был порезан на {pieces} кусочков.");
        }

        public void Eat(int pieces)
        {
            if (pieces <= 0) throw new ArgumentOutOfRangeException("Кол-во съеденых кусочков не может быть отрицательным.");
            if (Quantity == 0) throw new InvalidOperationException("Сначала порежьте арбуз.");
            Quantity -= pieces;
            Console.WriteLine($"{pieces} кусочков было съедено. Сейчас {Quantity} осталось");
        }

        public void Knock()
        {
            var sound = Quantity == 0 ? "глухой" : "пустой";
            Console.WriteLine($"Арбуз издал {sound} звук, когда вы постучали.");
        }

        public override string ToString()
        {
            return $"Арбуз: {sort}, Вес: {weight}, Кол-во кусочков: {Quantity}";
        }
    }
}
