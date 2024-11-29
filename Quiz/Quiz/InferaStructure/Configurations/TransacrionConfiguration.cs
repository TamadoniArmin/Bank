using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quiz.Entity;

namespace Quiz.InferaStructure.Configurations
{
    public class TransacrionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasOne(t => t.SourceCard)
            .WithMany(c => c.SourceTransactions)
            .HasForeignKey(t => t.SourceCardNumber)
            .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.DestinationCard)
            .WithMany(c => c.DestinationTransactions)
            .HasForeignKey(t => t.DestinationCardNumber)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
