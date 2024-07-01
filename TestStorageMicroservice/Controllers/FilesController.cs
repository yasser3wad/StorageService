using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Storage.CoreInfrastructure.CoreFeatures;

namespace Storage.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly IFileManagerService _fileManagerService;

        public FilesController(IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
        }

        [HttpPost ]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            var saved = await _fileManagerService.UploadFileAsync(file);
            return Ok(new { saved });
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var fileData = await _fileManagerService.DownloadFileAsync(fileName);
            if (fileData == null)
                return NotFound();

            return File(fileData, "application/octet-stream", fileName);
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            await _fileManagerService.DeleteFileAsync(fileName);
            return NoContent();
        }
        [HttpGet("{fileName}/metadata")]
        public async Task<IActionResult> RetriveMetaData(string fileName  )
        {
            var fileMetaData = await _fileManagerService.RetrieveFileMetaDataAsync(fileName);
            if (fileMetaData == null)
                return NotFound();

            return Ok(fileMetaData);
        }
    }

}
