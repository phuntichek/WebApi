using System;
namespace WebApi.Api.Dto
{
    public class MaterialDto
    {
        public Guid Id { get; set; }
        public string MaterialName { get; set; }
        public int MatCategoryId { set; get; }
    }
}
