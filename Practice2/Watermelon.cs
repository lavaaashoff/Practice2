using System;
using Form;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Berry
{
    public enum WatermelonSort
    {
        CrimosnSweet = 1,
        YellowMellow,
        SugarBaby,
        CharlestonGray,
        Kai,
        Sorento
    }


    public class Watermelon : Fruit
    {
        public const int MaxPieces = 72;
        public int Quantity { get; private set; }

        public Watermelon(Weight weight, WatermelonSort sort, int quantity) : base(weight, sort.ToString())
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Кол-во кусочков должно быть натуральным числом (>= 1).");
            Quantity = quantity;

            if (weight.value < 0.5m)
                throw new ArgumentOutOfRangeException(nameof(weight),"Этот арбуз слишком маленький и его будет жалко.");
            else if (weight.value > 20m)
                throw new ArgumentOutOfRangeException(nameof(weight), "Этот арбуз слишком большой, с ним сложно справиться одному (только если с друзьями).");
        }



        public void Cut(int pieces)
        {
            if (pieces <= 0) throw new ArgumentOutOfRangeException(nameof(pieces), "Кол-во кусочков для разреза должно быть положительным.");
            if(pieces > MaxPieces) throw new ArgumentOutOfRangeException(nameof(pieces), $"Кол-во кусочков не может превышать {MaxPieces}.");
            if (pieces < Quantity) throw new ArgumentOutOfRangeException(nameof(pieces),"Арбуз уже порезан на большее кол-во кусочков.");
            else
            {
                Quantity = pieces;
                Console.WriteLine($"Арбуз был порезан на {pieces} кусочков.");
            } 
        }

        public void Eat(int pieces)
        {
            if (pieces <= 0) throw new ArgumentOutOfRangeException(nameof(pieces), "Кол-во съеденых кусочков не может быть отрицательным.");
            if (pieces > Quantity) throw new InvalidOperationException("Недостаточно кусочков для съедения.");

            weight = new Weight(weight.value - (weight.value / Quantity) * pieces);
            Quantity -= pieces;
            Console.WriteLine($"{pieces} кусочков было съедено. Сейчас {Quantity} осталось ({weight} килограмм)");

            if(Quantity == 0 || weight.value < 0.001m)
                Console.WriteLine("От арбуза остались только корочки да семечки.");
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
