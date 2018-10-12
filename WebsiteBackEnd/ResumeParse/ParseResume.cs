using System.Diagnostics;
using System.IO;
using System.Linq;
//using Sharpenter.ResumeParser.OutputFormatter.Json;
//using Sharpenter.ResumeParser.ResumeProcessor;

namespace WebsiteBackEnd.ResumeParse
{
    public class ParseResume
    {
        //public void ParsePdfResume()
        //{
        //    var outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        //    if (Directory.Exists(outputFolder))
        //    {
        //        Directory.Delete(outputFolder, true);
        //    }

        //    Directory.CreateDirectory(outputFolder);

        //    var processor = new ResumeProcessor(new JsonOutputFormatter());

        //    //var files = Directory.GetFiles("Resumes").Select(Path.GetFullPath);            
        //    DirectoryInfo d = new DirectoryInfo(@"C:\Users\isd\Desktop\CVs");//Assuming Test is your Folder
        //    FileInfo[] files = d.GetFiles("*.pdf"); //Getting Text files
        //    //string str = "";
        //    foreach (var filex in files)
        //    {
        //        var file = filex.FullName;
        //        var output = processor.Process(file);

        //        System.Console.WriteLine(output);

        //        var outputFileName = file.Substring(file.LastIndexOf(Path.DirectorySeparatorChar) + 1) + ".txt";
        //        using (var writer = new StreamWriter(Path.Combine(outputFolder, outputFileName)))
        //        {
        //            writer.Write(output);
        //        }
        //    }
        //}
    }
}
