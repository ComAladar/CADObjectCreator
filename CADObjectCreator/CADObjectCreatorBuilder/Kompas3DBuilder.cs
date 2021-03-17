using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using KompasAPI7;
using Kompas6Constants;
using Kompas6Constants3D;
using CADObjectCreatorParameters;

namespace CADObjectCreatorBuilder
{
    public class Kompas3DBuilder
    {
        private ksPart _ksPart;
        private KompasObject _kompas;

        public void BuildObject(Parameters BuildParameters)
        {
            StartKompas();

        }

        private void StartKompas()
        {
            Kompas3DConnector KompasConnector = new Kompas3DConnector();
            if (_kompas != null)
            {
                KompasConnector.KompassObj = _kompas;
            }
            try
            {
                KompasConnector.StartUp();
            }
            catch (Exception e)
            {
                KompasConnector.KompassObj = null;
                KompasConnector.StartUp();
            }
            _kompas = KompasConnector.KompassObj;
        }

    }
}
