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
        private ksPart _ksPart;

        private void StartKompas()
        {
            /*
            string progId = "KOMPAS.Application.5";
            _kompas = (KompasObject)Marshal.GetActiveObject(progId);
            */
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
                        _ksPart = (ksPart)_document.GetPart((short)Part_Type.pTop_Part);
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

        private void StartUp()
        {
            StartKompas();
            CreateDocument();
        }

        public Kompas3DConnector(ref KompasObject TempKompas,out ksPart TempPart)
        {
            if (TempKompas != null)
            {
                _kompas = TempKompas;
            }
            try
            {
                StartUp();
            }
            catch (Exception e)
            {
                _kompas = null;
                StartUp();
            }
            TempKompas = _kompas;
            TempPart = _ksPart;
        }
    }
}
