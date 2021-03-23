using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Api.Dto;
using WebApi.Core.Interfaces;
using WebApi.Core.Models;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IMaterialServices _materialService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;
        private string _dir;

        public VersionController(IMaterialServices materialService, IMapper mapper, IHostingEnvironment env)
        {
            this._mapper = mapper;
            this._materialService = materialService;
            _env = env;
            _dir = _env.ContentRootPath + "/Library";
        }

        [Route("/api/material/{mId}/versions/dateOrder")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialVersionDto>>> GetVersionsOrdergniByDate(Guid mId)
        {
            var versions = await _materialService.FilterVersionsByDate(mId);
            var materialVersionResultDto = _mapper.
                Map<IEnumerable<MaterialVersion>, IEnumerable<MaterialVersionDto>>(versions);
            return Ok(materialVersionResultDto);
        }

        [Route("/api/material/{mId}/versions/syzeOrder")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialVersionDto>>> GetVersionsOrdergniBySyze(Guid mId)
        {
            var versions = await _materialService.FilterVersionsBySize(mId);
            var materialVersionResultDto = _mapper.
                Map<IEnumerable<MaterialVersion>, IEnumerable<MaterialVersionDto>>(versions);
            return Ok(materialVersionResultDto);
        }

        [Route("/api/version/upload/")]
        [HttpPost]
        public async Task<ActionResult> UploadNewVersionOfMaterial([FromForm] UploadMaterialVersionDto materialVersionform, IFormFile file)
        {
            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{materialVersionform.Name}{Path.GetExtension(file.FileName)}"),
                FileMode.Create,
                FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            var versionOfMaterial = await _materialService.UploadNewMaterialVersion($"{materialVersionform.Name}{Path.GetExtension(file.FileName)}",
                materialVersionform.MaterialId,
                file.Length);
            return Ok(versionOfMaterial);
        }

        [Route("/api/version/{vId}/download")]
        [HttpPost]
        public async Task<ActionResult> DownloadVersionOfMaterial(Guid vId)
        {
            var fileData = await _materialService.GetMaterialVersionFile(vId);
            return File(fileData.mas, fileData.fileType, fileData.fileName);
        }
    }
}
