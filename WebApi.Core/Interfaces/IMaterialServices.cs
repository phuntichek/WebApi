using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Core.Models;

namespace WebApi.Core.Interfaces
{
    public interface IMaterialServices
    {
        Task<IEnumerable<Material>> GetFilterMaterialsByDate();
        Task<IEnumerable<Material>> GetFilterMatreerialsByType(int catId);

        Task<Material> UploadNewMaterial(string fileName, Int32 categoryNameId, long length);
        Task<(byte[] mas, string fileType, string fileName)> GetDataForDownloadMaterialAsync(Guid matId);

        Task<IEnumerable<MaterialVersion>> FilterVersionsByDate(Guid mId);
        Task<IEnumerable<MaterialVersion>> FilterVersionsBySize(Guid mId);

        Task<(byte[] mas, string fileType, string fileName)> GetMaterialVersionFile(Guid vId);
        Task<MaterialVersion> UploadNewMaterialVersion(string fileName, Guid mId, long length);
    }
}
