using System;
using Berry;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Form
{
    public readonly struct Weight
    {
        public readonly decimal value { get; }

        public Weight(decimal value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Weight cannot be negative.");
            this.value = value;
        }

        public override string ToString()
        {
            return $"{value} kg";
        }
    }

    public class Fruit
    {
        public Weight weight { get; protected set; }
        public string sort { get; protected set; }
        
        protected Fruit(Weight weight, string sort)
        {
            this.weight = weight;
            this.sort = sort;
        }
        
        public void Deconstruct(out Weight weight, out string sort)
        {
            weight = this.weight;
            sort = this.sort;
        }

        public override string ToString()
        {
            return $"Fruit: {sort}, Weight: {weight}";
        }

    }
}
