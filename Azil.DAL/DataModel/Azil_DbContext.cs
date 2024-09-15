using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Azil.DAL.DataModel
{
    public partial class Azil_DbContext : DbContext
    {
        public Azil_DbContext()
        {
        }

        public Azil_DbContext(DbContextOptions<Azil_DbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<DnevnikUdomljavanja> DnevnikUdomljavanja { get; set; }
        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<KorisnikUloga> KorisnikUloga { get; set; }
        public virtual DbSet<KucniLjubimci> KucniLjubimci { get; set; }
        public virtual DbSet<KucniLjubimciUdomitelj> KucniLjubimciUdomitelj { get; set; }
        public virtual DbSet<Uloge> Uloge { get; set; }
        public virtual DbSet<OdbijeneZivotinje> OdbijeneZivotinje { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DnevnikUdomljavanja>(entity =>
            {
                entity.HasKey(e => e.id_ljubimca);

                entity.ToTable("dnevnik_udomljavanja");

                entity.Property(e => e.id_korisnika).
                HasColumnName("id_korisnika");

                entity.Property(e => e.ime_ljubimca)
                    .HasColumnName("ime_ljubimca")
                    .IsUnicode(false);

                entity.Property(e => e.tip_ljubimca)
                    .HasColumnName("tip_ljubimca")
                    .IsUnicode(false);

                entity.Property(e => e.udomljen)
                    .HasColumnName("udomljen")
                    .IsUnicode(false);

                entity.Property(e => e.datum)
                    .HasColumnName("datum")
                    .IsUnicode(false);

                entity.Property(e => e.vrijeme)
                    .HasColumnName("vrijeme")
                    .IsUnicode(false);

                entity.Property(e => e.imgUrl)
                    .HasColumnName("imgUrl")
                    .IsUnicode(false);

                entity.Property(e => e.stanje_zivotinje)
                    .HasColumnName("stanje_zivotinje");

                entity.Property(e => e.status_udomljavanja)
                   .HasColumnName("status_udomljavanja");
            });

            modelBuilder.Entity<Korisnici>(entity =>
            {
                entity.HasKey(e => e.id_korisnika);

                entity.ToTable("korisnici");

                entity.Property(e => e.id_korisnika).HasColumnName("id_korisnika");

                entity.Property(e => e.ime)
                    .HasColumnName("ime")
                    .IsUnicode(false);

                entity.Property(e => e.email)
                    .HasColumnName("email")
                    .IsUnicode(false);

                entity.Property(e => e.lozinka)
                    .HasColumnName("lozinka")
                    .IsUnicode(false);

                entity.Property(e => e.admin)
                    .HasColumnName("admin")
                    .IsUnicode(false);

                entity.Property(e => e.profileImg)
                    .HasColumnName("profileImg")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KorisnikUloga>(entity =>
            {
                entity.HasKey(e => e.id_korisnika);

                entity.ToTable("korisnik_uloga");

                entity.HasAlternateKey(e => e.id_uloge);

                entity.Property(e => e.datum_od)
                    .HasColumnName("datum_od")
                    .IsUnicode(false);

                entity.Property(e => e.datum_do)
                    .HasColumnName("datum_do")
                    .IsUnicode(false);

                entity.HasOne(d => d.Uloge)
                    .WithMany()
                    .HasForeignKey(d => d.id_uloge);
            });

            modelBuilder.Entity<KucniLjubimci>(entity =>
            {
                entity.HasKey(e => e.id_ljubimca);

                entity.ToTable("kucni_ljubimci");

                entity.Property(e => e.id_udomitelja)
                    .HasColumnName("id_udomitelja");

                entity.Property(e => e.ime_ljubimca)
                    .HasColumnName("ime_ljubimca")
                    .IsUnicode(false);

                entity.Property(e => e.tip_ljubimca)
                    .HasColumnName("tip_ljubimca")
                    .IsUnicode(false);

                entity.Property(e => e.opis_ljubimca)
                    .HasColumnName("opis_ljubimca")
                    .IsUnicode(false);

                entity.Property(e => e.udomljen)
                    .HasColumnName("udomljen")
                    .IsUnicode(false);

                entity.Property(e => e.imgUrl)
                    .HasColumnName("imgUrl")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<KucniLjubimciUdomitelj>(entity =>
            {
                entity.HasKey(e => e.id_ljubimca);

                entity.ToTable("kucni_ljubimci_udomitelj");

                entity.HasAlternateKey(e => e.id_udomitelja);
            });

            modelBuilder.Entity<Uloge>(entity =>
            {
                entity.HasKey(e => e.id_uloge);

                entity.ToTable("uloge");

                entity.Property(e => e.naziv_uloge)
                    .HasColumnName("naziv_uloge")
                    .IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<OdbijeneZivotinje>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.ToTable("odbijene_zivotinje");

                entity.Property(e => e.id_ljubimca)
                    .HasColumnName("id_ljubimca")
                    .IsUnicode(false);

                entity.Property(e => e.id_korisnika)
                    .HasColumnName("id_korisnika")
                    .IsUnicode(false);

                entity.Property(e => e.ime_ljubimca)
                    .HasColumnName("ime_ljubimca")
                    .IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}