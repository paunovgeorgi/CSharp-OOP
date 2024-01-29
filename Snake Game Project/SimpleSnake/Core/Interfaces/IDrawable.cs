using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.Core.Interfaces
{
    internal interface IDrawable
    {
        char DrawSymbol { get; }

        public void Draw()
        {

        }
    }
}
