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
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasData(
                new Card { CardNumber = "5859831000619801", HolderName = "Armin", Password = "123", Balance = 2000 },
                new Card { CardNumber = "5859831000619802", HolderName = "Mehdi", Password = "123", Balance = 2000 },
                new Card { CardNumber = "5859831000619803", HolderName = "Ali", Password = "123", Balance = 2000 },
                new Card { CardNumber = "5859831000619804", HolderName = "Arash", Password = "123", Balance = 2000 },
                new Card { CardNumber = "5859831000619805", HolderName = "Maryam", Password = "123", Balance = 2000 }
                );
        }
    }
}
