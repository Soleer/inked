﻿namespace Inked.Ordering.Infrastructure.EntityConfigurations;

internal class CardTypeEntityTypeConfiguration
    : IEntityTypeConfiguration<CardType>
{
    public void Configure(EntityTypeBuilder<CardType> cardTypesConfiguration)
    {
        cardTypesConfiguration.ToTable("cardtypes");

        cardTypesConfiguration.Property(ct => ct.Id)
            .ValueGeneratedNever();

        cardTypesConfiguration.Property(ct => ct.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}
