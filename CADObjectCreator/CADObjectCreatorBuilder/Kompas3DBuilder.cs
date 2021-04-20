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

        /// <summary>
        /// Поле для хранения значения нахождения центров сторон.
        /// </summary>
        private const int DivideAmount = 2;

        /// <summary>
        /// Поле для хранения размеров дуги крючков по x.
        /// </summary>
        private const double multiplyPoint2xAmount = 1.15;

        /// <summary>
        /// Поле для хранения размеров дуги крючков по y.
        /// </summary>
        private const double multiplyPoint2yAmount = 1.35;

        /// <summary>
        /// Поле для хранения отсутпа крючка.
        /// </summary>
        private const double addAmount = 5;

        /// <summary>
        /// Поле для хранения значения нахождения двойного отсупа крючка.
        /// </summary>
        private const double multiplyAmount = 2;

        /// <summary>
        /// Метод запускающий компас и строящий деталь.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="leftHook"></param>
        /// <param name="rightHook"></param>
        public void BuildObject(Parameters buildParameters,bool leftHook,bool rightHook)
        {
            Kompas3DConnector kompasConnector = new Kompas3DConnector(ref _kompas, out _ksPart);
            BuildMainBody(buildParameters, leftHook, rightHook);
        }

        /// <summary>
        /// Метод построения этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="leftHook"></param>
        /// <param name="rightHook"></param>
        private void BuildMainBody(Parameters buildParameters,bool leftHook, bool rightHook)
        {
            ksEntity currentEntity = 
                (ksEntity) _ksPart.GetDefaultEntity((short) Obj3dType.o3d_planeXOY);
            ksEntity sketch1 = 
                (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_sketch);
            BuildLegsModel(sketch1,currentEntity, buildParameters);

            //TODO: Дубли
            double offsetDistance= -buildParameters[ParametersName.ShelfLegsHeight].Value;
            ksEntity newEntity = CreateEntity(offsetDistance, currentEntity);
            //TODO: RSDN
            ksEntity sketch2 = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch); 
            BuildShelfModel(sketch2,newEntity, buildParameters);

            offsetDistance= 
                -buildParameters[ParametersName.ShelfBindingHeight].Value 
                - buildParameters[ParametersName.ShelfHeight].Value;
            ksEntity newEntity1 = CreateEntity(offsetDistance, newEntity);
            ksEntity sketch3 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildBindingModelReverse(sketch3, newEntity1, buildParameters);

            ksEntity newEntity2 = CreateEntity(0, newEntity1);
            ksEntity sketch4 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildShelfModel(sketch4, newEntity2, buildParameters);

            ksEntity newEntity3 = CreateEntity(offsetDistance, newEntity2);
            ksEntity sketch5 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildBindingModelReverse(sketch5, newEntity3, buildParameters);

            ksEntity newEntity4 = CreateEntity(0, newEntity3);
            ksEntity sketch6 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildShelfModel(sketch6, newEntity4, buildParameters);

            offsetDistance = -buildParameters[ParametersName.ShelfHeight].Value;
            ksEntity newEntity5 = CreateEntity(offsetDistance, newEntity4);
            ksEntity sketch7 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildBindingModelNormal(sketch7,newEntity5, buildParameters);

            offsetDistance = -buildParameters[ParametersName.ShelfHeight].Value;
            ksEntity innerEntity = CreateEntity(offsetDistance, newEntity);
            ksEntity sketch8 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildInnerPartsModel(sketch8, innerEntity, buildParameters);

            ksEntity innerEntity1 = CreateEntity(offsetDistance, newEntity1);
            ksEntity sketch9 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildInnerPartsModel(sketch9, innerEntity1, buildParameters);

            ksEntity innerEntity2 = CreateEntity(offsetDistance, newEntity4);
            ksEntity sketch10 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            BuildInnerPartsModel(sketch10, innerEntity2, buildParameters);

            BuildAllInclines(buildParameters);
            BuildAllFillets(buildParameters);

            if (rightHook == true || leftHook == true)
            {
                ksEntity newEntity6 =
                    (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeXOZ);
                newEntity6.Create();

                ksEntity sketch11 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
                ksSketchDefinition sketchDef = sketch11.GetDefinition();
                sketchDef.SetPlane(newEntity6);
                sketch11.Create();

                BuildHookModel(sketch11, newEntity6, buildParameters,leftHook,rightHook);
            }
        }

        /// <summary>
        /// Метод построения эскиза прямоугольника по координатам.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="sketchDef"></param>
        private void BuildRectangleSketch(Parameters buildParameters,ksSketchDefinition sketchDef)
        {
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
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
        private void BuildLegsSketch(Parameters buildParameters, ksSketchDefinition sketchDef)
        {
            //TODO: Duplication
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
            var shelfLengthDivided =
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount - Parameters.RadiusMargin;
            var shelfWidthDivided =
                buildParameters[ParametersName.ShelfWidth].Value / DivideAmount - Parameters.RadiusMargin;

            document.ksCircle(
                shelfLengthDivided,
                shelfWidthDivided, 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                -shelfLengthDivided,
                shelfWidthDivided, 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                shelfLengthDivided,
                -shelfWidthDivided, 
                Parameters.ShelfLegsRadius, 1);
            document.ksCircle(
                -shelfLengthDivided,
                -shelfWidthDivided, 
                Parameters.ShelfLegsRadius, 1);
        }

        /// <summary>
        /// Метод построения эскиза креплений этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildBindingSketch(Parameters buildParameters, ksSketchDefinition sketchDef)
        {
            //TODO: Duplication
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
            var shelfLengthDivided =
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount - Parameters.RadiusMargin;
            var shelfWidthDivided =
                buildParameters[ParametersName.ShelfWidth].Value / DivideAmount - Parameters.RadiusMargin;

            document.ksCircle(
                shelfLengthDivided,
                shelfWidthDivided, 
                Parameters.ShelfBindingRadius,1);
            document.ksCircle(
                -shelfLengthDivided,
                shelfWidthDivided, 
                Parameters.ShelfBindingRadius,1);
            document.ksCircle(
                shelfLengthDivided,
                -shelfWidthDivided,
                Parameters.ShelfBindingRadius, 1);
            document.ksCircle(
                -shelfLengthDivided,
                -shelfWidthDivided,
                Parameters.ShelfBindingRadius, 1);
        }

        /// <summary>
        /// Метод построения эскиза отделения для обуви этажерки.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        private void BuildInnerParts(Parameters buildParameters, ksSketchDefinition sketchDef)
        {
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
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
        /// <param name="sketch"></param>
        private void ExctrusionSketchNormal(double depth, ksEntity sketch)
        {
            ksEntity entityExtr = 
                (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition entityExtrDef = 
                (ksBossExtrusionDefinition) entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = 
                (ksExtrusionParam) entityExtrDef.ExtrusionParam();
            entityExtrDef.SetSketch(sketch);
            entityExtrParam.direction = (short) Direction_Type.dtNormal;
            entityExtrParam.typeNormal = (short) End_Type.etBlind;
            entityExtrParam.depthNormal = depth;
            entityExtr.Create();
        }

        /// <summary>
        /// Метод осуществляющий выдавливание эскиза в обратную сторону.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="sketch"></param>
        private void ExctrusionSketchReverse(double depth, ksEntity sketch)
        {
            ksEntity entityExtr = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition entityExtrDef = 
                (ksBossExtrusionDefinition)entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = 
                (ksExtrusionParam)entityExtrDef.ExtrusionParam();
            entityExtrDef.SetSketch(sketch);
            entityExtrParam.direction = (short)Direction_Type.dtReverse;
            entityExtrParam.typeNormal = (short)End_Type.etBlind;
            entityExtrParam.depthReverse = depth;
            entityExtr.Create();
        }

        /// <summary>
        /// Метод осуществляющий выдавливание эскиза вырезанием.
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="sketch"></param>
        private void CutSketch(double depth, ksEntity sketch)
        {
            ksEntity entityExtr = 
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition entityExtrDef = 
                (ksCutExtrusionDefinition)entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = 
                (ksExtrusionParam)entityExtrDef.ExtrusionParam();
            entityExtrDef.SetSketch(sketch);
            entityExtrParam.direction = (short)Direction_Type.dtNormal;
            entityExtrParam.typeNormal = (short)End_Type.etBlind;
            entityExtrParam.depthNormal = depth;
            entityExtr.Create();
        }

        /// <summary>
        /// Метод для создании уклона отделения для обуви этажерки.
        /// </summary>
        /// <param name="height"></param>
        private void CreateIncline(double height)
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
        /// <param name="offset"></param>
        /// <param name="tempEntity"></param>
        /// <param name="entityDef"></param>
        /// <returns></returns>
        private ksPlaneOffsetDefinition PlaneOffsetParamsSet(double offset, ksEntity tempEntity,
            ksEntity mainEntity)
        {
            ksPlaneOffsetDefinition entityDef = (ksPlaneOffsetDefinition)mainEntity.GetDefinition();
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
            ksSketchDefinition sketchDef = CreateSketchDef(sketch, entity);
            //TODO:
            BuildLegsSketch(buildParameters, sketchDef);
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
            ksSketchDefinition sketchDef = CreateSketchDef(sketch, entity);

            BuildRectangleSketch(buildParameters, sketchDef);
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
            ksSketchDefinition sketchDef = CreateSketchDef(sketch, entity);

            BuildInnerParts(buildParameters, sketchDef);
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
            ksSketchDefinition sketchDef = CreateSketchDef(sketch, entity);

            BuildBindingSketch(buildParameters, sketchDef);
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
            ksSketchDefinition sketchDef = CreateSketchDef(sketch, entity);

            BuildBindingSketch(buildParameters, sketchDef);
            sketchDef.EndEdit();
            ExctrusionSketchNormal(Parameters.ShelfBindingHeight, sketch);
        }

        /// <summary>
        /// Метод создающий уклоны у всех мест для хранения обуви.
        /// </summary>
        /// <param name="buildParameters"></param>
        private void BuildAllInclines(Parameters buildParameters)
        {
            var shelfLegsHeight = 
                buildParameters[ParametersName.ShelfLegsHeight].Value;
            var shelfHeight = 
                buildParameters[ParametersName.ShelfHeight].Value;
            var shelfBindingHeight = 
                buildParameters[ParametersName.ShelfBindingHeight].Value;
            double height = shelfLegsHeight + shelfHeight;
            CreateIncline(height);
            height = shelfLegsHeight + shelfHeight + shelfBindingHeight + shelfHeight;
            CreateIncline(height);
            height = shelfLegsHeight + shelfHeight + 
                     shelfBindingHeight + shelfHeight + shelfBindingHeight + shelfHeight;
            CreateIncline(height);
        }

        /// <summary>
        /// Метод создающий скругления на всей этажерке.
        /// </summary>
        /// <param name="buildParameters"></param>
        private void BuildAllFillets(Parameters buildParameters)
        {
            const int subAmount = 2;
            //TODO: Duplication
            var shelfLengthDivided = 
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount - Parameters.RadiusMargin;
            var shelfWidthDivided = 
                buildParameters[ParametersName.ShelfWidth].Value / DivideAmount - Parameters.RadiusMargin;
            var bindingHeight = 
                buildParameters[ParametersName.ShelfBindingHeight].Value;
            var legsHeight = 
                buildParameters[ParametersName.ShelfLegsHeight].Value;
            var shelfHeight = 
                buildParameters[ParametersName.ShelfHeight].Value;

            CreateFillet(shelfLengthDivided, shelfWidthDivided, 0);
            CreateFillet(-shelfLengthDivided, shelfWidthDivided, 0);
            CreateFillet(shelfLengthDivided, -shelfWidthDivided, 0);
            CreateFillet(-shelfLengthDivided,
                -shelfWidthDivided, 0);

            const int addAmount = 10;
            CreateFillet(
                (shelfLengthDivided),
                (shelfWidthDivided),
                legsHeight + shelfHeight + bindingHeight +
                shelfHeight + bindingHeight + shelfHeight + addAmount);
            CreateFillet(
                -((shelfLengthDivided)),
                ((shelfWidthDivided)),
                legsHeight + shelfHeight + bindingHeight +
                shelfHeight + bindingHeight + shelfHeight + addAmount);
            CreateFillet(
                ((shelfLengthDivided)),
                -((shelfWidthDivided)),
                legsHeight + shelfHeight + bindingHeight +
                shelfHeight + bindingHeight + shelfHeight + addAmount);
            CreateFillet(
                -((shelfLengthDivided)),
                -((shelfWidthDivided)),
                legsHeight + shelfHeight + bindingHeight +
                shelfHeight + bindingHeight + shelfHeight + addAmount);

             shelfLengthDivided =
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount;
             shelfWidthDivided =
                buildParameters[ParametersName.ShelfWidth].Value / DivideAmount;

            CreateFillet(0, 0, legsHeight);
            CreateFillet(
                shelfLengthDivided, 
                shelfWidthDivided, 
                legsHeight + shelfHeight);
            CreateFillet(
                shelfLengthDivided - subAmount, 
                shelfWidthDivided - subAmount, 
                legsHeight + shelfHeight + bindingHeight);
            CreateFillet(
                shelfLengthDivided, 
                shelfWidthDivided, 
                legsHeight + shelfHeight + bindingHeight + shelfHeight);
            CreateFillet(
                shelfLengthDivided - subAmount, 
                shelfWidthDivided - subAmount,
                legsHeight + shelfHeight + bindingHeight + 
                shelfHeight + bindingHeight);
            CreateFillet(
                shelfLengthDivided, 
                shelfWidthDivided,
                legsHeight + shelfHeight + bindingHeight + 
                shelfHeight + bindingHeight + shelfHeight);
        }

        /// <summary>
        /// Выполняет построение крючка.
        /// </summary>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <param name="buildParameters"></param>
        /// <param name="leftHook"></param>
        /// <param name="rightHook"></param>
        private void BuildHookModel(ksEntity sketch,ksEntity entity,
            Parameters buildParameters,bool leftHook,bool rightHook)
        {
            ksSketchDefinition sketchDef = sketch.GetDefinition();
            sketchDef.SetPlane(entity);
            sketch.Create();
            ksDocument2D document = (ksDocument2D)sketchDef.BeginEdit();
            if (leftHook)
            {
                BuildHookSketchLeft(buildParameters, document);
            }
            if (rightHook)
            {
                BuildHookSketchRight(buildParameters, document);
            }
            sketchDef.EndEdit();
            ExctrusionSketchNormal(10, sketch);
        }

        /// <summary>
        /// Выполняет построение левого эскиза крючка.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        /// <param name="sketchDef"></param>
        private void BuildHookSketchLeft(Parameters buildParameters, ksDocument2D document)
        {
            double offsetDistance = buildParameters[ParametersName.ShelfLegsHeight].Value
                                    + buildParameters[ParametersName.ShelfHeight].Value
                                    + buildParameters[ParametersName.ShelfBindingHeight].Value
                                    + buildParameters[ParametersName.ShelfHeight].Value
                                    + buildParameters[ParametersName.ShelfBindingHeight].Value
                                    + buildParameters[ParametersName.ShelfHeight].Value / DivideAmount;
            var shelfLengthDivided =
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount;
            double hookLength = buildParameters[ParametersName.ShelfHeight].Value / DivideAmount;
            const double hookWidth = 5;
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

            document.ksLineSeg(
                shelfLengthDivided,
                -offsetDistance - hookLength,
                shelfLengthDivided + hookWidth,
                -offsetDistance - hookLength, 1);

            //TODO: magic...
            document.ksArcBy3Points(shelfLengthDivided + hookWidth * multiplyAmount,
                -offsetDistance + hookLength, shelfLengthDivided + hookWidth * multiplyPoint2xAmount,
                -offsetDistance + hookLength * multiplyPoint2yAmount, shelfLengthDivided + hookWidth,
                -offsetDistance + hookLength, 1);
  
            document.ksLineSeg(
                shelfLengthDivided + hookWidth * multiplyAmount,
                -offsetDistance + hookLength,
                shelfLengthDivided + hookWidth * multiplyAmount + addAmount,
                -offsetDistance + hookLength, 1);

            document.ksArcBy3Points(shelfLengthDivided,
                -offsetDistance + hookLength, shelfLengthDivided + hookWidth,
                -offsetDistance + hookLength * multiplyAmount, 
                shelfLengthDivided + hookWidth * multiplyAmount + addAmount,
                -offsetDistance + hookLength, 1);
        }

        /// <summary>
        /// Выполняет построение правого эскиза крючка.
        /// </summary>
        /// <param name="buildParameters"></param>
        /// <param name="document"></param>
        /// <param name="sketchDef"></param>
        private void BuildHookSketchRight(Parameters buildParameters, ksDocument2D document)
        {
            double offsetDistance = -(buildParameters[ParametersName.ShelfLegsHeight].Value
                                      + buildParameters[ParametersName.ShelfHeight].Value
                                      + buildParameters[ParametersName.ShelfBindingHeight].Value
                                      + buildParameters[ParametersName.ShelfHeight].Value
                                      + buildParameters[ParametersName.ShelfBindingHeight].Value
                                      + buildParameters[ParametersName.ShelfHeight].Value / DivideAmount);
            var shelfLengthDivided =
                buildParameters[ParametersName.ShelfLength].Value / DivideAmount;
            double hookLength = buildParameters[ParametersName.ShelfHeight].Value / DivideAmount;
            const double hookWidth = 5;

            document.ksLineSeg(
                -shelfLengthDivided,
                offsetDistance + hookLength,
                -shelfLengthDivided,
                offsetDistance - hookLength, 1);

            document.ksLineSeg(
                -(shelfLengthDivided + hookWidth),
                offsetDistance + hookLength,
                -(shelfLengthDivided + hookWidth),
                offsetDistance - hookLength, 1);

            document.ksLineSeg(
                -shelfLengthDivided,
                offsetDistance - hookLength,
                -(shelfLengthDivided + hookWidth),
                offsetDistance - hookLength, 1);
            //TODO: magic...

            document.ksArcBy3Points(-(shelfLengthDivided + hookWidth * multiplyAmount),
                offsetDistance + hookLength, -(shelfLengthDivided + hookWidth * multiplyPoint2xAmount),
                offsetDistance + hookLength * multiplyPoint2yAmount, -(shelfLengthDivided + hookWidth),
                offsetDistance + hookLength, 1);

            document.ksLineSeg(
                -(shelfLengthDivided + hookWidth * multiplyAmount),
                offsetDistance + hookLength,
                -(shelfLengthDivided + hookWidth * multiplyAmount + addAmount),
                offsetDistance + hookLength, 1);

            document.ksArcBy3Points(-shelfLengthDivided,
                offsetDistance + hookLength, -(shelfLengthDivided + hookWidth),
                offsetDistance + hookLength * multiplyAmount, 
                -(shelfLengthDivided + hookWidth * multiplyAmount + addAmount),
                offsetDistance + hookLength, 1);
        }

        /// <summary>
        /// Создает ksEntity смешненной от плоскости.
        /// </summary>
        /// <param name="offsetDistance"></param>
        /// <param name="tempEntity"></param>
        /// <returns></returns>
        private ksEntity CreateEntity(double offsetDistance, ksEntity tempEntity)
        {
            ksEntity mainEntity =
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            PlaneOffsetParamsSet(offsetDistance, tempEntity, mainEntity);
            mainEntity.Create();
            return mainEntity;
        }

        /// <summary>
        /// Создает ksSketchDefinition смешенной от плоскости.
        /// </summary>
        /// <param name="sketch"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private ksSketchDefinition CreateSketchDef(ksEntity sketch,ksEntity entity)
        {
            ksSketchDefinition sketchDef = sketch.GetDefinition();
            sketchDef.SetPlane(entity);
            sketch.Create();
            return sketchDef;
        }

    }
}
