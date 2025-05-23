using System.Xml;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Models;

namespace TimeSheet.Data.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkingHours> WorkingHours { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMilestone> ProjectMilestones { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Employee>()
                .HasMany(w => w.WorkingHours)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId);
            modelBuilder.Entity<Employee>()
                .HasMany(w => w.Timesheets)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId);

            modelBuilder.Entity<Project>()
                .HasMany(w => w.ProjectMilestones)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);
            
            modelBuilder.Entity<ProjectMilestone>()
                .HasMany(w => w.Timesheets)
                .WithOne(t => t.ProjectMilestone)
                .HasForeignKey(t => t.ProjectMilestoneId);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Business Development" },
                new Department { Id = 2, Name = "Operations" },
                new Department { Id = 3, Name = "Finance & Payroll" },
                new Department { Id = 4, Name = "HR" },
                new Department { Id = 5, Name = "IT" },
                new Department { Id = 6, Name = "Marketing" },
                new Department { Id = 7, Name = "Legal" },
                new Department { Id = 8, Name = "Admin/Facilities" },
                new Department { Id = 9, Name = "Company Secretary" },
                new Department { Id = 10, Name = "ED" },
                new Department { Id = 11, Name = "Business Vertical Head" },
                new Department { Id = 12, Name = "Project Managers" },
                new Department { Id = 13, Name = "Bid Managers" },
                new Department { Id = 14, Name = "Project Coordinators" },
                new Department { Id = 15, Name = "Project Team Members" }
            );

            modelBuilder.Entity<Milestone>().HasData(
                // Business Development
                new Milestone { Id = 1, Name = "Bid Preparation", Description = "CV search, CV preparation, formatting, fee sheet, A & M, EMD Preparation, Proposal compilation", DepartmentId = 1 },
                new Milestone { Id = 2, Name = "Pipeline creation", Description = "Tender scanning, meeting prospective clients, Networking with tenderers", DepartmentId = 1 },
                new Milestone { Id = 3, Name = "BD support work", Description = "capdoc, brochure preparation, project Information sheet", DepartmentId = 1 },
                new Milestone { Id = 4, Name = "Bid analysis / reporting", Description = "", DepartmentId = 1 },
                new Milestone { Id = 5, Name = "Learning and development", Description = "", DepartmentId = 1 },
                new Milestone { Id = 6, Name = "Audit & Compliances", Description = "", DepartmentId = 1 },
                new Milestone { Id = 7, Name = "Additional", Description = "", DepartmentId = 1 },

                // Operations
                new Milestone { Id = 8, Name = "Invoicing", Description = "", DepartmentId = 2 },
                new Milestone { Id = 9, Name = "Procurement", Description = "", DepartmentId = 2 },
                new Milestone { Id = 10, Name = "ERP", Description = "", DepartmentId = 2 },
                new Milestone { Id = 11, Name = "Contract Management", Description = "", DepartmentId = 2 },
                new Milestone { Id = 12, Name = "Project/ Business Performance Analysis and Reporting", Description = "", DepartmentId = 2 },
                new Milestone { Id = 13, Name = "ERP Customization", Description = "", DepartmentId = 2 },
                new Milestone { Id = 14, Name = "Additional", Description = "", DepartmentId = 2 },

                // Finance & Payroll
                new Milestone { Id = 15, Name = "Financial Statements preparation and analysis", Description = "", DepartmentId = 3 },
                new Milestone { Id = 16, Name = "Payments", Description = "", DepartmentId = 3 },
                new Milestone { Id = 17, Name = "Taxation", Description = "", DepartmentId = 3 },
                new Milestone { Id = 18, Name = "Banking", Description = "", DepartmentId = 3 },
                new Milestone { Id = 19, Name = "Learning and Development", Description = "", DepartmentId = 3 },
                new Milestone { Id = 20, Name = "Audit & Compliances", Description = "", DepartmentId = 3 },
                new Milestone { Id = 21, Name = "Additional", Description = "", DepartmentId = 3 },

                // HR
                new Milestone { Id = 22, Name = "Talent acquisition", Description = "", DepartmentId = 4 },
                new Milestone { Id = 23, Name = "HR Administration", Description = "", DepartmentId = 4 },
                new Milestone { Id = 24, Name = "Performance Management, R&R", Description = "", DepartmentId = 4 },
                new Milestone { Id = 25, Name = "Succession Planning/Leadership Development", Description = "", DepartmentId = 4 },
                new Milestone { Id = 26, Name = "Training & Development", Description = "", DepartmentId = 4 },
                new Milestone { Id = 27, Name = "Audit & Compliances", Description = "", DepartmentId = 4 },
                new Milestone { Id = 28, Name = "Additional", Description = "", DepartmentId = 4 },

                // IT
                new Milestone { Id = 29, Name = "Hardware/Software procurement", Description = "", DepartmentId = 5 },
                new Milestone { Id = 30, Name = "IT Support/troubleshooting", Description = "", DepartmentId = 5 },
                new Milestone { Id = 31, Name = "Network/Server maintenance", Description = "", DepartmentId = 5 },
                new Milestone { Id = 32, Name = "Digital innovation support", Description = "", DepartmentId = 5 },
                new Milestone { Id = 33, Name = "Learning and Development", Description = "", DepartmentId = 5 },
                new Milestone { Id = 34, Name = "SLA/Policy Compliance", Description = "", DepartmentId = 5 },
                new Milestone { Id = 35, Name = "Additional", Description = "", DepartmentId = 5 },

                // Marketing (DepartmentId = 6)
                new Milestone { Id = 36, Name = "Brand/Corporate Communication", Description = "", DepartmentId = 6 },
                new Milestone { Id = 37, Name = "Marketing Material development", Description = "", DepartmentId = 6 },
                new Milestone { Id = 38, Name = "Digital Marketing", Description = "", DepartmentId = 6 },
                new Milestone { Id = 39, Name = "Association Membership and networking", Description = "", DepartmentId = 6 },
                new Milestone { Id = 40, Name = "Learning & Development", Description = "", DepartmentId = 6 },
                new Milestone { Id = 41, Name = "Audit/ Compliances", Description = "", DepartmentId = 6 },
                new Milestone { Id = 42, Name = "Additional", Description = "", DepartmentId = 6 },

                // Legal (DepartmentId = 7)
                new Milestone { Id = 43, Name = "Contract drafting & Review", Description = "", DepartmentId = 7 },
                new Milestone { Id = 44, Name = "Handling legal issues and cases", Description = "", DepartmentId = 7 },
                new Milestone { Id = 45, Name = "REIT", Description = "", DepartmentId = 7 },
                new Milestone { Id = 46, Name = "Learning & Development", Description = "", DepartmentId = 7 },
                new Milestone { Id = 47, Name = "Audit/ Compliances", Description = "", DepartmentId = 7 },
                new Milestone { Id = 48, Name = "Additional", Description = "", DepartmentId = 7 },

                // Admin/Facilities (DepartmentId = 8)
                new Milestone { Id = 49, Name = "Hiring/ de-hiring of premises/ Guest Houses", Description = "", DepartmentId = 8 },
                new Milestone { Id = 50, Name = "Procurement/Disposal of furniture, fixtures, monthly consumables", Description = "Maintaining Asset Register and tracking asset utilisation", DepartmentId = 8 },
                new Milestone { Id = 51, Name = "Travel Booking", Description = "Air/rail/Cab booking", DepartmentId = 8 },
                new Milestone { Id = 52, Name = "Handling office boys, security", Description = "", DepartmentId = 8 },
                new Milestone { Id = 53, Name = "Learning & Development", Description = "", DepartmentId = 8 },
                new Milestone { Id = 54, Name = "Audit/ Compliances", Description = "", DepartmentId = 8 },
                new Milestone { Id = 55, Name = "Additional", Description = "", DepartmentId = 8 },

                // Company Secretary (DepartmentId = 9)
                new Milestone { Id = 56, Name = "Secretarial", Description = "", DepartmentId = 9 },
                new Milestone { Id = 57, Name = "Fund Raising", Description = "", DepartmentId = 9 },
                new Milestone { Id = 58, Name = "Acquisition", Description = "", DepartmentId = 9 },
                new Milestone { Id = 59, Name = "REIT", Description = "", DepartmentId = 9 },
                new Milestone { Id = 60, Name = "Learning & Development", Description = "", DepartmentId = 9 },
                new Milestone { Id = 61, Name = "Audit & Compliances", Description = "", DepartmentId = 9 },
                new Milestone { Id = 62, Name = "Additional", Description = "", DepartmentId = 9 },

                // ED (DepartmentId = 10)
                new Milestone { Id = 63, Name = "Business Development - Pipeline creation", Description = "", DepartmentId = 10 },
                new Milestone { Id = 64, Name = "Business Development - Bidding", Description = "", DepartmentId = 10 },
                new Milestone { Id = 65, Name = "New Product/service development", Description = "", DepartmentId = 10 },
                new Milestone { Id = 66, Name = "Business Performance support and Analysis", Description = "", DepartmentId = 10 },
                new Milestone { Id = 67, Name = "Learning & Development", Description = "", DepartmentId = 10 },
                new Milestone { Id = 68, Name = "Audit & Compliances", Description = "", DepartmentId = 10 },
                new Milestone { Id = 69, Name = "Additional", Description = "", DepartmentId = 10 },

                // Business Vertical Head (DepartmentId = 11)
                new Milestone { Id = 70, Name = "Business Development - Pipeline creation", Description = "", DepartmentId = 11 },
                new Milestone { Id = 71, Name = "Business Development - Bidding", Description = "", DepartmentId = 11 },
                new Milestone { Id = 72, Name = "Project Quality Delivery", Description = "", DepartmentId = 11 },
                new Milestone { Id = 73, Name = "Project and Business Administration", Description = "", DepartmentId = 11 },
                new Milestone { Id = 74, Name = "Learning & Development", Description = "", DepartmentId = 11 },
                new Milestone { Id = 75, Name = "Audit & Compliances", Description = "", DepartmentId = 11 },
                new Milestone { Id = 76, Name = "Additional", Description = "", DepartmentId = 11 },

                // Project Managers (DepartmentId = 12)
                new Milestone { Id = 77, Name = "Business Development - Pipeline creation", Description = "", DepartmentId = 12 },
                new Milestone { Id = 78, Name = "Business Development - Bidding", Description = "", DepartmentId = 12 },
                new Milestone { Id = 79, Name = "Project Quality Delivery", Description = "", DepartmentId = 12 },
                new Milestone { Id = 80, Name = "Project Commercial success", Description = "", DepartmentId = 12 },
                new Milestone { Id = 81, Name = "Learning & Development", Description = "", DepartmentId = 12 },
                new Milestone { Id = 82, Name = "Audit & Compliances", Description = "", DepartmentId = 12 },
                new Milestone { Id = 83, Name = "Additional", Description = "", DepartmentId = 12 },

                // Bid Managers (DepartmentId = 13)
                new Milestone { Id = 84, Name = "Business Development - Pipeline creation", Description = "", DepartmentId = 13 },
                new Milestone { Id = 85, Name = "Business Development - Bidding", Description = "", DepartmentId = 13 },
                new Milestone { Id = 86, Name = "Bid Quality Delivery", Description = "", DepartmentId = 13 },
                new Milestone { Id = 87, Name = "Bid Commercial success", Description = "", DepartmentId = 13 },
                new Milestone { Id = 88, Name = "Learning & Development", Description = "", DepartmentId = 13 },
                new Milestone { Id = 89, Name = "Audit & Compliances", Description = "", DepartmentId = 13 },
                new Milestone { Id = 90, Name = "Additional", Description = "", DepartmentId = 13 },

                // Project Coordinators (DepartmentId = 14)
                new Milestone { Id = 91, Name = "Business Development - Pipeline creation", Description = "", DepartmentId = 14 },
                new Milestone { Id = 92, Name = "Business Development - Bidding", Description = "", DepartmentId = 14 },
                new Milestone { Id = 93, Name = "Project Quality Delivery", Description = "", DepartmentId = 14 },
                new Milestone { Id = 94, Name = "Project Commercial success", Description = "", DepartmentId = 14 },
                new Milestone { Id = 95, Name = "Learning & Development", Description = "", DepartmentId = 14 },
                new Milestone { Id = 96, Name = "Audit & Compliances", Description = "", DepartmentId = 14 },
                new Milestone { Id = 97, Name = "Additional", Description = "", DepartmentId = 14 },

                // Project Team Members (DepartmentId = 15)
                new Milestone { Id = 98, Name = "Learning & Development", Description = "", DepartmentId = 15 },
                new Milestone { Id = 99, Name = "Audit & Compliances", Description = "", DepartmentId = 15 },
                new Milestone { Id = 100, Name = "Additional", Description = "", DepartmentId = 15 }
            );

            //modelBuilder.Entity<Employee>()
            //.HasMany(s => s.Projects)
            //.WithMany(c => c.Employees)
            //.UsingEntity(j => j.ToTable("EmployeeProjects"));
        }
    }
}

