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
            new ParameterBase<double>("ShelfLength",0,100,1),
            new ParameterBase<double>("ShelfWidth",0,100,1),
            new ParameterBase<double>("ShelfHeight",0,100,1),
            new ParameterBase<double>("ShelfLegsHeight",0,100,1),
            new ParameterBase<double>("ShelfBindingHeight",0,100,1)
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


    }
}
