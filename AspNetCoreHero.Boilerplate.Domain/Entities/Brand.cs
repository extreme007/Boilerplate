using AspNetCoreHero.Abstractions.Domain;

namespace AspNetCoreHero.Boilerplate.Domain.Entities
{
    public class Brand : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Tax { get; set; }
    }
}