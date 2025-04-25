
using Barber.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Context
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Mapeamento
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Barbeiro> Barbeiros { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Oferece> Oferece { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<HorarioDisponivel> HorariosDisponiveis { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<HistoricoCorte> HistoricosCorte { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chave composta em Oferece
            modelBuilder.Entity<Oferece>()
                .HasKey(o => new { o.IdBarbeiro, o.IdServico });

            // Relacionamento Cliente - Agendamento
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Cliente)
                .WithMany()
                .HasForeignKey(a => a.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Servico - Agendamento
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Servico)
                .WithMany()
                .HasForeignKey(a => a.IdServico)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento HorarioDisponivel - Agendamento
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.HorarioDisponivel)
                .WithMany()
                .HasForeignKey(a => a.IdHorario)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Barbeiro - HorarioDisponivel
            modelBuilder.Entity<HorarioDisponivel>()
                .HasOne(h => h.Barbeiro)
                .WithMany()
                .HasForeignKey(h => h.IdBarbeiro)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Agendamento - Avaliacao
            modelBuilder.Entity<Avaliacao>()
                .HasOne(av => av.Agendamento)
                .WithMany()
                .HasForeignKey(av => av.IdAgendamento)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento Agendamento - HistoricoCorte
            modelBuilder.Entity<HistoricoCorte>()
                .HasOne(hc => hc.Agendamento)
                .WithMany()
                .HasForeignKey(hc => hc.IdAgendamento)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}