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
            TestBuild(BuildParameters);

        }

        private void TestBuild(Parameters buildParameters)
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

            //ВТОРАЯ ЧАСТЬ 
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
            newEntityDefinition1.offset = -buildParameters.ParametersList["ShelfBindingHeight"];
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

            //ЧЕТВЕРТАЯ ЧАСТЬ 
            

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
            newEntityDefinition3.offset = -buildParameters.ParametersList["ShelfBindingHeight"];
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

            //ШЕСТАЯ ЧАСТЬ ОСНОВА

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

            //СЕДЬМАЯ ЧАСТЬ КРЕПЛЕНИЕ
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
            document.ksCircle((buildParameters.ParametersList["ShelfLength"] / 2) - 20.5, (buildParameters.ParametersList["ShelfWidth"] / 2) - 20.5, buildParameters.StaticParameters.ShelfLegsRadius, 1);
            document.ksCircle(-((buildParameters.ParametersList["ShelfLength"] / 2) - 20.5), (buildParameters.ParametersList["ShelfWidth"] / 2) - 20.5, buildParameters.StaticParameters.ShelfLegsRadius, 1);
            document.ksCircle((buildParameters.ParametersList["ShelfLength"] / 2) - 20.5, -((buildParameters.ParametersList["ShelfWidth"] / 2) - 20.5), buildParameters.StaticParameters.ShelfLegsRadius, 1);
            document.ksCircle(-((buildParameters.ParametersList["ShelfLength"] / 2) - 20.5), -((buildParameters.ParametersList["ShelfWidth"] / 2) - 20.5), buildParameters.StaticParameters.ShelfLegsRadius, 1);
        }

        private void BuildBindingSketch(Parameters buildParameters, ksDocument2D document)
        {
            document.ksCircle((buildParameters.ParametersList["ShelfLength"] / 2) - 20.5, (buildParameters.ParametersList["ShelfWidth"] / 2) - 20.5, buildParameters.StaticParameters.ShelfBindingRadius, 1);
            document.ksCircle(-((buildParameters.ParametersList["ShelfLength"] / 2) - 20.5), (buildParameters.ParametersList["ShelfWidth"] / 2) - 20.5, buildParameters.StaticParameters.ShelfBindingRadius, 1);
            document.ksCircle((buildParameters.ParametersList["ShelfLength"] / 2) - 20.5, -((buildParameters.ParametersList["ShelfWidth"] / 2) - 20.5), buildParameters.StaticParameters.ShelfBindingRadius, 1);
            document.ksCircle(-((buildParameters.ParametersList["ShelfLength"] / 2) - 20.5), -((buildParameters.ParametersList["ShelfWidth"] / 2) - 20.5), buildParameters.StaticParameters.ShelfBindingRadius, 1);
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

        private void CreateSimpleSketch()
        {

        }

        private void CreateOffSetSketch()
        {

        }

    }
}
