using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADObjectCreatorBuilder;
using CADObjectCreatorParameters;
using OfficeOpenXml;


namespace CADObjectCreatorStressTest
{
    public class StressTest
    {
        /// <summary>
        /// Поле хранит экземпляр класса Parameters для выполнения тестирования.
        /// </summary>
        private Parameters _testParameters = new Parameters();

        /// <summary>
        /// Поле хранит экземпляр класса Builder для выполнения тестирования.
        /// </summary>
        private Kompas3DBuilder _testBuilder = new Kompas3DBuilder();

        /// <summary>
        /// Метод задающий значения зависящим параметрам.
        /// </summary>
        private void VerifyParameters()
        {
            _testParameters.ShelfBootsPlaceLength =
                _testParameters[ParametersName.ShelfLength].Value;
            _testParameters.ShelfBootsPlaceWidth =
                _testParameters[ParametersName.ShelfWidth].Value;
        }

        /// <summary>
        /// Метод выполняет нагрузочное тестирование состоящее из создания 200 моделей.
        /// </summary>
        public void StressTestStart()
        {
            VerifyParameters();
            _testBuilder.BuildObject(_testParameters, true, true);
            var processes = Process.GetProcessesByName("kStudy");
            var process = processes.First();

            var ramCounter = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName);
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            Stopwatch stopwatch = new Stopwatch();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var defaultPathToExcel = $@"{AppDomain.CurrentDomain.BaseDirectory}\StressTest.xlsx";
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("MainSheet");
                var mainSheet = excel.Workbook.Worksheets["MainSheet"];

                List<string[]> headerRow = new List<string[]>()
                {
                    new string[] {"RAM", "CPU", "TIME"}
                };

                string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
                mainSheet.Cells[headerRange].LoadFromArrays(headerRow);

                var count = 200;
                for (int i = 2; i < count+2; i++)
                {
                    stopwatch.Start();

                    cpuCounter.NextValue();
                    VerifyParameters();
                    _testBuilder.BuildObject(_testParameters, true, true);


                    stopwatch.Stop();

                    var ram = ramCounter.NextValue();
                    var cpu = cpuCounter.NextValue();

                    mainSheet.Cells["A" + $"{i}"].Value = Math.Round(ram / 1024 / 1024);
                    mainSheet.Cells["B" + $"{i}"].Value = cpu;
                    mainSheet.Cells["C" + $"{i}"].Value = stopwatch.Elapsed;

                    stopwatch.Reset();
                }


                FileInfo excelFile = new FileInfo(defaultPathToExcel);
                excel.SaveAs(excelFile);
            }

        }
    }
}
