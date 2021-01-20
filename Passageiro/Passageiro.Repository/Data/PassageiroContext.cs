 using Flunt.Notifications;
 using Microsoft.EntityFrameworkCore;
 using Passageiro.Domain.Entities;
 using Passageiro.Repository.Mappings;

 namespace Passageiro.Repository.Data {
   public class PassageiroContext : DbContext {

     public PassageiroContext (DbContextOptions<PassageiroContext> options):
       base (options) {

       }

     public DbSet<PassageiroDomain> Passageiro { get; set; }
     public DbSet<ControleAcessoDomain> ControleAcesso { get; set; }

     protected override void OnModelCreating (ModelBuilder modelBuilder) {
       modelBuilder.Ignore<Notification> ();
       modelBuilder.Ignore<Notifiable> ();
       modelBuilder.ApplyConfiguration (new PassageiroMap ());
       modelBuilder.ApplyConfiguration (new ControleAcessoMap ());

       base.OnModelCreating (modelBuilder);
     }
   }
 }