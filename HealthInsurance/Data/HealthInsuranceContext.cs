using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HealthInsurance.Models;

namespace HealthInsurance.Data
{
    public class HealthInsuranceContext : DbContext
    {
        public HealthInsuranceContext (DbContextOptions<HealthInsuranceContext> options)
            : base(options)
        {
        }

        public DbSet<HealthInsurance.Models.User> User { get; set; } = default!;

        public DbSet<HealthInsurance.Models.Category>? Category { get; set; }

        public DbSet<HealthInsurance.Models.InsuranceProducts>? InsuranceProducts { get; set; }

        public DbSet<HealthInsurance.Models.BenefitDetail>? BenefitDetail { get; set; }

        public DbSet<HealthInsurance.Models.DiseaseList>? DiseaseList { get; set; }

        public DbSet<HealthInsurance.Models.OutstandingBenefit>? OutstandingBenefit { get; set; }

        public DbSet<HealthInsurance.Models.ParticipationInformation>? ParticipationInformation { get; set; }

        public DbSet<HealthInsurance.Models.PackageInsurance>? PackageInsurance { get; set; }

        public DbSet<HealthInsurance.Models.Document>? Document { get; set; }

        public DbSet<HealthInsurance.Models.UserInformation>? UserInformation { get; set; }

        public DbSet<HealthInsurance.Models.ContractInsurance>? ContractInsurance { get; set; }

        public DbSet<HealthInsurance.Models.ConsultMessage>? ConsultMessage { get; set; }

        

        
    }
}
