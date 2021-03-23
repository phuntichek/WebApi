﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using WebApi.Core.Interfaces;
using WebApi.Core.Models;

namespace WebApi.BL
{
    public class MaterialService : IMaterialServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _env;
        private string _dir;

        public MaterialService(IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            this._unitOfWork = unitOfWork;
            _env = env;
            _dir = _env.ContentRootPath + "/Library";
        }

        public async Task<IEnumerable<Material>> GetFilterMaterialsByDate()
        {
            return await _unitOfWork.Materials.FilterMaterialsByDate();
        }

        public async Task<IEnumerable<Material>> GetFilterMatreerialsByType(int categoryId)
        {
            return await _unitOfWork.Materials.FilterMatreerialsByType(categoryId);
        }

        public async Task<(byte[] mas, string fileType, string fileName)> GetDataForDownloadMaterialAsync(Guid mId)
        {
            IEnumerable<MaterialVersion> ActualList() => _unitOfWork.MaterialVersions
                .Find(m => m.Material.Id == mId)
                .ToList()
                .OrderByDescending(m => m.FileDate);
            MaterialVersion ActualVersion = ActualList().Select(m => m).FirstOrDefault();
            string fileName = ActualVersion.FileName;
            string filePath = ActualVersion.PathOfFile + "/" + fileName;
            string fileType = "application/octet-stream";
            byte[] mas = System.IO.File.ReadAllBytes(filePath);

            return (mas, fileType, fileName);
        }

        public async Task<Material> UploadNewMaterial(string fileName, Int32 categoryNameId, long length)
        {
            Material uploadedMaterial = new Material
            {
                MaterialDate = DateTime.Now,
                MaterialName = $"{fileName}",
                MatCategoryId = Convert.ToInt16(categoryNameId)
            };
            MaterialVersion version = new MaterialVersion
            {
                FileDate = DateTime.Now,
                Material = uploadedMaterial,
                FileName = $"{fileName}",
                Size = length,
                PathOfFile = _dir
            };

            await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> { version });
            await _unitOfWork.CommitAsync();
            return uploadedMaterial;
        }

        public async Task<IEnumerable<MaterialVersion>> FilterVersionsByDate(Guid mId)
        {
            return await _unitOfWork.MaterialVersions.GetFilterVersionsByDate(mId);
        }

        public async Task<IEnumerable<MaterialVersion>> FilterVersionsBySize(Guid mId)
        {
            return await _unitOfWork.MaterialVersions.GetFilterVersionsBySize(mId);
        }

        public async Task<MaterialVersion> UploadNewMaterialVersion(string fileName, Guid mId, long length)
        {
            MaterialVersion uploadedVersion = new MaterialVersion
            {
                FileDate = DateTime.Now,
                FileName = $"{fileName}",
                PathOfFile = _dir,
                Size = length,
                Material = await _unitOfWork.Materials.GetMaterialById(mId)
            };

            await _unitOfWork.MaterialVersions.AddRangeAsync(new List<MaterialVersion> { uploadedVersion });
            await _unitOfWork.CommitAsync();
            return uploadedVersion;
        }

        public async Task<(byte[] mas, string fileType, string fileName)> GetMaterialVersionFile(Guid vId)
        {
            MaterialVersion GetOfMaterialVersion() =>
                _unitOfWork.MaterialVersions.Find(m => m.Id == vId).SingleOrDefault();
            string fileName = GetOfMaterialVersion().FileName;
            string filePath = GetOfMaterialVersion().PathOfFile + "/" + fileName;
            string fileType = "application/octet-stream";
            byte[] mas = System.IO.File.ReadAllBytes(filePath);

            return (mas, fileType, fileName);
        }
    }
}