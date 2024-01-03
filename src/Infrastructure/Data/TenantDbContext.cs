// Copyright (c) 2021 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

using System.Reflection;
using AuthPermissions.AspNetCore.GetDataKeyCode;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.EfCode;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Data
{
    public class TenantDbContext : DbContext, IDataKeyFilterReadOnly, ITenantDbContext
    {
        public string DataKey { get; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options, IGetDataKeyFromUser dataKeyFilter)
            : base(options)
        {
            // The DataKey is null when: no one is logged in, its a background service, or user hasn't got an assigned tenant
            // In these cases its best to set the data key that doesn't match any possible DataKey 
            DataKey = dataKeyFilter?.DataKey ?? "Bad key";
        }

        public DbSet<TodoList> TodoLists => Set<TodoList>();

        public DbSet<TodoItem> TodoItems => Set<TodoItem>();

        public DbSet<Product> Products => Set<Product>();

        public DbSet<Order> Orders => Set<Order>();

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.MarkWithDataKeyIfNeeded(DataKey);
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.MarkWithDataKeyIfNeeded(DataKey);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tenant");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<TodoList>().OwnsOne<Colour>(nameof(TodoList.Colour));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType?.ClrType?.BaseType?.Name == typeof(ValueObject).Name)
                    continue;

                if (typeof(IDataKeyFilterReadOnly).IsAssignableFrom(entityType?.ClrType))
                {
                    entityType.AddHierarchicalTenantReadOnlyQueryFilter(this);
                }
                else if (typeof(INoDataKey).IsAssignableFrom(entityType?.ClrType))
                {
                    continue;
                }
                else
                    throw new Exception(
                        $"You haven't added the {nameof(IDataKeyFilterReadWrite)} to the entity {entityType?.ClrType.Name}");

                foreach (var mutableProperty in entityType.GetProperties())
                {
                    if (mutableProperty.ClrType == typeof(decimal))
                    {
                        mutableProperty.SetPrecision(9);
                        mutableProperty.SetScale(2);
                    }
                }
            }
        }
    }
}
