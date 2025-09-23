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
                throw new ArgumentOutOfRangeException("Quantity cannot be negative.");
            }
            Quantity = quantity;
        }



        public void Cut(int pieces)
        {
            if (pieces <= 0) throw new ArgumentOutOfRangeException("Pieces cannot be negative.");
            if (Quantity > 0) throw new InvalidOperationException("Watermelon has already been cut.");
            Quantity = pieces; 
            Console.WriteLine($"The watermelon has been cut into {pieces} pieces.");
        }

        public void Eat(int pieces)
        {
            if (pieces <= 0) throw new ArgumentOutOfRangeException("Pieces cannot be negative.");
            if (Quantity == 0) throw new InvalidOperationException("First cut the watermelon.");
            Quantity -= pieces;
            Console.WriteLine($"{pieces} pieces had eaten. Now {Quantity} left");
        }

        public void Knock()
        {
            var sound = Quantity == 0 ? "thud" : "hollow";
            Console.WriteLine($"The watermelon makes a {sound} sound when knocked.");
        }

        public override string ToString()
        {
            return $"Watermelon: {sort}, Weight: {weight}, Quantity: {Quantity}";
        }
    }
}
