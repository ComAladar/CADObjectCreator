using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADObjectCreatorParameters
{
    public class StaticParameters
    {
        public int ShelfLegsRadius { get; private set; } = 20;
        public int ShelfBindingRadius { get; private set; } = 10;
        public int ShelfSlopeRadius { get; private set; } = 5;
        public double FilletRadius { get; private set; } = 0.5;
        public double RadiusMargin { get; private set; } = 21.5;
        public int ShelfBootsHeight { get; private set; } = 10;
        public int BindingHeight { get; private set; } = 10;
    }
}
