using ImageMagick;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace DFBlazor.Data {
    public class MainLibrary {


        #region Image File Conversion
        public List<string> ConvertAllFiles(string path, string origExt, string outPath, string outExt) {
            string[] allfiles = Directory.GetFiles(path, origExt, SearchOption.TopDirectoryOnly);
            List<string> outputFiles = new();
            foreach(var file in allfiles) {

                FileInfo info = new FileInfo(file);
                using (MagickImage i = new MagickImage(info.FullName)) {
                    //save as jpg
                    i.Write(outPath + info.Name + outExt);
                    outputFiles.Add(info.FullName);
                }
            }

            return outputFiles;
        }
        public string ConvertFile(string path, string origExt, string outPath, string outExt) {
            string outputFile;

            FileInfo info = new FileInfo(path + origExt);
            using ( MagickImage i = new MagickImage(info.FullName) ) {
                //save as jpg
                i.Write(outPath + info.Name + outExt);
                outputFile = info.FullName;
            }
            

            return outputFile;
        }
        #endregion

    }
}
