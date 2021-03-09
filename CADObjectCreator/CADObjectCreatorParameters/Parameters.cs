using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADObjectCreatorParameters
{
    public class Parameters
    {
        public ParametersList ParametersList { get; set; } = new ParametersList();

        public StaticParameters StaticParameters { get; set; } = new StaticParameters();

        public DependentParameters DependentParameters { get; set; } = new DependentParameters();
    }
}
