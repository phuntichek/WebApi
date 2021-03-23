using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Api.Dto;
using WebApi.Api.Validations;
using WebApi.Core.Interfaces;
using WebApi.Core.Models;

namespace WebApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialServices _materialService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;
        private string _dir;

        public MaterialController(IMaterialServices materialService, IMapper mapper, IHostingEnvironment env)
        {
            this._mapper = mapper;
            this._materialService = materialService;
            _env = env;
            _dir = _env.ContentRootPath + "/Library";
        }

        [HttpGet("/api/materials/dateOrder")]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterialsByDate()
        {
            var materials = await _materialService.GetFilterMaterialsByDate();
            var materialResultDto = _mapper.
                Map<IEnumerable<Material>, IEnumerable<MaterialDto>>(materials);
            return Ok(materialResultDto);
        }

        [HttpPost("/api/material/upload")]
        public async Task<ActionResult<MaterialDto>> UploadedMaterial([FromForm] UploadMaterialDto uploadMaterialForm, IFormFile file)
        {
            var validator = new SaveMaterialValidator();
            var validationResult = await validator.ValidateAsync(uploadMaterialForm);
            if (!validationResult.IsValid)
                return BadRequest("Invalid data");

            using (var fileStream = new FileStream(
                Path.Combine(_dir,
                    $"{uploadMaterialForm.Name}{Path.GetExtension(file.FileName)}"),
                FileMode.Create,
                FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            var material = await _materialService.UploadNewMaterial(
                $"{uploadMaterialForm.Name}{Path.GetExtension(file.FileName)}",
                uploadMaterialForm.CategoryNameId,
                file.Length);
            var materialResultDto = _mapper.
                Map<Material, MaterialDto>(material);
            return Ok(materialResultDto);
        }

        [Route("/api/material/{mId}/download")]
        [HttpPost]
        public async Task<ActionResult> DownloadMaterial(Guid mId)
        {
            var fileData = await _materialService.GetDataForDownloadMaterialAsync(mId);
            return File(fileData.mas, fileData.fileType, fileData.fileName);
        }
    }
}
