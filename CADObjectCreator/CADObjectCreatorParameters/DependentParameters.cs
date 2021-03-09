using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADObjectCreatorParameters
{
    public class DependentParameters
    {
        private double _shelfBootsPlaceLength;
        private double _shelfBootsPlaceWidth;

        public double ShelfBootsPlaceLength
        {
            get
            {
                return _shelfBootsPlaceLength;
            }
            set
            {
                _shelfBootsPlaceLength = 0.7*value;
            }
        }

        public double ShelfBootsPlaceWidth
        {
            get
            {
                return _shelfBootsPlaceWidth;
            }
            set
            {
                _shelfBootsPlaceWidth = 0.85*value;
            }
        }
    }
}
