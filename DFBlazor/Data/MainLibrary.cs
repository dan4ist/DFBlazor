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
        public string ConvertFile(string path) {
            string outputFile;

            FileInfo info = new FileInfo(path);
            using ( MagickImage i = new MagickImage(info.FullName) ) {
                //save as jpg
                outputFile = path.Substring(0, path.IndexOf('.')) + ".jpg";
                i.Write(outputFile);
            }
            
            return outputFile;
        }
        #endregion

    }
}
