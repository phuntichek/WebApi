using System;
namespace WebApi.Api.Dto
{
    public class MaterialVersionDto
    {
        public Guid Id { get; set; }
        public string FileName { set; get; }
        public long Size { set; get; }
        public DateTime FileDate { set; get; }
    }
}
