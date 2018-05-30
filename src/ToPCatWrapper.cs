using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tat.classes.TopCAT
{
    class Wrapper
    {
        private string _sInputFilePath;
        private int _iXResolution;
        private int _iYResolution;
        private int _iNWindowCount;
        private int _iZMin;
        private int _iZMean;
        private int _iStandardDeviation;
        private bool _bReady;
        private string _sBatchCommand;        

        public Wrapper(string sInputFilePath,
                        int iXResolution,
                        int iYResolution,
                        int iNWindowCount,
                        int iZMin = int.MaxValue,
                        int iZMean = int.MaxValue,
                        int iStandardDeviation = int.MaxValue)
        {
            if (String.IsNullOrEmpty(_sInputFilePath))
            {
                throw new Exception("Input file path cannot be empty.");
            }



            _sInputFilePath = sInputFilePath;
            _iXResolution = iXResolution;
            _iYResolution = iYResolution;
            _iNWindowCount = iNWindowCount;
            _iZMin = iZMin;
            _iZMean = iZMean;
            _iStandardDeviation = iStandardDeviation;

            // determine if file is ready to be processed by ToPCAT
            _bReady = IsReady(_sInputFilePath);

            // get topcat executable path
            string sTopCAT_Path = TopCAT_Configuration.GetTopCATPath();

            // create unique temp file
            string sOutputBatchPath = string.Empty;
      
            // create dictionary of parameters
            Dictionary<string, string> dFlagParameters = new Dictionary<string, string> { };
            dFlagParameters["--xres"] = iXResolution.ToString();
            dFlagParameters["--yres"] = iYResolution.ToString();
            dFlagParameters["--nmin"] = iNWindowCount.ToString();

            if (iZMin != int.MaxValue)
            {
                dFlagParameters["--*zmin"] = iZMin.ToString();
            }

            if (iZMin != int.MaxValue)
            {
                dFlagParameters["--*zmean"] = iZMean.ToString();
            }

            if (iZMin != int.MaxValue)
            {
                dFlagParameters["--*stdev"] = iStandardDeviation.ToString();
            }

            _sBatchCommand = classes.Utilities.BatchFile.CreateCommand(sTopCAT_Path,
                                                                        sInputFilePath, 
                                                                        dFlagParameters);
            System.Diagnostics.Process.Start("CMD.exe",
                                             _sBatchCommand);
                                              

        }

        private void Execute()
        {

        }

        private void InitialFileScan(string sInputPath)
        {


        }


        private string CreateSummary()
        {
            string sSummary = String.Format("File: {1}{0}Decimated: {2}{0}X Resolution: {3}{0}Y Resolution: {4}{0}Min points used to calculate statistics: {5}{0}Standard deviation sample window: {6}{0}",
                                            Environment.NewLine,
                                            "TODO",
                                            true,
                                            _iXResolution,
                                            _iYResolution,
                                            _iNWindowCount,
                                            _iStandardDeviation);

            string sFlagParameters = String.Format("The detrended standard deviation for each sample window was calculated as {0} standard deviations from the {1}",
                                                    _iStandardDeviation,
                                                    "TODO:zmean");

            sSummary += sFlagParameters;
            return sSummary;
        }


        private bool IsReady(string sInputFilePath)
        {
            bool bReady = false;

            return bReady;
        }

    }
}
