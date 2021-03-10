using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADObjectCreatorParameters
{
    public class ParametersList
    {
        private List<ParameterBase<double>> _parametersBase = new List<ParameterBase<double>>()
        {
            new ParameterBase<double>("ShelfLength",420,480,420),
            new ParameterBase<double>("ShelfWidth",190,220,190),
            new ParameterBase<double>("ShelfHeight",20,40,20),
            new ParameterBase<double>("ShelfLegsHeight",40,70,40),
            new ParameterBase<double>("ShelfBindingHeight",160,180,160)
        };

        public double this[string index]
        {
            get
            {
                return _parametersBase.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Value;
            }
            set
            {
                _parametersBase.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Value = value;
            }
        }

        /// <summary>
        /// Возвращает максимум при true или минимум при false;
        /// </summary>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public double this[string index,bool variable]
        {
            get
            {
                if(variable==true)
                { 
                return _parametersBase.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Max;
                }
                else
                {
                    return _parametersBase.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Min;
                }
            }
            set
            {
                _parametersBase.Find((ParameterBase) => ParameterBase.Name.Equals(index)).Value = value;
            }
        }
    }
}
