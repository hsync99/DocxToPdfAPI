using GemBox.Document;

namespace DocxToPdfAPI.Services
{
    public class ConvertService : IConvertService
    {
        public async  Task<string> ConvertToPdf(string filepath)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

            // Create new document.
            var document = DocumentModel.Load(filepath);

            FileInfo wordfile = new FileInfo(filepath);
            Object filename = (Object)wordfile.FullName;
            string outputfilename = wordfile.FullName.Replace(".docx", ".pdf");
            document.Save(outputfilename);
            return outputfilename;

        }
    }
}
