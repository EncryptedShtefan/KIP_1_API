using Microsoft.EntityFrameworkCore;

namespace KIP_1_API.Models
{
    public class MyDbContext : DbContext //класс нашего контекста базы данных, наследуется от базового класса DbContext EF
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Manufacturers> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturers>(entity =>
            {
                //устанавливаем свойства для полей таблицы производителей (указываем, что данные поля не могут быть "нулевыми")
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Cars>(entity =>
            {
                entity.Property(e => e.ModelName).IsRequired();
                entity.Property(e => e.EngineValue).IsRequired();

                entity.HasOne(d => d.Manufacturers) //связь машин с производителями - один производитель-много машин, айдишник производителя
                .WithMany(p => p.Cars)
                .HasForeignKey(d => d.ManufId);
            });
        }
    }

}
