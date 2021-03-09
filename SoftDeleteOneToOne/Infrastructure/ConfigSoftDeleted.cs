using SoftDeleteServices.Configuration;

namespace SoftDeleteOneToOne.Infrastructure
{
    public class ConfigSoftDeleted : SingleSoftDeleteConfiguration<ISingleSoftDelete>
    {
        public ConfigSoftDeleted(MyDbContext context) : base(context)
        {
            GetSoftDeleteValue = entity => entity.IsDeleted;
            SetSoftDeleteValue = (entity, value) => entity.IsDeleted = value;
        }
    }
}