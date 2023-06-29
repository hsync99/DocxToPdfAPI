using DocxToPdfAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DocxToPdfAPI.Controllers
{
    public class ConvertationController : Controller
    {
        IConvertService _convertService;

        public ConvertationController(IConvertService convertService)
        {
            _convertService = convertService;

        }
        [HttpPost]
        [Route("ConvertToPdf")]
        public async Task<IActionResult> ConvertToPDF(IFormFile file)
        {
            var filePath = Path.GetFullPath(file.FileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            string filename = file.FileName.Replace(".docx", ".pdf");
            var result = await _convertService.ConvertToPdf(filePath);
            System.IO.File.Delete(filePath);
            try
            {
                byte[] filecontent = System.IO.File.ReadAllBytes(result);
                System.IO.File.Delete(result);
                return File(filecontent, "application/pdf", filename);
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
    }
}
