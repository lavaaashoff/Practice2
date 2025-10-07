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
        CrimsonSweet = 1,
        YellowMellow,
        SugarBaby,
        CharlestonGray,
        Kai,
        Sorento
    }

    public enum Colors
    {
        Red,
        Yellow,
        Orange,
        Pink
    }


    public class Watermelon : Fruit
    {
        public const int maxPieces = 72;
        public int quantity { get; private set; }
        public Colors color { get; private set; } //добавил свойство цвета



        public Watermelon()
        {
            this.quantity = 0;
            this.color = Colors.Red;
        }

        public Watermelon(Weight weight, WatermelonSort sort, int quantity, Colors color) : base(weight, sort.ToString())
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Кол-во кусочков должно быть натуральным числом (>= 1).");
            this.quantity = quantity;

            if (weight.value < 0.5m)
                throw new ArgumentOutOfRangeException(nameof(weight), "Этот арбуз слишком маленький и его будет жалко.");
            else if (weight.value > 20m)
                throw new ArgumentOutOfRangeException(nameof(weight), "Этот арбуз слишком большой, с ним сложно справиться одному (только если с друзьями).");
            this.color = color;
        }



        public void Cut(int pieces)
        {
            if (pieces <= 0) throw new ArgumentOutOfRangeException(nameof(pieces), "Кол-во кусочков для разреза должно быть положительным.");
            if(pieces > maxPieces) throw new ArgumentOutOfRangeException(nameof(pieces), $"Кол-во кусочков не может превышать {maxPieces}.");
            if (pieces < quantity) throw new ArgumentOutOfRangeException(nameof(pieces),"Арбуз уже порезан на большее кол-во кусочков.");
            else
            {
                quantity = pieces;
                Console.WriteLine($"Арбуз был порезан на {pieces} кусочков.");
            } 
        }

        public void Eat(int pieces)
        {
            if (pieces <= 0) throw new ArgumentOutOfRangeException(nameof(pieces), "Кол-во съеденых кусочков не может быть отрицательным.");
            if (pieces > quantity) throw new InvalidOperationException("Недостаточно кусочков для съедения.");

            weight = new Weight(weight.value - (weight.value / quantity) * pieces);
            quantity -= pieces;
            Console.WriteLine($"{pieces} кусочков было съедено. Сейчас {quantity} осталось ({weight} килограмм)");

            if(quantity == 0 || weight.value < 0.001m)
                Console.WriteLine("От арбуза остались только корочки да семечки.");
        }

        public void Knock()
        {
            var sound = quantity == 0 ? "глухой" : "пустой";
            Console.WriteLine($"Арбуз издал {sound} звук, когда вы постучали.");
        }

        public override string ToString()
        {
            return $"Арбуз: {sort}, Вес: {weight}, Кол-во кусочков: {quantity}";
        }

        //перегрузка оператора +
        public static Watermelon operator +(Watermelon a, Watermelon b)
        {
            if (a == null || b == null)
                throw new ArgumentNullException("Оба арбуза должны существовать.");

            var random = new Random();
            decimal average = (a.weight.value + b.weight.value) / 2.0m;
            decimal deviation = average * 0.15m; // 15% отклонение
            decimal newWeightValue = average + (decimal)(random.NextDouble() * 2 - 1) * deviation;

            Weight newWeight = new Weight(newWeightValue);

            Colors newColor;
            if (a.color == b.color || a.color < b.color) newColor = a.color;
            else newColor = b.color;

            var newSort = random.NextDouble() < 0.5 ? a.sort : b.sort;

            int newQuantity;
            if ((a.quantity + b.quantity) / 2 > maxPieces) newQuantity = maxPieces;
            else newQuantity = (a.quantity + b.quantity) / 2;

            return new Watermelon(newWeight, Enum.Parse<WatermelonSort>(newSort), newQuantity, newColor);
        }

    }
}
