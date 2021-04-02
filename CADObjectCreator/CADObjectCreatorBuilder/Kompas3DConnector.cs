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
    /// <summary>
    /// Класс отвечающий за запуск и работу с Компас3D.
    /// </summary>
    public class Kompas3DConnector
    {
         //TODO: RSDN
        /// <summary>
        /// Метод запускает и возвращает активный экземпляр Компас3D.
        /// </summary>
        /// <param name="_kompas"></param>
        /// <returns></returns>
        private KompasObject StartKompas(KompasObject _kompas)
        {
            //TODO:
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
            return _kompas;

        }

         //TODO: RSDN
        /// <summary>
        /// Метод создает и возвращает новый документ детали в Компас3D.
        /// </summary>
        /// <param name="_kompas"></param>
        /// <param name="_document"></param>
        /// <exception cref="NullReferenceException">Нет возможности создать документ без экземпляра Компас3D.</exception>
        /// <returns></returns>
        private ksDocument3D CreateDocument(KompasObject _kompas,ksDocument3D _document)
        {
            try
            {
                if (_kompas != null)
                {
                    _document = (ksDocument3D)_kompas.Document3D();
                        _document.Create(false, true);
                        _document = (ksDocument3D)_kompas.ActiveDocument3D();
                        return _document;
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

         //TODO: RSDN
        /// <summary>
        /// Метод запускает Компас3D и создает документ детали. Возвращает ссылку на деталь в документе.
        /// </summary>
        /// <param name="_kompas"></param>
        /// <param name="_document"></param>
        /// <param name="_ksPart"></param>
        private void StartUp(ref KompasObject _kompas, ksDocument3D _document,out ksPart _ksPart)
        {
            _kompas=StartKompas(_kompas);
            _document=CreateDocument(_kompas,_document);
            _ksPart = (ksPart)_document.GetPart((short)Part_Type.pTop_Part);
        }

        /// <summary>
        /// Конструктор класса Kompas3DConnector.
        /// </summary>
        /// <param name="TempKompas"></param>
        /// <param name="TempDocument"></param>
        /// <param name="TempPart"></param>
        public Kompas3DConnector(ref KompasObject TempKompas,ref ksDocument3D TempDocument,out ksPart TempPart)
        {
            try
            {
                StartUp(ref TempKompas,TempDocument,out TempPart);
            }
            catch
            {
                TempKompas = null;
                StartUp(ref TempKompas,TempDocument,out TempPart);
            }
        }
        
        }
}
