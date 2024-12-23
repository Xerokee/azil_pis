using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Azil.WebAPI.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DnevnikUdomljavanja> DnevnikUdomljavanja { get; set; }
        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<KorisnikUloga> KorisnikUloga { get; set; }
        public virtual DbSet<KucniLjubimci> KucniLjubimci { get; set; }
        public virtual DbSet<KucniLjubimciUdomitelj> KucniLjubimciUdomitelj { get; set; }
        public virtual DbSet<Uloge> Uloge { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=193.198.57.183,7081;Database=MargetaAzil;User Id=mmargeta;Password=Vuvo125!;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DnevnikUdomljavanja>(entity =>
            {
                entity.HasKey(e => e.IdLjubimca);

                entity.ToTable("dnevnik_udomljavanja");

                entity.Property(e => e.IdLjubimca)
                    .HasColumnName("id_ljubimca")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImeLjubimca)
                    .HasColumnName("ime_ljubimca")
                    .HasColumnType("text");

                entity.Property(e => e.TipLjubimca)
                    .HasColumnName("tip_ljubimca")
                    .HasColumnType("text");

                entity.Property(e => e.Udomljen)
                    .HasColumnName("udomljen")
                    .HasColumnType("bool");

                entity.Property(e => e.Datum)
                    .HasColumnName("datum")
                    .HasColumnType("date");

                entity.Property(e => e.Vrijeme)
                    .HasColumnName("vrijeme")
                    .HasColumnType("time");

                entity.Property(e => e.ImgUrl)
                    .HasColumnName("imgUrl")
                    .HasColumnType("text");

                entity.Property(e => e.IdKorisnika).HasColumnName("id_korisnika");

                entity.Property(e => e.StanjeZivotinje)
                    .HasColumnName("stanje_zivotinje")
                    .HasColumnType("bool");

                entity.Property(e => e.StatusUdomljavanja)
                    .HasColumnName("status_udomljavanja")
                    .HasColumnType("bool");
            });

            modelBuilder.Entity<Korisnici>(entity =>
            {
                entity.HasKey(e => e.IdKorisnika)
                    .HasName("PK__korisnic__A34FFD245548701B");

                entity.ToTable("korisnici");

                entity.Property(e => e.IdKorisnika)
                    .HasColumnName("id_korisnika")
                    .ValueGeneratedNever();

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("text");

                entity.Property(e => e.KorisnickoIme)
                    .HasColumnName("korisnickoIme")
                    .HasColumnType("text");

                entity.Property(e => e.Ime)
                    .HasColumnName("ime")
                    .HasColumnType("text");

                entity.Property(e => e.Prezime)
                    .HasColumnName("prezime")
                    .HasColumnType("text");

                entity.Property(e => e.Lozinka).HasColumnName("lozinka");

                entity.HasOne(d => d.IdKorisnikaNavigation)
                    .WithOne(p => p.Korisnici)
                    .HasForeignKey<Korisnici>(d => d.IdKorisnika)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_korisnici_dnevnik_udomljavanja");
            });

            modelBuilder.Entity<KorisnikUloga>(entity =>
            {
                entity.HasKey(e => new { e.IdKorisnika, e.IdUloge });

                entity.ToTable("korisnik_uloga");

                entity.Property(e => e.IdKorisnika).HasColumnName("id_korisnika");

                entity.Property(e => e.IdUloge).HasColumnName("id_uloge");

                entity.Property(e => e.DatumDo)
                    .HasColumnName("datum_do")
                    .HasColumnType("date");

                entity.Property(e => e.DatumOd)
                    .HasColumnName("datum_od")
                    .HasColumnType("date");

                entity.HasOne(d => d.IdKorisnikaNavigation)
                    .WithMany(p => p.KorisnikUloga)
                    .HasForeignKey(d => d.IdKorisnika)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_korisnik_uloga_uloge");
            });

            modelBuilder.Entity<KucniLjubimci>(entity =>
            {
                entity.HasKey(e => e.IdLjubimca)
                    .HasName("PK__kucni_Lj__75F531F53E3CDFAE");

                entity.ToTable("kucni_Ljubimci");

                entity.Property(e => e.IdLjubimca)
                    .HasColumnName("id_ljubimca")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdUdomitelja).HasColumnName("id_udomitelja");

                entity.Property(e => e.ImeLjubimca)
                    .HasColumnName("ime_ljubimca")
                    .HasColumnType("text");

                entity.Property(e => e.OpisLjubimca)
                    .HasColumnName("opis_ljubimca")
                    .HasColumnType("text");

                entity.Property(e => e.TipLjubimca)
                    .HasColumnName("tip_ljubimca")
                    .HasColumnType("text");

                entity.Property(e => e.Udomljen).HasColumnName("udomljen");

                entity.Property(e => e.ZahtjevUdomljen).HasColumnName("zahtjev_udomljen");

                entity.Property(e => e.ImgUrl)
                    .HasColumnName("imgUrl")
                    .HasColumnType("text");

                entity.HasMany(e => e.GalerijaZivotinja)
                    .WithOne()
                    .HasForeignKey(g => g.IdLjubimca);

                entity.Property(e => e.Dob)
                    .HasColumnName("dob");

                entity.Property(e => e.Boja)
                    .HasColumnName("boja")
                    .HasColumnType("text");

                entity.HasOne(d => d.IdLjubimcaNavigation)
                    .WithOne(p => p.KucniLjubimci)
                    .HasForeignKey<KucniLjubimci>(d => d.IdLjubimca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_kucni_Ljubimci_dnevnik_udomljavanja");
            });

            modelBuilder.Entity<GalerijaZivotinja>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("galerija_zivotinja");

                entity.Property(e => e.IdLjubimca)
                    .HasColumnName("id_ljubimca");

                entity.Property(e => e.ImgUrl)
                    .HasColumnName("imgUrl");
            });

            modelBuilder.Entity<KucniLjubimciUdomitelj>(entity =>
            {
                entity.HasKey(e => new { e.IdLjubimca, e.IdUdomitelja });

                entity.ToTable("kucni_ljubimci_udomitelj");

                entity.Property(e => e.IdLjubimca).HasColumnName("id_ljubimca");

                entity.Property(e => e.IdUdomitelja).HasColumnName("id_udomitelja");

                entity.HasOne(d => d.IdLjubimcaNavigation)
                    .WithMany(p => p.KucniLjubimciUdomitelj)
                    .HasForeignKey(d => d.IdLjubimca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_kucni_ljubimci_udomitelj_kucni_ljubimci");
            });

            modelBuilder.Entity<Uloge>(entity =>
            {
                entity.HasKey(e => e.IdUloge)
                    .HasName("PK__uloge__493A5F1712BCE163");

                entity.ToTable("uloge");

                entity.Property(e => e.IdUloge)
                    .HasColumnName("id_uloge")
                    .ValueGeneratedNever();

                entity.Property(e => e.NazivUloge)
                    .HasColumnName("naziv_uloge")
                    .HasColumnType("text");

                entity.HasOne(d => d.IdUlogeNavigation)
                    .WithOne(p => p.Uloge)
                    .HasForeignKey<Uloge>(d => d.IdUloge)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_uloge_korisnici");
            });

            modelBuilder.Entity<OdbijeneZivotinje>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__odbijene__3213E83F5BC9A5A8");

                entity.ToTable("odbijene_zivotinje");

                entity.Property(e => e.IdLjubimca)
                    .HasColumnName("id_ljubimca")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdKorisnika)
                    .HasColumnName("id_korisnika")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImeLjubimca)
                    .HasColumnName("ime_ljubimca")
                    .ValueGeneratedNever();

                entity.Property(e => e.ZahtjevUdomljen).HasColumnName("zahtjev_udomljen");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
