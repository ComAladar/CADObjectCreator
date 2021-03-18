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
            Kompas3DConnector KompasConnector = new Kompas3DConnector(ref _kompas,out _ksPart);
            BuildMainBody(BuildParameters);
        }

        private void BuildMainBody(Parameters buildParameters)
        {
            //ПЕРВАЯ ЧАСТЬ 
            ksEntity currentEntity = (ksEntity) _ksPart.GetDefaultEntity((short) Obj3dType.o3d_planeXOY);
            ksDocument2D document2D;

            ksEntity Sketch1 = (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch1Def = Sketch1.GetDefinition();
            Sketch1Def.SetPlane(currentEntity);
            Sketch1.Create();

            document2D = (ksDocument2D) Sketch1Def.BeginEdit();
            BuildLegsSketch(buildParameters,document2D);
            Sketch1Def.EndEdit();

            ExctrusionSketch(buildParameters.ParametersList["ShelfLegsHeight"],Sketch1,true);
            //КОНЕЦ ПЕРВОЙ ЧАСТИ

            //ВТОРАЯ ЧАСТЬ* 
            ksEntity newEntity = (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition = (ksPlaneOffsetDefinition) newEntity.GetDefinition();
            newEntityDefinition.SetPlane(currentEntity);
            newEntityDefinition.direction = false;
            newEntityDefinition.offset = -buildParameters.ParametersList["ShelfLegsHeight"];
            newEntity.Create();

            ksEntity Sketch2 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch2Def = Sketch2.GetDefinition();
            Sketch2Def.SetPlane(newEntity);
            Sketch2.Create();

            document2D = (ksDocument2D) Sketch2Def.BeginEdit();
            BuildRectangleSketch(buildParameters,document2D);
            Sketch2Def.EndEdit();

            ExctrusionSketch(buildParameters.ParametersList["ShelfHeight"],Sketch2,true);
            //КОНЕЦ ВТОРОЙ ЧАСТИ

            //ТРЕТЬЯ ЧАСТЬ 
            ksEntity newEntity1 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition1 = (ksPlaneOffsetDefinition)newEntity1.GetDefinition();
            newEntityDefinition1.SetPlane(newEntity);
            newEntityDefinition1.direction = false;
            newEntityDefinition1.offset = -buildParameters.ParametersList["ShelfBindingHeight"]-buildParameters.ParametersList["ShelfHeight"];
            newEntity1.Create();

            ksEntity Sketch3 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch3Def = Sketch3.GetDefinition();
            Sketch3Def.SetPlane(newEntity1);
            Sketch3.Create();

            document2D = (ksDocument2D)Sketch3Def.BeginEdit();
            BuildBindingSketch(buildParameters, document2D);
            Sketch3Def.EndEdit();

            ExctrusionSketch(buildParameters.ParametersList["ShelfBindingHeight"], Sketch3, false);
            //КОНЕЦ ТРЕТЬЕЙ ЧАСТИ

            //ЧЕТВЕРТАЯ ЧАСТЬ* 
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

            ExctrusionSketch(buildParameters.ParametersList["ShelfHeight"],Sketch4,true);
            //КОНЕЦ ЧЕТВЕРТОЙ ЧАСТИ

            //ПЯТАЯ ЧАСТЬ 
            ksEntity newEntity3 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition3 = (ksPlaneOffsetDefinition)newEntity3.GetDefinition();
            newEntityDefinition3.SetPlane(newEntity2);
            newEntityDefinition3.direction = false;
            newEntityDefinition3.offset = -buildParameters.ParametersList["ShelfBindingHeight"]-buildParameters.ParametersList["ShelfHeight"];
            newEntity3.Create();

            ksEntity Sketch5 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch5Def = Sketch5.GetDefinition();
            Sketch5Def.SetPlane(newEntity3);
            Sketch5.Create();

            document2D = (ksDocument2D)Sketch5Def.BeginEdit();
            BuildBindingSketch(buildParameters, document2D);
            Sketch5Def.EndEdit();

            ExctrusionSketch(buildParameters.ParametersList["ShelfBindingHeight"], Sketch5, false);
            //КОНЕЦ ПЯТОЙ ЧАСТИ

            //ШЕСТАЯ ЧАСТЬ*
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

            ExctrusionSketch(buildParameters.ParametersList["ShelfHeight"], Sketch6, true);
            //КОНЕЦ ШЕСТАЯ ЧАСТЬ

            //СЕДЬМАЯ ЧАСТЬ
            ksEntity newEntity5 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition newEntityDefinition5 = (ksPlaneOffsetDefinition)newEntity5.GetDefinition();
            newEntityDefinition5.SetPlane(newEntity4);
            newEntityDefinition5.direction = false;
            newEntityDefinition5.offset = -buildParameters.ParametersList["ShelfHeight"];
            newEntity5.Create();

            ksEntity Sketch7 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch7Def = Sketch7.GetDefinition();
            Sketch7Def.SetPlane(newEntity5);
            Sketch7.Create();

            document2D = (ksDocument2D)Sketch7Def.BeginEdit();
            BuildBindingSketch(buildParameters, document2D);
            Sketch7Def.EndEdit();

            ExctrusionSketch(10, Sketch7, true);
            //КОНЕЦ СЕДЬМОЙ ЧАСТИ

            //ПЕРВОЕ ОТВЕРСТИЕ
            ksEntity innerEntity = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition = (ksPlaneOffsetDefinition)innerEntity.GetDefinition();
            innerEntityDefinition.SetPlane(newEntity);
            innerEntityDefinition.direction = false;
            innerEntityDefinition.offset = -buildParameters.ParametersList["ShelfHeight"];
            innerEntity.Create();

            ksEntity Sketch8 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch8Def = Sketch8.GetDefinition();
            Sketch8Def.SetPlane(innerEntity);
            Sketch8.Create();

            document2D = (ksDocument2D)Sketch8Def.BeginEdit();
            BuildInnerParts(buildParameters, document2D);
            Sketch8Def.EndEdit();

            CutSketch(10, Sketch8, true);

            //ПЕРВОЕ ОТВЕРСТИЕ

            //ВТОРОЕ ОТВЕРСТИЕ
            ksEntity innerEntity1 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition1 = (ksPlaneOffsetDefinition)innerEntity1.GetDefinition();
            innerEntityDefinition1.SetPlane(newEntity1);
            innerEntityDefinition1.direction = false;
            innerEntityDefinition1.offset = -buildParameters.ParametersList["ShelfHeight"];
            innerEntity1.Create();

            ksEntity Sketch9 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch9Def = Sketch9.GetDefinition();
            Sketch9Def.SetPlane(innerEntity1);
            Sketch9.Create();

            document2D = (ksDocument2D)Sketch9Def.BeginEdit();
            BuildInnerParts(buildParameters, document2D);
            Sketch9Def.EndEdit();

            CutSketch(10,Sketch9,true);
            
            //ВТОРОЕ ОТВЕРСТИЕ

            //ТРЕТЬЕ ОТВЕРСТИЕ
            ksEntity innerEntity2 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_planeOffset);
            ksPlaneOffsetDefinition innerEntityDefinition2 = (ksPlaneOffsetDefinition)innerEntity2.GetDefinition();
            innerEntityDefinition2.SetPlane(newEntity4);
            innerEntityDefinition2.direction = false;
            innerEntityDefinition2.offset = -buildParameters.ParametersList["ShelfHeight"];
            innerEntity2.Create();

            ksEntity Sketch10 = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_sketch);
            ksSketchDefinition Sketch10Def = Sketch10.GetDefinition();
            Sketch10Def.SetPlane(innerEntity2);
            Sketch10.Create();

            document2D = (ksDocument2D)Sketch10Def.BeginEdit();
            BuildInnerParts(buildParameters, document2D);
            Sketch10Def.EndEdit();

            CutSketch(10, Sketch10, true);

            //ТРЕТЬЕ ОТВЕРСТИЕ

            //СОЗДАНИЕ УКЛОНОВ
            CreateIncline(buildParameters, 0);
            CreateIncline(buildParameters, 1);
            CreateIncline(buildParameters, 2);
            //СОЗДАНИЕ УКЛОНОВ

            //СОЗДАНИЕ СКРУГЛЕНИЙ
            var ShelfLength = buildParameters.ParametersList["ShelfLength"];
            var ShelfWidth = buildParameters.ParametersList["ShelfWidth"];
            var BindingHeight = buildParameters.ParametersList["ShelfBindingHeight"];
            var LegsHeight = buildParameters.ParametersList["ShelfLegsHeight"];
            var ShelfHeight = buildParameters.ParametersList["ShelfHeight"];

            CreateFillet(buildParameters,(ShelfLength/2)-21.5,(ShelfWidth/2)-21.5,0);
            CreateFillet(buildParameters, -((ShelfLength / 2) - 21.5), ((ShelfWidth / 2) - 21.5), 0);
            CreateFillet(buildParameters, ((ShelfLength / 2) - 21.5), -((ShelfWidth / 2) - 21.5), 0);
            CreateFillet(buildParameters, -((ShelfLength / 2) - 21.5), -((ShelfWidth / 2) - 21.5), 0);
            
            CreateFillet(buildParameters, 0, 0, LegsHeight);
            
            CreateFillet(buildParameters, ShelfLength/2, ShelfWidth/2, LegsHeight+ShelfHeight);
            
            CreateFillet(buildParameters, ShelfLength/2 -2, ShelfWidth/2 -2, LegsHeight+ShelfHeight+BindingHeight);

            CreateFillet(buildParameters, ShelfLength/2, ShelfWidth/2, LegsHeight + ShelfHeight + BindingHeight+ShelfHeight);//

            CreateFillet(buildParameters, ShelfLength/2 -2, ShelfWidth/2 -2, LegsHeight + ShelfHeight + BindingHeight + ShelfHeight+BindingHeight);

            CreateFillet(buildParameters, ShelfLength/2, ShelfWidth/2, LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight+ShelfHeight);


            CreateFillet(buildParameters, (ShelfLength / 2) - 21.5, (ShelfWidth / 2) - 21.5, LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight+10);
            CreateFillet(buildParameters, -((ShelfLength / 2) - 21.5), ((ShelfWidth / 2) - 21.5), LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight+10);
            CreateFillet(buildParameters, ((ShelfLength / 2) - 21.5), -((ShelfWidth / 2) - 21.5), LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight+10);
            CreateFillet(buildParameters, -((ShelfLength / 2) - 21.5), -((ShelfWidth / 2) - 21.5), LegsHeight + ShelfHeight + BindingHeight + ShelfHeight + BindingHeight + ShelfHeight+10);

            //СОЗДАНИЕ СКРУГЛЕНИЙ
        }

        private void BuildRectangleSketch(Parameters buildParameters,ksDocument2D document)
        {
            document.ksLineSeg(buildParameters.ParametersList["ShelfLength"]/2, buildParameters.ParametersList["ShelfWidth"]/2, -buildParameters.ParametersList["ShelfLength"] / 2, buildParameters.ParametersList["ShelfWidth"] / 2,1);
            document.ksLineSeg(buildParameters.ParametersList["ShelfLength"] / 2, buildParameters.ParametersList["ShelfWidth"] / 2, buildParameters.ParametersList["ShelfLength"] / 2, -buildParameters.ParametersList["ShelfWidth"] / 2, 1);
            document.ksLineSeg(buildParameters.ParametersList["ShelfLength"] / 2, -buildParameters.ParametersList["ShelfWidth"] / 2, -buildParameters.ParametersList["ShelfLength"] / 2, -buildParameters.ParametersList["ShelfWidth"] / 2, 1);
            document.ksLineSeg(-buildParameters.ParametersList["ShelfLength"] / 2, -buildParameters.ParametersList["ShelfWidth"] / 2, -buildParameters.ParametersList["ShelfLength"] / 2, buildParameters.ParametersList["ShelfWidth"] / 2, 1);
        }

        private void BuildLegsSketch(Parameters buildParameters, ksDocument2D document)
        {
            document.ksCircle((buildParameters.ParametersList["ShelfLength"] / 2) - 21.5, (buildParameters.ParametersList["ShelfWidth"] / 2) - 21.5, buildParameters.StaticParameters.ShelfLegsRadius, 1);
            document.ksCircle(-((buildParameters.ParametersList["ShelfLength"] / 2) - 21.5), (buildParameters.ParametersList["ShelfWidth"] / 2) - 21.5, buildParameters.StaticParameters.ShelfLegsRadius, 1);
            document.ksCircle((buildParameters.ParametersList["ShelfLength"] / 2) - 21.5, -((buildParameters.ParametersList["ShelfWidth"] / 2) - 21.5), buildParameters.StaticParameters.ShelfLegsRadius, 1);
            document.ksCircle(-((buildParameters.ParametersList["ShelfLength"] / 2) - 21.5), -((buildParameters.ParametersList["ShelfWidth"] / 2) - 21.5), buildParameters.StaticParameters.ShelfLegsRadius, 1);
        }

        private void BuildBindingSketch(Parameters buildParameters, ksDocument2D document)
        {
            document.ksCircle((buildParameters.ParametersList["ShelfLength"] / 2) - 21.5, (buildParameters.ParametersList["ShelfWidth"] / 2) - 21.5, buildParameters.StaticParameters.ShelfBindingRadius, 1);
            document.ksCircle(-((buildParameters.ParametersList["ShelfLength"] / 2) - 21.5), (buildParameters.ParametersList["ShelfWidth"] / 2) - 21.5, buildParameters.StaticParameters.ShelfBindingRadius, 1);
            document.ksCircle((buildParameters.ParametersList["ShelfLength"] / 2) - 21.5, -((buildParameters.ParametersList["ShelfWidth"] / 2) - 21.5), buildParameters.StaticParameters.ShelfBindingRadius, 1);
            document.ksCircle(-((buildParameters.ParametersList["ShelfLength"] / 2) - 21.5), -((buildParameters.ParametersList["ShelfWidth"] / 2) - 21.5), buildParameters.StaticParameters.ShelfBindingRadius, 1);
        }

        private void BuildInnerParts(Parameters buildParameters, ksDocument2D document)
        {
            document.ksLineSeg(buildParameters.DependentParameters.ShelfBootsPlaceLength / 2, buildParameters.DependentParameters.ShelfBootsPlaceWidth / 2, -buildParameters.DependentParameters.ShelfBootsPlaceLength / 2, buildParameters.DependentParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(buildParameters.DependentParameters.ShelfBootsPlaceLength / 2, buildParameters.DependentParameters.ShelfBootsPlaceWidth / 2, buildParameters.DependentParameters.ShelfBootsPlaceLength / 2, -buildParameters.DependentParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(buildParameters.DependentParameters.ShelfBootsPlaceLength / 2, -buildParameters.DependentParameters.ShelfBootsPlaceWidth / 2, -buildParameters.DependentParameters.ShelfBootsPlaceLength / 2, -buildParameters.DependentParameters.ShelfBootsPlaceWidth / 2, 1);
            document.ksLineSeg(-buildParameters.DependentParameters.ShelfBootsPlaceLength / 2, -buildParameters.DependentParameters.ShelfBootsPlaceWidth / 2, -buildParameters.DependentParameters.ShelfBootsPlaceLength / 2, buildParameters.DependentParameters.ShelfBootsPlaceWidth / 2, 1);
        }

        private void ExctrusionSketch(double depth, ksEntity Sketch,bool direction)
        {
            ksEntity entityExtr = (ksEntity) _ksPart.NewEntity((short) Obj3dType.o3d_bossExtrusion);
            ksBossExtrusionDefinition entityExtrDef = (ksBossExtrusionDefinition) entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = (ksExtrusionParam) entityExtrDef.ExtrusionParam();
            entityExtrDef.SetSketch(Sketch);
            if (direction == true)
            {
                entityExtrParam.direction = (short) Direction_Type.dtNormal;
            }
            else
            {
                entityExtrParam.direction = (short)Direction_Type.dtReverse;
            }

            entityExtrParam.typeNormal = (short) End_Type.etBlind;
            if (direction == true)
            {
                entityExtrParam.depthNormal = depth;
            }
            else
            {
                entityExtrParam.depthReverse = depth;
            }
            entityExtr.Create();
        }

        private void CutSketch(double depth, ksEntity Sketch, bool direction)
        {
            ksEntity entityExtr = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            ksCutExtrusionDefinition entityExtrDef = (ksCutExtrusionDefinition)entityExtr.GetDefinition();
            ksExtrusionParam entityExtrParam = (ksExtrusionParam)entityExtrDef.ExtrusionParam();
            entityExtrDef.SetSketch(Sketch);
            if (direction == true)
            {
                entityExtrParam.direction = (short)Direction_Type.dtNormal;
            }
            else
            {
                entityExtrParam.direction = (short)Direction_Type.dtReverse;
            }
            entityExtrParam.typeNormal = (short)End_Type.etBlind;
            if (direction == true)
            {
                entityExtrParam.depthNormal = depth;
            }
            else
            {
                entityExtrParam.depthReverse = depth;
            }
            entityExtr.Create();
        }

        private void CreateIncline(Parameters buildParameters,int n)
        {
            ksEntity entInc = (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_incline);
            ksInclineDefinition incDef = (ksInclineDefinition)entInc.GetDefinition();
            ksEntityCollection collect = (ksEntityCollection)_ksPart.EntityCollection((short)Obj3dType.o3d_face);
            incDef.direction = true;
            incDef.angle = buildParameters.StaticParameters.ShelfSlopeRadius; 
            ksEntity currentEntity = (ksEntity)_ksPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            incDef.SetPlane(currentEntity); 
            ksEntityCollection entColInc = (ksEntityCollection)incDef.FaceArray();
            var ShelfLegs = buildParameters.ParametersList["ShelfLegsHeight"];
            var ShelfHeight = buildParameters.ParametersList["ShelfHeight"];
            var ShelfBinding = buildParameters.ParametersList["ShelfBindingHeight"];
            if (n == 0)
            {
                var height = ShelfLegs+ShelfHeight-10;
                collect.SelectByPoint(0, 0, height);
            }

            if (n == 1)
            {
                var height = ShelfLegs+ShelfHeight+ShelfBinding+ShelfHeight-10;
                collect.SelectByPoint(0, 0, height);
            }

            if (n == 2)
            {
                var height = ShelfLegs+ShelfHeight+ShelfBinding+ShelfHeight+ShelfBinding+ShelfHeight-10;
                collect.SelectByPoint(0, 0, height);
            }
            entColInc.Add(collect.GetByIndex(0));
            collect.refresh();
            entInc.Create();
        }

        private void CreateFillet(Parameters buildParameters,double x,double y,double z)
        {
            // Получение интерфейса объекта скругление
            ksEntity entityFillet =
                (ksEntity)_ksPart.NewEntity((short)Obj3dType.o3d_fillet);
            // Получаем интерфейс параметров объекта скругление
            ksFilletDefinition filletDefinition =
                (ksFilletDefinition)entityFillet.GetDefinition();
            // Радиус скругления 
            filletDefinition.radius = buildParameters.StaticParameters.FilletRadius;
            // Не продолжать по касательным ребрам
            filletDefinition.tangent = true;
            // Получаем массив граней объекта
            ksEntityCollection entityCollectionPart =
                (ksEntityCollection)_ksPart.EntityCollection((short)Obj3dType.o3d_face);
            // Получаем массив скругляемых граней
            ksEntityCollection entityCollectionFillet =
                (ksEntityCollection)filletDefinition.array();
            entityCollectionFillet.Clear();
            // Заполняем массив скругляемых объектов
            entityCollectionPart.SelectByPoint(x, -y, z);
            entityCollectionFillet.Add(entityCollectionPart.First());
            // Создаем скругление
            entityFillet.Create();
        }

    }
}
