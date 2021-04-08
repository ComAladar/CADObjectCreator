using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        private const int DivideAmount=2;

        //TODO: RSDN
        /// <summary>
        /// Метод запускающий компас и строящий деталь.
        /// </summary>
        /// <param name="buildParameters"></param>
        public void BuildObject(Parameters buildParameters)
        {
            Kompas3DConnector kompasConnector = new Kompas3DConnector(ref _kompas, out _ksPart);
            BuildMainBody(buildParameters);
        }

        /// <summary>
        /// Метод построения этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        private void BuildMainBody(Parameters buildParameters)
        {
            ksEntity currentEntity = 
                (ksEntity) _ksPart.GetDefaultEntity((short) Obj3dType.o3d_planeXOY);
            ksEntity Sketch1 = 
                (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_sketch);
            BuildLegsModel(Sketch1,currentEntity, buildParameters);

            //TODO: Дубли
            double offsetDistance= -buildParameters[ParametersName.ShelfLegsHeight].Value; //Нужно
            ksEntity newEntity = 
                (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_planeOffset); //Нужно
            ksPlaneOffsetDefinition newEntityDefinition = 
                (ksPlaneOffsetDefinition) newEntity.GetDefinition(); // Возможно убрать но тогда надо будет передавать в метод ниже два ентити
            PlaneOffsetParamsSet(offsetDistance, currentEntity, newEntityDefinition);
            newEntity.Create();
            ksEntity Sketch2 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch); //Нужно
            BuildShelfModel(Sketch2,newEntity, buildParameters);

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
            BuildBindingModelReverse(Sketch3, newEntity1, buildParameters);

            ksEntity newEntity2 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition2 = 
                (ksPlaneOffsetDefinition)newEntity2.GetDefinition();
            newEntityDefinition2.SetPlane(newEntity1);
            newEntity2.Create();
            ksEntity Sketch4 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildShelfModel(Sketch4, newEntity2, buildParameters);

            ksEntity newEntity3 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition3 = 
                (ksPlaneOffsetDefinition)newEntity3.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity2, newEntityDefinition3);
            newEntity3.Create();
            ksEntity Sketch5 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildBindingModelReverse(Sketch5, newEntity3, buildParameters);

            ksEntity newEntity4 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition4 = 
                (ksPlaneOffsetDefinition)newEntity4.GetDefinition();
            newEntityDefinition4.SetPlane(newEntity3);
            newEntity4.Create();
            ksEntity Sketch6 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildShelfModel(Sketch6, newEntity4, buildParameters);

            offsetDistance = -buildParameters[ParametersName.ShelfHeight].Value;
            ksEntity newEntity5 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition5 = 
                (ksPlaneOffsetDefinition)newEntity5.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity4, newEntityDefinition5);
            newEntity5.Create();
            ksEntity Sketch7 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildBindingModelNormal(Sketch7,newEntity5, buildParameters);

            offsetDistance = -buildParameters[ParametersName.ShelfHeight].Value;
            ksEntity innerEntity = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset); 
            ksPlaneOffsetDefinition innerEntityDefinition = 
                (ksPlaneOffsetDefinition)innerEntity.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity, innerEntityDefinition);
            innerEntity.Create();
            ksEntity Sketch8 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildInnerPartsModel(Sketch8, innerEntity, buildParameters);

            ksEntity innerEntity1 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition1 = 
                (ksPlaneOffsetDefinition)innerEntity1.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity1, innerEntityDefinition1);
            innerEntity1.Create();
            ksEntity Sketch9 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildInnerPartsModel(Sketch9, innerEntity1, buildParameters);

            ksEntity innerEntity2 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition2 = 
                (ksPlaneOffsetDefinition)innerEntity2.GetDefinition();
            PlaneOffsetParamsSet(offsetDistance, newEntity4, innerEntityDefinition2);
            innerEntity2.Create();
            ksEntity Sketch10 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildInnerPartsModel(Sketch10, innerEntity2, buildParameters);

            BuildAllInclines(buildParameters);
            BuildAllFillets(buildParameters);


            //СОЗДАНИЕ КРЮЧКО
            ksEntity newEntity6 =
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeXOZ);
            newEntity6.Create();
            ksEntity Sketch11 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);

            ksSketchDefinition sketchDef = Sketch11.GetDefinition();
            sketchDef.SetPlane(newEntity6);
            Sketch11.Create();
            //СТРОИТЬ ТУТ
            BuildHookModel(Sketch11,newEntity6,buildParameters);
            //СТРОИТЬ ТУТ

            sketchDef.EndEdit();
            ExctrusionSketchNormal(10,Sketch11);




            //СОЗДАНИЕ КРЮЧКА
        }

        /// <summary>
        /// Метод построения эскиза прямоугольника по координатам.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildRectangleSketch(Parameters buildParameters,ksDocument2D document)
        {
            var shelfLengthDivided = 
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount;
            var shelfWidthDivided = 
                buildParameters[ParametersName.ShelfWidth].Value / DivideAmount;
            document.ksLineSeg(
                shelfLengthDivided,
                shelfWidthDivided,
                -shelfLengthDivided,
                shelfWidthDivided, 1);
            document.ksLineSeg(
                shelfLengthDivided,
                shelfWidthDivided,
                shelfLengthDivided, 
                -shelfWidthDivided, 1);
            document.ksLineSeg(
                shelfLengthDivided, 
                -shelfWidthDivided,
                -shelfLengthDivided, 
                -shelfWidthDivided, 1);
            document.ksLineSeg(
                -shelfLengthDivided, 
                -shelfWidthDivided,
                -shelfLengthDivided,
                shelfWidthDivided, 1);
        }

        /// <summary>
        /// Метод построения эскиза ножек этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildLegsSketch(Parameters buildParameters, ksDocument2D document)
        {
            var shelfLengthDivided =
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount;
            var shelfWidthDivided =
                buildParameters[ParametersName.ShelfWidth].Value / DivideAmount;

            document.ksCircle(
                (shelfLengthDivided) - Parameters.RadiusMargin,
                (shelfWidthDivided) - Parameters.RadiusMargin, 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                -((shelfLengthDivided) - Parameters.RadiusMargin),
                (shelfWidthDivided) - Parameters.RadiusMargin, 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                (shelfLengthDivided) - Parameters.RadiusMargin,
                -((shelfWidthDivided) - Parameters.RadiusMargin), 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                -((shelfLengthDivided) - Parameters.RadiusMargin),
                -((shelfWidthDivided) - Parameters.RadiusMargin), 
                Parameters.ShelfLegsRadius, 1);
        }

        /// <summary>
        /// Метод построения эскиза креплений этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildBindingSketch(Parameters buildParameters, ksDocument2D document)
        {
            var shelfLengthDivided =
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount;
            var shelfWidthDivided =
                buildParameters[ParametersName.ShelfWidth].Value / DivideAmount;

            document.ksCircle(
                (shelfLengthDivided) - Parameters.RadiusMargin,
                (shelfWidthDivided) - Parameters.RadiusMargin, 
                Parameters.ShelfBindingRadius,1);
            document.ksCircle(
                -((shelfLengthDivided) - Parameters.RadiusMargin),
                (shelfWidthDivided) - Parameters.RadiusMargin, 
                Parameters.ShelfBindingRadius,1);
            document.ksCircle(
                (shelfLengthDivided) - Parameters.RadiusMargin,
                -((shelfWidthDivided) - Parameters.RadiusMargin),
                Parameters.ShelfBindingRadius, 1);
            document.ksCircle(
                -((shelfLengthDivided) - Parameters.RadiusMargin),
                -((shelfWidthDivided) - Parameters.RadiusMargin),
                Parameters.ShelfBindingRadius, 1);
        }

        /// <summary>
        /// Метод построения эскиза отделения для обуви этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildInnerParts(Parameters buildParameters, ksDocument2D document)
        {
            var shelfBootsPlaceLengthDivided =
                buildParameters.ShelfBootsPlaceLength / DivideAmount;
            var shelfBootsPlaceWidthDivided =
                buildParameters.ShelfBootsPlaceWidth / DivideAmount;

            document.ksLineSeg(
                shelfBootsPlaceLengthDivided, 
                shelfBootsPlaceWidthDivided,
                -shelfBootsPlaceLengthDivided,
                shelfBootsPlaceWidthDivided, 1);
            document.ksLineSeg(
                shelfBootsPlaceLengthDivided,
                shelfBootsPlaceWidthDivided,
                shelfBootsPlaceLengthDivided, 
                -shelfBootsPlaceWidthDivided, 1);
            document.ksLineSeg(
                shelfBootsPlaceLengthDivided, 
                -shelfBootsPlaceWidthDivided,
                -shelfBootsPlaceLengthDivided, 
                -shelfBootsPlaceWidthDivided, 1);
            document.ksLineSeg(
                -shelfBootsPlaceLengthDivided, 
                -shelfBootsPlaceWidthDivided,
                -shelfBootsPlaceLengthDivided,
                shelfBootsPlaceWidthDivided, 1);
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
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="buildParameters"></param>
        private void BuildLegsModel(ksEntity sketch, ksEntity entity, Parameters buildParameters)
        {
            ksSketchDefinition sketchDef = sketch.GetDefinition();
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
            BuildLegsSketch(buildParameters, document);
            sketchDef.EndEdit();
            ExctrusionSketchNormal(buildParameters[ParametersName.ShelfLegsHeight].Value, sketch);
        }

        /// <summary>
        /// Метод создающий эскиз полки и выдавлиющий его.
        /// </summary>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="buildParameters"></param>
        private void BuildShelfModel(ksEntity sketch, ksEntity entity, Parameters buildParameters)
        {
            ksSketchDefinition sketchDef = sketch.GetDefinition();
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
            BuildRectangleSketch(buildParameters, document);
            sketchDef.EndEdit();
            ExctrusionSketchNormal(buildParameters[ParametersName.ShelfHeight].Value, sketch);
        }

        /// <summary>
        /// Метод создающий эскиз места для обуви и выдавливающий его.
        /// </summary>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="buildParameters"></param>
        private void BuildInnerPartsModel(ksEntity sketch, ksEntity entity, Parameters buildParameters)
        {
            ksSketchDefinition sketchDef = sketch.GetDefinition();
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
            BuildInnerParts(buildParameters, document);
            sketchDef.EndEdit();
            CutSketch(Parameters.ShelfBootsPlaceHeight, sketch);
        }

        /// <summary>
        /// Метод создающий эскиз креплений и выдавливающий его в обратном направлении.
        /// </summary>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="buildParameters"></param>
        private void BuildBindingModelReverse(ksEntity sketch, ksEntity entity, Parameters buildParameters)
        {
            ksSketchDefinition sketchDef = sketch.GetDefinition();
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
            BuildBindingSketch(buildParameters, document);
            sketchDef.EndEdit();
            ExctrusionSketchReverse(buildParameters[ParametersName.ShelfBindingHeight].Value, sketch);
        }

        /// <summary>
        /// Метод создающий эскиз верхних креплений и выдавливающий.
        /// </summary>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="buildParameters"></param>
        private void BuildBindingModelNormal( ksEntity sketch, ksEntity entity, Parameters buildParameters)
        {
            ksSketchDefinition sketchDef = sketch.GetDefinition();
            sketchDef.SetPlane(entity);
            sketch.Create();
            //TODO:
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
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
            var shelfLegsHeight = 
                buildParameters[ParametersName.ShelfLegsHeight].Value;
            var shelfHeight = 
                buildParameters[ParametersName.ShelfHeight].Value;
            var shelfBindingHeight = 
                buildParameters[ParametersName.ShelfBindingHeight].Value;
            double height = shelfLegsHeight + shelfHeight;
            CreateIncline(buildParameters, height);
            height = shelfLegsHeight + shelfHeight + shelfBindingHeight + shelfHeight;
            CreateIncline(buildParameters, height);
            height = shelfLegsHeight + shelfHeight + 
                     shelfBindingHeight + shelfHeight + shelfBindingHeight + shelfHeight;
            CreateIncline(buildParameters, height);
        }

        /// <summary>
        /// Метод создающий скругления на всей этажерке.
        /// </summary>
        /// <param name="buildParameters"></param>
        private void BuildAllFillets(Parameters buildParameters)
        {
            //TODO: RSDN
            int addRange = 10;
            int subRange = 2;
            var shelfLengthDivided = 
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount;
            var shelfWidthDivided = 
                buildParameters[ParametersName.ShelfWidth].Value / DivideAmount;
            var bindingHeight = 
                buildParameters[ParametersName.ShelfBindingHeight].Value;
            var legsHeight = 
                buildParameters[ParametersName.ShelfLegsHeight].Value;
            var shelfHeight = 
                buildParameters[ParametersName.ShelfHeight].Value;

            CreateFillet((shelfLengthDivided) - Parameters.RadiusMargin,
                (shelfWidthDivided) - Parameters.RadiusMargin, 0);
            CreateFillet(-((shelfLengthDivided) - Parameters.RadiusMargin),
                ((shelfWidthDivided) - Parameters.RadiusMargin), 0);
            CreateFillet(((shelfLengthDivided) - Parameters.RadiusMargin),
                -((shelfWidthDivided) - Parameters.RadiusMargin), 0);
            CreateFillet(-((shelfLengthDivided) - Parameters.RadiusMargin),
                -((shelfWidthDivided) - Parameters.RadiusMargin), 0);

            CreateFillet(0, 0, legsHeight);
            CreateFillet(
                shelfLengthDivided, 
                shelfWidthDivided, 
                legsHeight + shelfHeight);
            CreateFillet(
                shelfLengthDivided - subRange, 
                shelfWidthDivided - subRange, 
                legsHeight + shelfHeight + bindingHeight);
            CreateFillet(
                shelfLengthDivided, 
                shelfWidthDivided, 
                legsHeight + shelfHeight + bindingHeight + shelfHeight);
            CreateFillet(
                shelfLengthDivided - subRange, 
                shelfWidthDivided - subRange,
                legsHeight + shelfHeight + bindingHeight + 
                shelfHeight + bindingHeight);
            CreateFillet(
                shelfLengthDivided, 
                shelfWidthDivided,
                legsHeight + shelfHeight + bindingHeight + 
                shelfHeight + bindingHeight + shelfHeight);

            //TODO: const 10
            CreateFillet(
                (shelfLengthDivided) - Parameters.RadiusMargin,
                (shelfWidthDivided) - Parameters.RadiusMargin,
                legsHeight + shelfHeight + bindingHeight + 
                shelfHeight + bindingHeight + shelfHeight + addRange);
            CreateFillet(
                -((shelfLengthDivided) - Parameters.RadiusMargin),
                ((shelfWidthDivided) - Parameters.RadiusMargin),
                legsHeight + shelfHeight + bindingHeight + 
                shelfHeight + bindingHeight + shelfHeight + addRange);
            CreateFillet(
                ((shelfLengthDivided) - Parameters.RadiusMargin),
                -((shelfWidthDivided) - Parameters.RadiusMargin),
                legsHeight + shelfHeight + bindingHeight + 
                shelfHeight + bindingHeight + shelfHeight + addRange);
            CreateFillet(
                -((shelfLengthDivided) - Parameters.RadiusMargin),
                -((shelfWidthDivided) - Parameters.RadiusMargin),
                legsHeight + shelfHeight + bindingHeight + 
                shelfHeight + bindingHeight + shelfHeight + addRange);
        }

        private void BuildHookModel(ksEntity sketch,ksEntity entity,Parameters buildParameters)
        {
            ksSketchDefinition sketchDef = sketch.GetDefinition();
            sketchDef.SetPlane(entity);
            sketch.Create();
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
            BuildHookSketch(buildParameters,document);
            sketchDef.EndEdit();
            ExctrusionSketchNormal(10, sketch);
        }

        private void BuildHookSketch(Parameters buildParameters, ksDocument2D document)
        {

            double offsetDistance = buildParameters[ParametersName.ShelfLegsHeight].Value
                                    + buildParameters[ParametersName.ShelfHeight].Value
                                    + buildParameters[ParametersName.ShelfBindingHeight].Value
                                    + buildParameters[ParametersName.ShelfHeight].Value
                                    + buildParameters[ParametersName.ShelfBindingHeight].Value
                                    + buildParameters[ParametersName.ShelfHeight].Value / DivideAmount;

            var shelfLengthDivided =
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount;
            double hookLength = 10;
            double hookWidth = 10;

            //ПАРАЛЛЕЛЬНЫЕ
            document.ksLineSeg(
                shelfLengthDivided,
                -offsetDistance + hookLength,
                shelfLengthDivided,
                -offsetDistance - hookLength, 1);
            document.ksLineSeg(
                shelfLengthDivided + hookWidth,
                -offsetDistance + hookLength,
                shelfLengthDivided + hookWidth,
                -offsetDistance - hookLength, 1);

            //ВЕРХНИЕ
            document.ksLineSeg(
                shelfLengthDivided,
                -offsetDistance + hookLength,
                shelfLengthDivided + hookWidth,
                -offsetDistance + hookLength, 1);

            document.ksLineSeg(
                shelfLengthDivided,
                -offsetDistance - hookLength,
                shelfLengthDivided + hookWidth,
                -offsetDistance - hookLength, 1);

            //document.ksArcBy3Points(shelfLengthDivided + hookWidth / 2,
            //    -offsetDistance + hookLength / 2, shelfLengthDivided + hookWidth,
            //    -offsetDistance + hookLength * 2, shelfLengthDivided + hookWidth * 2,
            //       -offsetDistance + hookLength, 1);
            //document.ksArcBy3Points(shelfLengthDivided + hookWidth * 2,
            //    -offsetDistance + hookLength, shelfLengthDivided + hookWidth * 0.5,
            //    -offsetDistance + hookLength * 1.5, shelfLengthDivided + hookWidth / 2,
            //    -offsetDistance + hookLength / 2, 1);

            document.ksArcBy3Points(shelfLengthDivided,
            -offsetDistance + hookLength, shelfLengthDivided + hookWidth / 2,
                -offsetDistance + hookLength * 1.5, shelfLengthDivided + hookWidth * 2,
                   -offsetDistance + hookLength, 1);

            document.ksArcBy3Points(shelfLengthDivided + hookWidth * 2,
                -offsetDistance + hookLength, shelfLengthDivided + hookWidth * 1.15,
                -offsetDistance + hookLength*1.15, shelfLengthDivided + hookWidth,
                -offsetDistance + hookLength, 1);
        }
    }
}
