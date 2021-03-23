using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADObjectCreatorParameters
{
    public class Parameters
    {
        private double _shelfBootsPlaceLength;
        private double _shelfBootsPlaceWidth;
        private List<Parameter<double>> _parameters = new List<Parameter<double>>()
        {
            new Parameter<double>("ShelfLength",420,480,420),
            new Parameter<double>("ShelfWidth",190,220,190),
            new Parameter<double>("ShelfHeight",20,40,20),
            new Parameter<double>("ShelfLegsHeight",40,70,40),
            new Parameter<double>("ShelfBindingHeight",160,180,160)
        };

        public int ShelfLegsRadius { get; private set; } = 20;
        public int ShelfBindingRadius { get; private set; } = 10;
        public int ShelfSlopeRadius { get; private set; } = 5;
        public double FilletRadius { get; private set; } = 0.5;
        public double RadiusMargin { get; private set; } = 21.5;
        public int ShelfBootsPlaceHeight { get; private set; } = 10;
        public int ShelfBindingHeight { get; private set; } = 10;

        public double ShelfBootsPlaceLength
        {
            get
            {
                return _shelfBootsPlaceLength;
            }
            set
            {
                _shelfBootsPlaceLength = 0.7 * value;
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
                _shelfBootsPlaceWidth = 0.85 * value;
            }
        }


        public double this[string index]
        {
            get
            {
                return _parameters.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Value;
            }
            set
            {
                _parameters.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Value = value;
            }
        }

        public double GetMaxParameter(string index)
        {
            return _parameters.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Max;
        }

        public double GetMinParameter(string index)
        {
            return _parameters.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Min;
        }

    }
}
