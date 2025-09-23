using Form;
using Garden;
using Berry;

namespace Practice2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var watermelon1 = new Watermelon(new Weight(5.5m), WatermelonSort.CrimosnSweet, 1);
            watermelon1.Knock();
            watermelon1.Cut(10);
            watermelon1.Eat(2);

            var watermelon2 = new Watermelon(new Weight(9.6m), WatermelonSort.Sorento, 1);
            var garden = new Garden.Garden();
            garden.ShowPlants();

            (Weight w, string sort) = watermelon1;
            Console.WriteLine($"Deconstructed Watermelon - Sort: {sort}, Weight: {w}");
            garden.ShowPlants();
        }
    }
}
