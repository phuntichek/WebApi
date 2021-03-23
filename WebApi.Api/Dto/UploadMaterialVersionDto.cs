using System;
namespace WebApi.Api.Dto
{
    public class UploadMaterialVersionDto
    {
        public string Name { get; set; }
        public Guid MaterialId { get; set; }
    }
}
