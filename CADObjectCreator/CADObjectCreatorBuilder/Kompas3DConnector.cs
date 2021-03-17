using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using KompasAPI7;
using Kompas6Constants;
using Kompas6Constants3D;

namespace CADObjectCreatorBuilder
{
    public class Kompas3DConnector
    {
        private KompasObject _kompas;
        private ksDocument3D _document;

        public KompasObject KompassObj
        {
            get
            {
                return _kompas;
            }
            set
            {
                _kompas = value;
            }
        }

        private void StartKompas()
        {

            if (_kompas == null)
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                _kompas = (KompasObject)Activator.CreateInstance(t);
            }
            _kompas.Visible = true;
            _kompas.ActivateControllerAPI();
            
        }

        private void CreateDocument()
        {
            try
            {
                if (_kompas != null)
                {
                    _document = (ksDocument3D)_kompas.Document3D();
                        _document.Create(false, true);
                        _document = (ksDocument3D)_kompas.ActiveDocument3D();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }

        public void StartUp()
        {
            StartKompas();
            CreateDocument();
        }
    }
}
