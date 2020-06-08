using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cw11.Models
{
    public class ClinicCodeFirstContext : DbContext
    {

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
        public ClinicCodeFirstContext(DbContextOptions<ClinicCodeFirstContext> options) : base(options)
        {
        }

        public ClinicCodeFirstContext() 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(Pt => Pt.IdPatient).HasName("Patient_PK");

                entity.Property(Pt => Pt.FirstName).HasMaxLength(100).IsRequired();

                entity.Property(Pt => Pt.LastName).HasMaxLength(100).IsRequired();

                entity.Property(Pt => Pt.Birthdate).IsRequired();
            });

            modelBuilder.Entity<Prescription>(entity =>
            {
                entity.HasKey(Pr => Pr.IdPrescription).HasName("Prescription_PK");

                entity.Property(Pr => Pr.Date).IsRequired();

                entity.Property(Pr => Pr.DueDate).IsRequired();

                entity.HasOne(Pr => Pr.Patient)
                .WithMany(Pr => Pr.Prescriptions).HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Prescription_Patient");

                entity.HasOne(Dr => Dr.Doctor).
                WithMany(Dr => Dr.Prescriptions).HasForeignKey(d => d.IdDoctor).
                OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Prescription_Doctor");


            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.HasKey(Dr => Dr.IdDoctor).HasName("Doctor_PK");

                entity.Property(Dr => Dr.FirstName).HasMaxLength(100).IsRequired();

                entity.Property(Dr => Dr.LastName).HasMaxLength(100).IsRequired();

                entity.Property(Dr => Dr.Email).HasMaxLength(100).IsRequired();

                

            });

            modelBuilder.Entity<Prescription_Medicament>(entity =>
            {

                entity.ToTable("Prescription_Medicament");

                entity.HasKey(Pm => new { Pm.IdMedicament, Pm.IdPrescription }).HasName("PrescriptionMedicament_PK");

                entity.Property(Pm => Pm.IdMedicament).ValueGeneratedNever();

                entity.Property(Pm => Pm.IdPrescription).ValueGeneratedNever();

                entity.Property(Pm => Pm.Details).HasMaxLength(100).IsRequired();

                entity.Property(Pm => Pm.Dose).IsRequired();

                entity.HasOne(m => m.Medicament).
                WithMany(m => m.PrescriptionMedicaments).HasForeignKey(d => d.IdMedicament).
                OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("PM_Medicament");

                entity.HasOne(d => d.Prescription).
                WithMany(p => p.PrescriptionMedicaments).HasForeignKey(d => d.IdPrescription).
                OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("PM_Prescription");
            });
            Seed(modelBuilder);
        }

        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient { IdPatient = 01, FirstName = "Grzegorz", LastName = "Filipkowski", Birthdate = DateTime.Parse("03-12-1998") },
                new Patient { IdPatient = 02, FirstName = "Lucja", LastName = "Sobczak", Birthdate = DateTime.Parse("10-10-1975") }
                );


            modelBuilder.Entity<Doctor>().HasData(
                new Doctor { IdDoctor = 01, FirstName = "Jerzy", LastName = "Gołab", Email = "jerzy.golab@gmail.com" },
                new Doctor { IdDoctor = 02, FirstName = "Marcin", LastName = "Grzesiak", Email = "MarcinGrzesiak@wp.pl" },
                new Doctor { IdDoctor = 03, FirstName = "Hugo", LastName = "Kołątaj", Email = "hkolataj@op.pl" }
                );


            modelBuilder.Entity<Medicament>().HasData(
                new Medicament { IdMedicament = 01, Name = "Duomox", Description = "Obniża gorączkę", Type = "Antybiotyk" },
                new Medicament { IdMedicament = 02, Name = "Aftin", Description = "Na afty", Type = "Lek zwalczający afty" }
                );


            modelBuilder.Entity<Prescription>().HasData(
                new Prescription { IdPrescription = 01, Date = DateTime.Now, DueDate = DateTime.Parse("08-04-2020"), IdPatient = 1, IdDoctor = 2 },
                new Prescription { IdPrescription = 02, Date = DateTime.Now, DueDate = DateTime.Parse("15-05-2020"), IdPatient = 2, IdDoctor = 2 },
                new Prescription { IdPrescription = 03, Date = DateTime.Now, DueDate = DateTime.Parse("07-07-2020"), IdPatient = 2, IdDoctor = 3 }
                );


            modelBuilder.Entity<Prescription_Medicament>().HasData(
                new Prescription_Medicament { Dose = 01, Details = "Stosować przez sześć dni co 12h", IdMedicament = 1, IdPrescription = 1 },
                new Prescription_Medicament { Dose = 02, Details = "Stosować trzy dziennie do ustąpienia objawów", IdMedicament = 2, IdPrescription = 2 }
                );

        }
    }
}
