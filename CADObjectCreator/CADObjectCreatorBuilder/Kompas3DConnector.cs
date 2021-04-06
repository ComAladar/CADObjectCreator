﻿using System;
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
        /// <param name="kompas"></param>
        /// <returns></returns>
        private KompasObject StartKompas(KompasObject kompas)
        {
            //TODO:
            if (kompas == null)
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                kompas = (KompasObject)Activator.CreateInstance(t);
            }
            kompas.Visible = true;
            kompas.ActivateControllerAPI();
            return kompas;

        }

         //TODO: RSDN
        /// <summary>
        /// Метод создает и возвращает новый документ детали в Компас3D.
        /// </summary>
        /// <param name="kompas"></param>
        /// <param name="document"></param>
        /// <exception cref="NullReferenceException">Нет возможности создать документ без экземпляра Компас3D.</exception>
        /// <returns></returns>
        private ksDocument3D CreateDocument(KompasObject kompas,ksDocument3D document)
        {
            try
            {
                if (kompas != null)
                {
                    document = (ksDocument3D)kompas.Document3D();
                        document.Create(false, true);
                        document = (ksDocument3D)kompas.ActiveDocument3D();
                        return document;
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
        /// <param name="kompas"></param>
        /// <param name="document"></param>
        /// <param name="ksPart"></param>
        private void StartUp(ref KompasObject kompas, ksDocument3D document,out ksPart ksPart)
        {
            kompas=StartKompas(kompas);
            document=CreateDocument(kompas,document);
            ksPart = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
        }

        /// <summary>
        /// Конструктор класса Kompas3DConnector.
        /// </summary>
        /// <param name="tempKompas"></param>
        /// <param name="tempDocument"></param>
        /// <param name="tempPart"></param>
        public Kompas3DConnector(ref KompasObject tempKompas,ref ksDocument3D tempDocument,
            out ksPart tempPart)
        {
            try
            {
                StartUp(ref tempKompas,tempDocument,out tempPart);
            }
            catch
            {
                tempKompas = null;
                StartUp(ref tempKompas,tempDocument,out tempPart);
            }
        }
        
        }
}
