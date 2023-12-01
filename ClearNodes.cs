using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace ProjNodeUpdateHelper
{
    public class ClearNodes
    {
        public static int cntr { get; set; }
        public static int totalLines { get; set; }
        public static int totalUpdates { get; set; }
        public string _NodeDestDir { get; set; }

        public string _filename { get; set; }
        public string opath = "";

        public string destPath = ConfigurationManager.AppSettings["destPath"];
        //@"C:\devlab\teststudio\tempDest";
        public string updatePath = @"C:\devlab\UpdatedSql\";
        public ClearNodes()
        {
            this.opath = ConfigurationManager.AppSettings["nodeLocation"];

            cntr = 0;
            MoveNM();
        }


        public void MoveNM()
        {
            var cur = opath + @"\node_modules";
            if (Directory.Exists(cur))
            {
                var newDirInx = 0;
                var destDir = destPath + @"\node_modules" + newDirInx;
                while (Directory.Exists(destDir))
                {
                    destDir = destPath + @"\node_modules" + newDirInx++;
                }
                _NodeDestDir = destDir;
                bool notDone = true;
                while (notDone)
                {
                    try
                    {
                        Directory.Move(cur, destDir);
                        notDone = false;
                    }
                    catch (FileNotFoundException fne)
                    {
                        cclog.wlf("already gone");
                        notDone = false;

                    }
                    catch (Exception ex)
                    {
                        cclog.wlf(ex.ToString());

                        cclog.wlf("Directory In USE!!!!!  Close Directory!!!");
                        Thread.Sleep(5000);
                    }
                }
                cclog.wlf("Directory moved, You can reinstall.");
                DeleteDest();
            }
        }


        public void DeleteDest()
        {
            if (Directory.Exists(_NodeDestDir))
            {
                Directory.Delete(_NodeDestDir, true);

            }


        }


    }



}
