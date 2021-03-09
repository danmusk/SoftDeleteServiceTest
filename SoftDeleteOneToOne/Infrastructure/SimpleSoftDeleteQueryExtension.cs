using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SoftDeleteOneToOne.Infrastructure
{
    public static class SimpleSoftDeleteQueryExtension
    {
        public static void AddSingleSoftDeleteQueryFilter(
            this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SimpleSoftDeleteQueryExtension)
                    .GetMethod(nameof(GetSoftDeleteFilter),
                        BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall.Invoke(null, new object[] { });
            entityData.SetQueryFilter((LambdaExpression)filter!);
            entityData.AddIndex(entityData.FindProperty(nameof(ISingleSoftDelete.IsDeleted)));
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>()
            where TEntity : class, ISingleSoftDelete
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }
}