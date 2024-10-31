using Application;
using Application.Features.OperationClaims.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnName("Id");
        builder.Property(o => o.Name).HasColumnName("Name");
        builder.HasIndex(indexExpression: o => o.Name, name: "UK_OperationClaims_Name").IsUnique();

        //builder.HasData(getSeeds());
        builder.HasData(_seeds);
    }

    //private HashSet<OperationClaim> getSeeds()
    //{
    //    int id = 0;
    //    HashSet<OperationClaim> seeds = new();

    //    seeds.Add(new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin });

    //    return seeds;
    //}

    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            int id = 0;

            yield return new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin };

            #region Feature Operation Claims
            IEnumerable<Type> featureOperationClaimsTypes = Assembly.
                GetAssembly(typeof(ApplicationServiceRegistration))!
                .GetTypes()
                .Where(
                type =>
                (type.Namespace?.Contains("Features") == true)
                && type.IsClass
                && type.Name.EndsWith("OperationClaims")
                );

            foreach (Type type in featureOperationClaimsTypes)
            {
                FieldInfo[] typeFields = type.GetFields(BindingFlags.Public | BindingFlags.Static);
                IEnumerable<string> typeFieldsValues = typeFields.Select(field => field.GetValue(null)!.ToString()!);

                IEnumerable<OperationClaim> featureOperationClaimsToAdd = typeFieldsValues.Select(
                    value => new OperationClaim { Id = ++id, Name = value }
                    );

                foreach (OperationClaim featureOperationClaim in featureOperationClaimsToAdd)
                    yield return featureOperationClaim;
            }
            #endregion

        }
    }
}
