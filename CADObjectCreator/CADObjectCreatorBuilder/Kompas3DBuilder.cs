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

         //TODO: RSDN
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
            ksEntity currentEntity = 
                (ksEntity) _ksPart.GetDefaultEntity((short) Obj3dType.o3d_planeXOY);
            ksDocument2D document2D=null;
            ksEntity Sketch1 = 
                (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch1Def = Sketch1.GetDefinition();
            BuildLegsModel(Sketch1Def,Sketch1,currentEntity,
                document2D,buildParameters);

            //TODO: Дубли
            double offsetDistance= 
                -buildParameters[ParametersName.ShelfLegsHeight].Value;
            ksEntity newEntity = 
                (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition = 
                (ksPlaneOffsetDefinition) newEntity.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, currentEntity, newEntityDefinition);
            newEntity.Create();
            ksEntity Sketch2 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch2Def = Sketch2.GetDefinition();
            BuildShelfModel(Sketch2Def,Sketch2,newEntity,
                document2D,buildParameters);

            offsetDistance= 
                -buildParameters[ParametersName.ShelfBindingHeight].Value 
                - buildParameters[ParametersName.ShelfHeight].Value;
            ksEntity newEntity1 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition1 = 
                (ksPlaneOffsetDefinition)newEntity1.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity, newEntityDefinition1);
            newEntity1.Create();
            ksEntity Sketch3 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch3Def = Sketch3.GetDefinition();
            BuildBindingModelReverse(Sketch3Def, Sketch3, newEntity1, 
                document2D,buildParameters);

            ksEntity newEntity2 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition2 = 
                (ksPlaneOffsetDefinition)newEntity2.GetDefinition();
            newEntityDefinition2.SetPlane(newEntity1);
            newEntity2.Create();
            ksEntity Sketch4 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch4Def = Sketch4.GetDefinition();
            BuildShelfModel(Sketch4Def, Sketch4, newEntity2, 
                document2D, buildParameters);

            ksEntity newEntity3 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition3 = 
                (ksPlaneOffsetDefinition)newEntity3.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity2, newEntityDefinition3);
            newEntity3.Create();
            ksEntity Sketch5 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch5Def = Sketch5.GetDefinition();
            BuildBindingModelReverse(Sketch5Def, Sketch5, newEntity3, 
                document2D, buildParameters);

            ksEntity newEntity4 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition4 = 
                (ksPlaneOffsetDefinition)newEntity4.GetDefinition();
            newEntityDefinition4.SetPlane(newEntity3);
            newEntity4.Create();
            ksEntity Sketch6 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch6Def = Sketch6.GetDefinition();
            BuildShelfModel(Sketch6Def, Sketch6, newEntity4, 
                document2D, buildParameters);

            offsetDistance = -buildParameters[ParametersName.ShelfHeight].Value;
            ksEntity newEntity5 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition5 = 
                (ksPlaneOffsetDefinition)newEntity5.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity4, newEntityDefinition5);
            newEntity5.Create();
            ksEntity Sketch7 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch7Def = Sketch7.GetDefinition();
            BuildBindingModelNormal(Sketch7Def,Sketch7,newEntity5,
                document2D,buildParameters);

            offsetDistance = -buildParameters[ParametersName.ShelfHeight].Value;
            ksEntity innerEntity = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset); 
            ksPlaneOffsetDefinition innerEntityDefinition = 
                (ksPlaneOffsetDefinition)innerEntity.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity, innerEntityDefinition);
            innerEntity.Create();
            ksEntity Sketch8 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch); 
            ksSketchDefinition Sketch8Def = Sketch8.GetDefinition();
            BuildInnerPartsModel(Sketch8Def, Sketch8, innerEntity, 
                document2D, buildParameters);

            ksEntity innerEntity1 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition1 = 
                (ksPlaneOffsetDefinition)innerEntity1.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity1, innerEntityDefinition1);
            innerEntity1.Create();
            ksEntity Sketch9 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch9Def = Sketch9.GetDefinition();
            BuildInnerPartsModel(Sketch9Def, Sketch9, innerEntity1, 
                document2D, buildParameters);

            ksEntity innerEntity2 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition2 = 
                (ksPlaneOffsetDefinition)innerEntity2.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity4, innerEntityDefinition2);
            innerEntity2.Create();
            ksEntity Sketch10 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch10Def = Sketch10.GetDefinition();
            BuildInnerPartsModel(Sketch10Def, Sketch10, innerEntity2, 
                document2D, buildParameters);
            BuildAllInclines(buildParameters);
            BuildAllFillets(buildParameters);
        }

        /// <summary>
        /// Метод построения эскиза прямоугольника по координатам.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildRectangleSketch(Parameters buildParameters,ksDocument2D document)
        {
            document.ksLineSeg(
                buildParameters[ParametersName.ShelfLength].Value / 2, 
                buildParameters[ParametersName.ShelfWidth].Value / 2,
                -buildParameters[ParametersName.ShelfLength].Value / 2, 
                buildParameters[ParametersName.ShelfWidth].Value / 2, 1);
            document.ksLineSeg(
                buildParameters[ParametersName.ShelfLength].Value / 2, 
                buildParameters[ParametersName.ShelfWidth].Value / 2,
                buildParameters[ParametersName.ShelfLength].Value / 2, 
                -buildParameters[ParametersName.ShelfWidth].Value / 2, 1);
            document.ksLineSeg(
                buildParameters[ParametersName.ShelfLength].Value / 2, 
                -buildParameters[ParametersName.ShelfWidth].Value / 2,
                -buildParameters[ParametersName.ShelfLength].Value / 2, 
                -buildParameters[ParametersName.ShelfWidth].Value / 2, 1);
            document.ksLineSeg(
                -buildParameters[ParametersName.ShelfLength].Value / 2, 
                -buildParameters[ParametersName.ShelfWidth].Value / 2,
                -buildParameters[ParametersName.ShelfLength].Value / 2, 
                buildParameters[ParametersName.ShelfWidth].Value / 2, 1);
        }

        /// <summary>
        /// Метод построения эскиза ножек этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildLegsSketch(Parameters buildParameters, ksDocument2D document)
        {
            document.ksCircle(
                (buildParameters[ParametersName.ShelfLength].Value / 2) - Parameters.RadiusMargin,
                (buildParameters[ParametersName.ShelfWidth].Value / 2) - Parameters.RadiusMargin, 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                -((buildParameters[ParametersName.ShelfLength].Value / 2) - Parameters.RadiusMargin),
                (buildParameters[ParametersName.ShelfWidth].Value / 2) - Parameters.RadiusMargin, 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                (buildParameters[ParametersName.ShelfLength].Value / 2) - Parameters.RadiusMargin,
                -((buildParameters[ParametersName.ShelfWidth].Value / 2) - Parameters.RadiusMargin), 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                -((buildParameters[ParametersName.ShelfLength].Value / 2) - Parameters.RadiusMargin),
                -((buildParameters[ParametersName.ShelfWidth].Value / 2) - Parameters.RadiusMargin), 
                Parameters.ShelfLegsRadius, 1);
        }

        /// <summary>
        /// Метод построения эскиза креплений этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildBindingSketch(Parameters buildParameters, ksDocument2D document)
        {
            document.ksCircle(
                (buildParameters[ParametersName.ShelfLength].Value / 2) - Parameters.RadiusMargin,
                (buildParameters[ParametersName.ShelfWidth].Value / 2) - Parameters.RadiusMargin, 
                Parameters.ShelfBindingRadius,1);
            document.ksCircle(
                -((buildParameters[ParametersName.ShelfLength].Value / 2) - Parameters.RadiusMargin),
                (buildParameters[ParametersName.ShelfWidth].Value / 2) - Parameters.RadiusMargin, 
                Parameters.ShelfBindingRadius,1);
            document.ksCircle(
                (buildParameters[ParametersName.ShelfLength].Value / 2) - Parameters.RadiusMargin,
                -((buildParameters[ParametersName.ShelfWidth].Value / 2) - Parameters.RadiusMargin),
                Parameters.ShelfBindingRadius, 1);
            document.ksCircle(
                -((buildParameters[ParametersName.ShelfLength].Value / 2) - Parameters.RadiusMargin),
                -((buildParameters[ParametersName.ShelfWidth].Value / 2) - Parameters.RadiusMargin),
                Parameters.ShelfBindingRadius, 1);
        }

        /// <summary>
        /// Метод построения эскиза отделения для обуви этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildInnerParts(Parameters buildParameters, ksDocument2D document)
        {
            document.ksLineSeg(
                buildParameters.ShelfBootsPlaceLength / 2, 
                buildParameters.ShelfBootsPlaceWidth / 2,
                -buildParameters.ShelfBootsPlaceLength / 2, 
                buildParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(
                buildParameters.ShelfBootsPlaceLength / 2, 
                buildParameters.ShelfBootsPlaceWidth / 2,
                buildParameters.ShelfBootsPlaceLength / 2, 
                -buildParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(
                buildParameters.ShelfBootsPlaceLength / 2, 
                -buildParameters.ShelfBootsPlaceWidth / 2,
                -buildParameters.ShelfBootsPlaceLength / 2, 
                -buildParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(
                -buildParameters.ShelfBootsPlaceLength / 2, 
                -buildParameters.ShelfBootsPlaceWidth / 2,
                -buildParameters.ShelfBootsPlaceLength / 2, 
                buildParameters.ShelfBootsPlaceWidth / 2, 1);
        }

        /// <summary>
        /// Метод осуществляющий выдавливание эскиза.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="Sketch"></param>
        private void ExctrusionSketchNormal(double depth, ksEntity Sketch)
        {
            ksEntity entityExtr = 
                (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition entityExtrDef = 
                (ksBossExtrusionDefinition) entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = 
                (ksExtrusionParam) entityExtrDef.ExtrusionParam();
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
            ksEntity entityExtr = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition entityExtrDef = 
                (ksBossExtrusionDefinition)entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = 
                (ksExtrusionParam)entityExtrDef.ExtrusionParam();
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
            ksEntity entityExtr = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition entityExtrDef = 
                (ksCutExtrusionDefinition)entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = 
                (ksExtrusionParam)entityExtrDef.ExtrusionParam();
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
            ksEntity entInc = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_incline);
            ksInclineDefinition incDef = 
                (ksInclineDefinition)entInc.GetDefinition();
            ksEntityCollection collect = 
                (ksEntityCollection)_ksPart.EntityCollection((short)Obj3dType.o3d_face);
            incDef.direction = true;
            incDef.angle = Parameters.ShelfSlopeRadius; 
            ksEntity currentEntity = 
                (ksEntity)_ksPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            incDef.SetPlane(currentEntity); 
            ksEntityCollection entColInc = (ksEntityCollection)incDef.FaceArray();

            height = height- Parameters.ShelfBootsPlaceHeight;
            collect.SelectByPoint(0, 0, height);

            entColInc.Add(collect.GetByIndex(0));
            collect.refresh();
            entInc.Create();
        }

        /// <summary>
        /// Методя для выполнения скруглений сторон этажерки.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        private void CreateFillet(double x,double y,double z)
        {
            ksEntity entityFillet = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_fillet);
            ksFilletDefinition filletDefinition = 
                (ksFilletDefinition)entityFillet.GetDefinition();
            filletDefinition.radius = Parameters.FilletRadius;
            filletDefinition.tangent = true;
            ksEntityCollection entityCollectionPart = 
                (ksEntityCollection)_ksPart.EntityCollection((short)Obj3dType.o3d_face);
            ksEntityCollection entityCollectionFillet = 
                (ksEntityCollection)filletDefinition.array();
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
        private ksPlaneOffsetDefinition PlaneOffsetParamsSet(double offset, ksEntity tempEntity,
            ksPlaneOffsetDefinition entityDef)
        {
            entityDef.SetPlane(tempEntity);
            entityDef.direction = false;
            entityDef.offset = offset;
            return entityDef;
        }

        /// <summary>
        /// Метод создающий эскиз ножек и выдавливающий его.
        /// </summary>
        /// <param name="sketchDef"></param>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="document"></param>
        /// <param name="buildParameters"></param>
        private void BuildLegsModel(ksSketchDefinition sketchDef, ksEntity sketch, ksEntity entity,
            ksDocument2D document, Parameters buildParameters)
        {
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            document = (ksDocument2D)sketchDef.BeginEdit();
            BuildLegsSketch(buildParameters, document);
            sketchDef.EndEdit();
            ExctrusionSketchNormal(buildParameters[ParametersName.ShelfLegsHeight].Value, sketch);
        }

        /// <summary>
        /// Метод создающий эскиз полки и выдавлиющий его.
        /// </summary>
        /// <param name="sketchDef"></param>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="document"></param>
        /// <param name="buildParameters"></param>
        private void BuildShelfModel(ksSketchDefinition sketchDef, ksEntity sketch, ksEntity entity,
            ksDocument2D document, Parameters buildParameters)
        {
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            document = (ksDocument2D)sketchDef.BeginEdit();
            BuildRectangleSketch(buildParameters, document);
            sketchDef.EndEdit();
            ExctrusionSketchNormal(buildParameters[ParametersName.ShelfHeight].Value, sketch);
        }

        /// <summary>
        /// Метод создающий эскиз места для обуви и выдавливающий его.
        /// </summary>
        /// <param name="sketchDef"></param>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="document"></param>
        /// <param name="buildParameters"></param>
        private void BuildInnerPartsModel(ksSketchDefinition sketchDef, ksEntity sketch, ksEntity entity,
            ksDocument2D document, Parameters buildParameters)
        {
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            document = (ksDocument2D)sketchDef.BeginEdit();
            BuildInnerParts(buildParameters, document);
            sketchDef.EndEdit();
            CutSketch(Parameters.ShelfBootsPlaceHeight, sketch);
        }

        /// <summary>
        /// Метод создающий эскиз креплений и выдавливающий его в обратном направлении.
        /// </summary>
        /// <param name="sketchDef"></param>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="document"></param>
        /// <param name="buildParameters"></param>
        private void BuildBindingModelReverse(ksSketchDefinition sketchDef, ksEntity sketch, ksEntity entity,
            ksDocument2D document, Parameters buildParameters)
        {
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            document = (ksDocument2D)sketchDef.BeginEdit();
            BuildBindingSketch(buildParameters, document);
            sketchDef.EndEdit();
            ExctrusionSketchReverse(buildParameters[ParametersName.ShelfBindingHeight].Value, sketch);
        }

        /// <summary>
        /// Метод создающий эскиз верхних креплений и выдавливающий.
        /// </summary>
        /// <param name="sketchDef"></param>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="document"></param>
        /// <param name="buildParameters"></param>
        private void BuildBindingModelNormal(ksSketchDefinition sketchDef, ksEntity sketch, ksEntity entity,
            ksDocument2D document, Parameters buildParameters)
        {
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            document = (ksDocument2D)sketchDef.BeginEdit();
            BuildBindingSketch(buildParameters, document);
            sketchDef.EndEdit();
            ExctrusionSketchNormal(Parameters.ShelfBindingHeight, sketch);
        }

        /// <summary>
        /// Метод создающий уклоны у всех мест для хранения обуви.
        /// </summary>
        /// <param name="buildParameters"></param>
        private void BuildAllInclines(Parameters buildParameters)
        {
             //TODO: RSDN
            var ShelfLegsHeight = buildParameters[ParametersName.ShelfLegsHeight].Value;
            var ShelfHeight = buildParameters[ParametersName.ShelfHeight].Value;
            var ShelfBindingHeight = buildParameters[ParametersName.ShelfBindingHeight].Value;
            double height = ShelfLegsHeight + ShelfHeight;
            CreateIncline(buildParameters, height);
            height = ShelfLegsHeight + ShelfHeight + ShelfBindingHeight + ShelfHeight;
            CreateIncline(buildParameters, height);
            height = ShelfLegsHeight + ShelfHeight + ShelfBindingHeight + ShelfHeight + ShelfBindingHeight + ShelfHeight;
            CreateIncline(buildParameters, height);
        }

        /// <summary>
        /// Метод создающий скругления на всей этажерке.
        /// </summary>
        /// <param name="buildParameters"></param>
        private void BuildAllFillets(Parameters buildParameters)
        {
             //TODO: RSDN
            var ShelfLength = buildParameters[ParametersName.ShelfLength].Value;
            var ShelfWidth = buildParameters[ParametersName.ShelfWidth].Value;
            var BindingHeight = buildParameters[ParametersName.ShelfBindingHeight].Value;
            var LegsHeight = buildParameters[ParametersName.ShelfLegsHeight].Value;
            var ShelfHeight = buildParameters[ParametersName.ShelfHeight].Value;

            CreateFillet((ShelfLength / 2) - Parameters.RadiusMargin,
                (ShelfWidth / 2) - Parameters.RadiusMargin, 0);
            CreateFillet(-((ShelfLength / 2) - Parameters.RadiusMargin),
                ((ShelfWidth / 2) - Parameters.RadiusMargin), 0);
            CreateFillet(((ShelfLength / 2) - Parameters.RadiusMargin),
                -((ShelfWidth / 2) - Parameters.RadiusMargin), 0);
            CreateFillet(-((ShelfLength / 2) - Parameters.RadiusMargin),
                -((ShelfWidth / 2) - Parameters.RadiusMargin), 0);

            CreateFillet(0, 0, LegsHeight);
            CreateFillet(
                ShelfLength / 2, 
                ShelfWidth / 2, 
                LegsHeight + ShelfHeight);
            CreateFillet(
                ShelfLength / 2 - 2, 
                ShelfWidth / 2 - 2, 
                LegsHeight + ShelfHeight + BindingHeight);
            CreateFillet(
                ShelfLength / 2, 
                ShelfWidth / 2, 
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight);
            CreateFillet(
                ShelfLength / 2 - 2, 
                ShelfWidth / 2 - 2,
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight);
            CreateFillet(
                ShelfLength / 2, 
                ShelfWidth / 2,
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight);

            //TODO: const 10
            CreateFillet(
                (ShelfLength / 2) - Parameters.RadiusMargin,
                (ShelfWidth / 2) - Parameters.RadiusMargin,
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight + 10);
            CreateFillet(
                -((ShelfLength / 2) - Parameters.RadiusMargin),
                ((ShelfWidth / 2) - Parameters.RadiusMargin),
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight + 10);
            CreateFillet(
                ((ShelfLength / 2) - Parameters.RadiusMargin),
                -((ShelfWidth / 2) - Parameters.RadiusMargin),
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight + 10);
            CreateFillet(
                -((ShelfLength / 2) - Parameters.RadiusMargin),
                -((ShelfWidth / 2) - Parameters.RadiusMargin),
                LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight + 10);
        }
    }
}
