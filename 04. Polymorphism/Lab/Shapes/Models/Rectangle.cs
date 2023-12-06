using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Models
{
    internal class Rectangle : Shape
    {
        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }

        public double Height { get; private set; }
        public double Width { get; private set; }
        public override double CalculatePerimeter()
        {
            return Height * 2 + Width * 2;
        }

        public override double CalculateArea()
        {
            return Height * Width;
        }

    }
}
