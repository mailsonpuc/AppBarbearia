
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
                .HasKey(o => new { o.BarbeiroId, o.ServicoId });

            // Relacionamento Cliente - Agendamento
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Cliente)
                .WithMany()
                .HasForeignKey(a => a.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Servico - Agendamento
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Servico)
                .WithMany()
                .HasForeignKey(a => a.ServicoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento HorarioDisponivel - Agendamento
            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.HorarioDisponivel)
                .WithMany()
                .HasForeignKey(a => a.HorarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Barbeiro - HorarioDisponivel
            modelBuilder.Entity<HorarioDisponivel>()
                .HasOne(h => h.Barbeiro)
                .WithMany()
                .HasForeignKey(h => h.BarbeiroId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Agendamento - Avaliacao
            modelBuilder.Entity<Avaliacao>()
                .HasOne(av => av.Agendamento)
                .WithMany()
                .HasForeignKey(av => av.AgendamentoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento Agendamento - HistoricoCorte
            modelBuilder.Entity<HistoricoCorte>()
                .HasOne(hc => hc.Agendamento)
                .WithMany()
                .HasForeignKey(hc => hc.AgendamentoId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}