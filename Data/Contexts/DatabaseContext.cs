using Microsoft.EntityFrameworkCore;
using sistemaDivtech.Models;

namespace sistemaDivtech.Data.Contexts
{
    public class DatabaseContext : DbContext
    {
        // Propriedades para manipular as classes no Banco
        public DbSet<FornecedorModel> Fornecedores { get; set; }
        public DbSet<ClienteModel> Clientes { get; set; }

        // Método para criação das entidades no DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Criação da TBL Fornecedores do DB
            modelBuilder.Entity<FornecedorModel>(entity =>
            {
                entity.ToTable("FORNECEDORES_DIVTECH");
                entity.HasKey(e => e.FornecedorId);

                // Configurações das propriedades
                entity.Property(e => e.FornecedorNome)
                    .HasMaxLength(100);

                entity.Property(e => e.Cnpj)
                    .HasColumnType("varchar(14)")
                    .IsRequired();

                entity.Property(e => e.Cep)
                    .HasColumnType("varchar(8)")
                    .IsRequired();

                entity.Property(e => e.Endereco)
                    .HasMaxLength(255);

                entity.Property(e => e.Segmento)
                    .IsRequired();
            });

            //Criação da TBL Clientes no DB
            modelBuilder.Entity<ClienteModel>(entity =>
            {
                entity.ToTable("CLIENTES_DIVTECH");
                entity.HasKey(e => e.ClienteId);
                entity.Property(e => e.Foto)
                    .HasColumnType("BLOB");
                entity.HasOne(e => e.Fornecedor)
                .WithMany()
                .HasForeignKey(e => e.FornecedorId)
                .IsRequired();
            });
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected DatabaseContext()
        {
        }
    }
}
