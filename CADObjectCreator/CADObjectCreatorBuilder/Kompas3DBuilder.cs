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
    /// <summary>
    /// Класс отвечающий за построение детали в Компас3D.
    /// </summary>
    public class Kompas3DBuilder
    {
        /// <summary>
        /// Поле данных о детали в компасе.
        /// </summary>
        private ksPart _ksPart;

        /// <summary>
        /// Поле хранящее экземпляр компаса.
        /// </summary>
        private KompasObject _kompas;

        /// <summary>
        /// Поле хранящее деталь в компасе.
        /// </summary>
        private ksDocument3D _document3D;

        /// <summary>
        /// Метод запускающий компас и строящий деталь.
        /// </summary>
        /// <param name="BuildParameters"></param>
        public void BuildObject(Parameters BuildParameters)
        {
            Kompas3DConnector KompasConnector = new Kompas3DConnector(ref _kompas, ref _document3D,out _ksPart);
            BuildMainBody(BuildParameters);
        }

        /// <summary>
        /// Метод построения этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        private void BuildMainBody(Parameters buildParameters)
        {
            #region Создание эскизов и выдавливаний
            ksEntity currentEntity = (ksEntity) _ksPart.GetDefaultEntity((short) Obj3dType.o3d_planeXOY);
            ksDocument2D document2D;

            ksEntity Sketch1 = (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch1Def = Sketch1.GetDefinition();
            Sketch1Def.SetPlane(currentEntity);
            Sketch1.Create();
            document2D = (ksDocument2D) Sketch1Def.BeginEdit();
            BuildLegsSketch(buildParameters,document2D);
            Sketch1Def.EndEdit();
            ExctrusionSketchNormal(buildParameters["ShelfLegsHeight"],Sketch1);
            
            ksEntity newEntity = (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition = (ksPlaneOffsetDefinition) newEntity.GetDefinition();
            newEntityDefinition.SetPlane(currentEntity);
            newEntityDefinition.direction = false;
            newEntityDefinition.offset = -buildParameters["ShelfLegsHeight"];
            newEntity.Create();
            ksEntity Sketch2 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch2Def = Sketch2.GetDefinition();
            Sketch2Def.SetPlane(newEntity);
            Sketch2.Create();
            document2D = (ksDocument2D) Sketch2Def.BeginEdit();
            BuildRectangleSketch(buildParameters,document2D);
            Sketch2Def.EndEdit();
            ExctrusionSketchNormal(buildParameters["ShelfHeight"],Sketch2);

            ksEntity newEntity1 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition1 = (ksPlaneOffsetDefinition)newEntity1.GetDefinition();
            newEntityDefinition1.SetPlane(newEntity);
            newEntityDefinition1.direction = false;
            newEntityDefinition1.offset = -buildParameters["ShelfBindingHeight"]-buildParameters["ShelfHeight"];
            newEntity1.Create();
            ksEntity Sketch3 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch3Def = Sketch3.GetDefinition();
            Sketch3Def.SetPlane(newEntity1);
            Sketch3.Create();
            document2D = (ksDocument2D)Sketch3Def.BeginEdit();
            BuildBindingSketch(buildParameters, document2D);
            Sketch3Def.EndEdit();
            ExctrusionSketchReverse(buildParameters["ShelfBindingHeight"], Sketch3);

            ksEntity newEntity2 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition2 = (ksPlaneOffsetDefinition)newEntity2.GetDefinition();
            newEntityDefinition2.SetPlane(newEntity1);
            newEntity2.Create();
            ksEntity Sketch4 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch4Def = Sketch4.GetDefinition();
            Sketch4Def.SetPlane(newEntity2);
            Sketch4.Create();
            document2D = (ksDocument2D)Sketch4Def.BeginEdit();
            BuildRectangleSketch(buildParameters, document2D);
            Sketch4Def.EndEdit();
            ExctrusionSketchNormal(buildParameters["ShelfHeight"],Sketch4);

            ksEntity newEntity3 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition3 = (ksPlaneOffsetDefinition)newEntity3.GetDefinition();
            newEntityDefinition3.SetPlane(newEntity2);
            newEntityDefinition3.direction = false;
            newEntityDefinition3.offset = -buildParameters["ShelfBindingHeight"]-buildParameters["ShelfHeight"];
            newEntity3.Create();
            ksEntity Sketch5 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch5Def = Sketch5.GetDefinition();
            Sketch5Def.SetPlane(newEntity3);
            Sketch5.Create();
            document2D = (ksDocument2D)Sketch5Def.BeginEdit();
            BuildBindingSketch(buildParameters, document2D);
            Sketch5Def.EndEdit();
            ExctrusionSketchReverse(buildParameters["ShelfBindingHeight"], Sketch5);

            ksEntity newEntity4 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition4 = (ksPlaneOffsetDefinition)newEntity4.GetDefinition();
            newEntityDefinition4.SetPlane(newEntity3);
            newEntity4.Create();
            ksEntity Sketch6 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch6Def = Sketch6.GetDefinition();
            Sketch6Def.SetPlane(newEntity4);
            Sketch6.Create();
            document2D = (ksDocument2D)Sketch6Def.BeginEdit();
            BuildRectangleSketch(buildParameters, document2D);
            Sketch6Def.EndEdit();
            ExctrusionSketchNormal(buildParameters["ShelfHeight"], Sketch6);

            ksEntity newEntity5 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition5 = (ksPlaneOffsetDefinition)newEntity5.GetDefinition();
            newEntityDefinition5.SetPlane(newEntity4);
            newEntityDefinition5.direction = false;
            newEntityDefinition5.offset = -buildParameters["ShelfHeight"];
            newEntity5.Create();
            ksEntity Sketch7 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch7Def = Sketch7.GetDefinition();
            Sketch7Def.SetPlane(newEntity5);
            Sketch7.Create();
            document2D = (ksDocument2D)Sketch7Def.BeginEdit();
            BuildBindingSketch(buildParameters, document2D);
            Sketch7Def.EndEdit();
            ExctrusionSketchNormal(buildParameters.ShelfBindingHeight, Sketch7);

            #endregion
            #region Создание выдавливаний вырезанием для обуви
            ksEntity innerEntity = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset); 
            ksPlaneOffsetDefinition innerEntityDefinition = (ksPlaneOffsetDefinition)innerEntity.GetDefinition(); 
            /*
            innerEntityDefinition.SetPlane(newEntity);
            innerEntityDefinition.direction = false;
            innerEntityDefinition.offset = -buildParameters["ShelfHeight"];
            */
            PlaneOffsetParamsSet(buildParameters, newEntity, innerEntityDefinition);
            innerEntity.Create();
            ksEntity Sketch8 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch); 
            ksSketchDefinition Sketch8Def = Sketch8.GetDefinition(); 
            Sketch8Def.SetPlane(innerEntity);
            Sketch8.Create();
            document2D = (ksDocument2D)Sketch8Def.BeginEdit();
            BuildInnerParts(buildParameters, document2D);
            Sketch8Def.EndEdit();
            CutSketch(buildParameters.ShelfBootsPlaceHeight, Sketch8);

            ksEntity innerEntity1 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition1 = (ksPlaneOffsetDefinition)innerEntity1.GetDefinition();
            /*
            innerEntityDefinition1.SetPlane(newEntity1);
            innerEntityDefinition1.direction = false;
            innerEntityDefinition1.offset = -buildParameters["ShelfHeight"];
            */
            PlaneOffsetParamsSet(buildParameters, newEntity1, innerEntityDefinition1);
            innerEntity1.Create();
            ksEntity Sketch9 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch9Def = Sketch9.GetDefinition();
            Sketch9Def.SetPlane(innerEntity1);
            Sketch9.Create();
            document2D = (ksDocument2D)Sketch9Def.BeginEdit();
            BuildInnerParts(buildParameters, document2D);
            Sketch9Def.EndEdit();
            CutSketch(buildParameters.ShelfBootsPlaceHeight, Sketch9);

            ksEntity innerEntity2 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition2 = (ksPlaneOffsetDefinition)innerEntity2.GetDefinition();
            /*
            innerEntityDefinition2.SetPlane(newEntity4);
            innerEntityDefinition2.direction = false;
            innerEntityDefinition2.offset = -buildParameters["ShelfHeight"];
            */
            PlaneOffsetParamsSet(buildParameters, newEntity4, innerEntityDefinition2);
            innerEntity2.Create();
            ksEntity Sketch10 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch10Def = Sketch10.GetDefinition();
            Sketch10Def.SetPlane(innerEntity2);
            Sketch10.Create();
            document2D = (ksDocument2D)Sketch10Def.BeginEdit();
            BuildInnerParts(buildParameters, document2D);
            Sketch10Def.EndEdit();
            CutSketch(buildParameters.ShelfBootsPlaceHeight, Sketch10);
            #endregion

            #region Создание уклонов
            double height = buildParameters["ShelfLegsHeight"] + buildParameters["ShelfHeight"];
            CreateIncline(buildParameters, height);
            height = buildParameters["ShelfLegsHeight"] + buildParameters["ShelfHeight"] + buildParameters["ShelfBindingHeight"] + buildParameters["ShelfHeight"];
            CreateIncline(buildParameters, height);
            height = buildParameters["ShelfLegsHeight"] + buildParameters["ShelfHeight"] +
                     buildParameters["ShelfBindingHeight"] + buildParameters["ShelfHeight"] +
                     buildParameters["ShelfBindingHeight"] + buildParameters["ShelfHeight"];
            CreateIncline(buildParameters, height);
            #endregion
            #region Создание скруглений
            var ShelfLength = buildParameters["ShelfLength"];
            var ShelfWidth = buildParameters["ShelfWidth"];
            var BindingHeight = buildParameters["ShelfBindingHeight"];
            var LegsHeight = buildParameters["ShelfLegsHeight"];
            var ShelfHeight = buildParameters["ShelfHeight"];

            CreateFillet(buildParameters, (ShelfLength / 2) - buildParameters.RadiusMargin,
                (ShelfWidth / 2) - buildParameters.RadiusMargin, 0);
            CreateFillet(buildParameters, -((ShelfLength / 2) - buildParameters.RadiusMargin),
                ((ShelfWidth / 2) - buildParameters.RadiusMargin), 0);
            CreateFillet(buildParameters, ((ShelfLength / 2) - buildParameters.RadiusMargin),
                -((ShelfWidth / 2) - buildParameters.RadiusMargin), 0);
            CreateFillet(buildParameters, -((ShelfLength / 2) - buildParameters.RadiusMargin),
                -((ShelfWidth / 2) - buildParameters.RadiusMargin), 0);
            
            CreateFillet(buildParameters, 0, 0, LegsHeight);
            CreateFillet(buildParameters, ShelfLength/2, ShelfWidth/2, LegsHeight+ShelfHeight);
            CreateFillet(buildParameters, ShelfLength/2 -2, ShelfWidth/2 -2, LegsHeight+ShelfHeight+BindingHeight);
            CreateFillet(buildParameters, ShelfLength/2, ShelfWidth/2, LegsHeight + ShelfHeight + BindingHeight+ShelfHeight);
            CreateFillet(buildParameters, ShelfLength / 2 - 2, ShelfWidth / 2 - 2,
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight);
            CreateFillet(buildParameters, ShelfLength / 2, ShelfWidth / 2,
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight);

            CreateFillet(buildParameters, (ShelfLength / 2) - buildParameters.RadiusMargin,
                (ShelfWidth / 2) - buildParameters.RadiusMargin,
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight + 10);
            CreateFillet(buildParameters, -((ShelfLength / 2) - buildParameters.RadiusMargin),
                ((ShelfWidth / 2) - buildParameters.RadiusMargin),
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight + 10);
            CreateFillet(buildParameters, ((ShelfLength / 2) - buildParameters.RadiusMargin),
                -((ShelfWidth / 2) - buildParameters.RadiusMargin),
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight + 10);
            CreateFillet(buildParameters, -((ShelfLength / 2) - buildParameters.RadiusMargin),
                -((ShelfWidth / 2) - buildParameters.RadiusMargin),
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight + 10);
            #endregion
        }

        #region Методы
        /// <summary>
        /// Метод построения эскиза прямоугольника по координатам.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildRectangleSketch(Parameters buildParameters,ksDocument2D document)
        {
            document.ksLineSeg(buildParameters["ShelfLength"] / 2, buildParameters["ShelfWidth"] / 2,
                -buildParameters["ShelfLength"] / 2, buildParameters["ShelfWidth"] / 2, 1);
            document.ksLineSeg(buildParameters["ShelfLength"] / 2, buildParameters["ShelfWidth"] / 2,
                buildParameters["ShelfLength"] / 2, -buildParameters["ShelfWidth"] / 2, 1);
            document.ksLineSeg(buildParameters["ShelfLength"] / 2, -buildParameters["ShelfWidth"] / 2,
                -buildParameters["ShelfLength"] / 2, -buildParameters["ShelfWidth"] / 2, 1);
            document.ksLineSeg(-buildParameters["ShelfLength"] / 2, -buildParameters["ShelfWidth"] / 2,
                -buildParameters["ShelfLength"] / 2, buildParameters["ShelfWidth"] / 2, 1);
        }

        /// <summary>
        /// Метод построения эскиза ножек этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildLegsSketch(Parameters buildParameters, ksDocument2D document)
        {
            document.ksCircle((buildParameters["ShelfLength"] / 2) - buildParameters.RadiusMargin,
                (buildParameters["ShelfWidth"] / 2) - buildParameters.RadiusMargin, buildParameters.ShelfLegsRadius, 1);
            document.ksCircle(-((buildParameters["ShelfLength"] / 2) - buildParameters.RadiusMargin),
                (buildParameters["ShelfWidth"] / 2) - buildParameters.RadiusMargin, buildParameters.ShelfLegsRadius, 1);
            document.ksCircle((buildParameters["ShelfLength"] / 2) - buildParameters.RadiusMargin,
                -((buildParameters["ShelfWidth"] / 2) - buildParameters.RadiusMargin), buildParameters.ShelfLegsRadius,
                1);
            document.ksCircle(-((buildParameters["ShelfLength"] / 2) - buildParameters.RadiusMargin),
                -((buildParameters["ShelfWidth"] / 2) - buildParameters.RadiusMargin), buildParameters.ShelfLegsRadius,
                1);
        }

        /// <summary>
        /// Метод построения эскиза креплений этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildBindingSketch(Parameters buildParameters, ksDocument2D document)
        {
            document.ksCircle((buildParameters["ShelfLength"] / 2) - buildParameters.RadiusMargin,
                (buildParameters["ShelfWidth"] / 2) - buildParameters.RadiusMargin, buildParameters.ShelfBindingRadius,1);
            document.ksCircle(-((buildParameters["ShelfLength"] / 2) - buildParameters.RadiusMargin),
                (buildParameters["ShelfWidth"] / 2) - buildParameters.RadiusMargin, buildParameters.ShelfBindingRadius,1);
            document.ksCircle((buildParameters["ShelfLength"] / 2) - buildParameters.RadiusMargin,
                -((buildParameters["ShelfWidth"] / 2) - buildParameters.RadiusMargin),
                buildParameters.ShelfBindingRadius, 1);
            document.ksCircle(-((buildParameters["ShelfLength"] / 2) - buildParameters.RadiusMargin),
                -((buildParameters["ShelfWidth"] / 2) - buildParameters.RadiusMargin),
                buildParameters.ShelfBindingRadius, 1);
        }

        /// <summary>
        /// Метод построения эскиза отделения для обуви этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildInnerParts(Parameters buildParameters, ksDocument2D document)
        {
            document.ksLineSeg(buildParameters.ShelfBootsPlaceLength / 2, buildParameters.ShelfBootsPlaceWidth / 2,
                -buildParameters.ShelfBootsPlaceLength / 2, buildParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(buildParameters.ShelfBootsPlaceLength / 2, buildParameters.ShelfBootsPlaceWidth / 2,
                buildParameters.ShelfBootsPlaceLength / 2, -buildParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(buildParameters.ShelfBootsPlaceLength / 2, -buildParameters.ShelfBootsPlaceWidth / 2,
                -buildParameters.ShelfBootsPlaceLength / 2, -buildParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(-buildParameters.ShelfBootsPlaceLength / 2, -buildParameters.ShelfBootsPlaceWidth / 2,
                -buildParameters.ShelfBootsPlaceLength / 2, buildParameters.ShelfBootsPlaceWidth / 2, 1);
        }

        /// <summary>
        /// Метод осуществляющий выдавливание эскиза.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="Sketch"></param>
        private void ExctrusionSketchNormal(double depth, ksEntity Sketch)
        {
            ksEntity entityExtr = (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition entityExtrDef = (ksBossExtrusionDefinition) entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = (ksExtrusionParam) entityExtrDef.ExtrusionParam();
            entityExtrDef.SetSketch(Sketch);
            entityExtrParam.direction = (short) Direction_Type.dtNormal;
            entityExtrParam.typeNormal = (short) End_Type.etBlind;
            entityExtrParam.depthNormal = depth;
            entityExtr.Create();
        }

        /// <summary>
        /// Метод осуществляющий выдавливание эскиза в обратную сторону.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="Sketch"></param>
        private void ExctrusionSketchReverse(double depth, ksEntity Sketch)
        {
            ksEntity entityExtr = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition entityExtrDef = (ksBossExtrusionDefinition)entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = (ksExtrusionParam)entityExtrDef.ExtrusionParam();
            entityExtrDef.SetSketch(Sketch);
            entityExtrParam.direction = (short)Direction_Type.dtReverse;
            entityExtrParam.typeNormal = (short)End_Type.etBlind;
            entityExtrParam.depthReverse = depth;
            entityExtr.Create();
        }

        /// <summary>
        /// Метод осуществляющий выдавливание эскиза вырезанием.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="Sketch"></param>
        private void CutSketch(double depth, ksEntity Sketch)
        {
            ksEntity entityExtr = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition entityExtrDef = (ksCutExtrusionDefinition)entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = (ksExtrusionParam)entityExtrDef.ExtrusionParam();
            entityExtrDef.SetSketch(Sketch);
            entityExtrParam.direction = (short)Direction_Type.dtNormal;
            entityExtrParam.typeNormal = (short)End_Type.etBlind;
            entityExtrParam.depthNormal = depth;
            entityExtr.Create();
        }

        /// <summary>
        /// Метод для создании уклона отделения для обуви этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="height"></param>
        private void CreateIncline(Parameters buildParameters,double height)
        {
            ksEntity entInc = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_incline);
            ksInclineDefinition incDef = (ksInclineDefinition)entInc.GetDefinition();
            ksEntityCollection collect = (ksEntityCollection)_ksPart.EntityCollection((short)Obj3dType.o3d_face);
            incDef.direction = true;
            incDef.angle = buildParameters.ShelfSlopeRadius; 
            ksEntity currentEntity = (ksEntity)_ksPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            incDef.SetPlane(currentEntity); 
            ksEntityCollection entColInc = (ksEntityCollection)incDef.FaceArray();

            height = height- buildParameters.ShelfBootsPlaceHeight;
            collect.SelectByPoint(0, 0, height);

            entColInc.Add(collect.GetByIndex(0));
            collect.refresh();
            entInc.Create();
        }

        /// <summary>
        /// Методя для выполнения скруглений сторон этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private void CreateFillet(Parameters buildParameters,double x,double y,double z)
        {
            ksEntity entityFillet = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_fillet);
            ksFilletDefinition filletDefinition = (ksFilletDefinition)entityFillet.GetDefinition();
            filletDefinition.radius = buildParameters.FilletRadius;
            filletDefinition.tangent = true;
            ksEntityCollection entityCollectionPart = (ksEntityCollection)_ksPart.EntityCollection((short)Obj3dType.o3d_face);
            ksEntityCollection entityCollectionFillet = (ksEntityCollection)filletDefinition.array();
            entityCollectionFillet.Clear();
            entityCollectionPart.SelectByPoint(x, -y, z);
            entityCollectionFillet.Add(entityCollectionPart.First());
            entityFillet.Create();
        }

        /// <summary>
        /// Метод возвращающий расположение смещенной плоскости при выполнении выдавливания вырезанием.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="tempEntity"></param>
        /// <param name="entityDef"></param>
        /// <returns></returns>
        private ksPlaneOffsetDefinition PlaneOffsetParamsSet(Parameters buildParameters,ksEntity tempEntity,ksPlaneOffsetDefinition entityDef)
        {
            entityDef.SetPlane(tempEntity);
            entityDef.direction = false;
            entityDef.offset = -buildParameters["ShelfHeight"];
            return entityDef;
        }

        #endregion

    }
}
