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
        public DbSet<SubmissionLog> SubmissionLogs { get; set; }

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
                new Milestone { Id = 1, Name = "Bid Preparation", DepartmentId = 1 },
                new Milestone { Id = 2, Name = "Pipeline creation", DepartmentId = 1 },
                new Milestone { Id = 3, Name = "BD support work", DepartmentId = 1 },
                new Milestone { Id = 4, Name = "Bid analysis / reporting", DepartmentId = 1 },
                new Milestone { Id = 5, Name = "Learning and development", DepartmentId = 1 },
                new Milestone { Id = 6, Name = "Audit & Compliances", DepartmentId = 1 },
                new Milestone { Id = 7, Name = "Additional", DepartmentId = 1 },

                // Operations
                new Milestone { Id = 8, Name = "Invoicing", DepartmentId = 2 },
                new Milestone { Id = 9, Name = "Procurement", DepartmentId = 2 },
                new Milestone { Id = 10, Name = "ERP", DepartmentId = 2 },
                new Milestone { Id = 11, Name = "Contract Management", DepartmentId = 2 },
                new Milestone { Id = 12, Name = "Project/ Business Performance Analysis and Reporting", DepartmentId = 2 },
                new Milestone { Id = 13, Name = "ERP Customization", DepartmentId = 2 },
                new Milestone { Id = 14, Name = "Additional", DepartmentId = 2 },

                // Finance & Payroll
                new Milestone { Id = 15, Name = "Financial Statements preparation and analysis", DepartmentId = 3 },
                new Milestone { Id = 16, Name = "Payments", DepartmentId = 3 },
                new Milestone { Id = 17, Name = "Taxation", DepartmentId = 3 },
                new Milestone { Id = 18, Name = "Banking", DepartmentId = 3 },
                new Milestone { Id = 19, Name = "Learning and Development", DepartmentId = 3 },
                new Milestone { Id = 20, Name = "Audit & Compliances", DepartmentId = 3 },
                new Milestone { Id = 21, Name = "Additional", DepartmentId = 3 },

                // HR
                new Milestone { Id = 22, Name = "Talent acquisition", DepartmentId = 4 },
                new Milestone { Id = 23, Name = "HR Administration", DepartmentId = 4 },
                new Milestone { Id = 24, Name = "Performance Management, R&R", DepartmentId = 4 },
                new Milestone { Id = 25, Name = "Succession Planning/Leadership Development", DepartmentId = 4 },
                new Milestone { Id = 26, Name = "Training & Development", DepartmentId = 4 },
                new Milestone { Id = 27, Name = "Audit & Compliances", DepartmentId = 4 },
                new Milestone { Id = 28, Name = "Additional", DepartmentId = 4 },

                // IT
                new Milestone { Id = 29, Name = "Hardware/Software procurement", DepartmentId = 5 },
                new Milestone { Id = 30, Name = "IT Support/troubleshooting", DepartmentId = 5 },
                new Milestone { Id = 31, Name = "Network/Server maintenance", DepartmentId = 5 },
                new Milestone { Id = 32, Name = "Digital innovation support", DepartmentId = 5 },
                new Milestone { Id = 33, Name = "Learning and Development", DepartmentId = 5 },
                new Milestone { Id = 34, Name = "SLA/Policy Compliance", DepartmentId = 5 },
                new Milestone { Id = 35, Name = "Additional", DepartmentId = 5 },

                // Marketing (DepartmentId = 6)
                new Milestone { Id = 36, Name = "Brand/Corporate Communication", DepartmentId = 6 },
                new Milestone { Id = 37, Name = "Marketing Material development", DepartmentId = 6 },
                new Milestone { Id = 38, Name = "Digital Marketing", DepartmentId = 6 },
                new Milestone { Id = 39, Name = "Association Membership and networking", DepartmentId = 6 },
                new Milestone { Id = 40, Name = "Learning & Development", DepartmentId = 6 },
                new Milestone { Id = 41, Name = "Audit/ Compliances", DepartmentId = 6 },
                new Milestone { Id = 42, Name = "Additional", DepartmentId = 6 },

                // Legal (DepartmentId = 7)
                new Milestone { Id = 43, Name = "Contract drafting & Review", DepartmentId = 7 },
                new Milestone { Id = 44, Name = "Handling legal issues and cases", DepartmentId = 7 },
                new Milestone { Id = 45, Name = "REIT", DepartmentId = 7 },
                new Milestone { Id = 46, Name = "Learning & Development", DepartmentId = 7 },
                new Milestone { Id = 47, Name = "Audit/ Compliances", DepartmentId = 7 },
                new Milestone { Id = 48, Name = "Additional", DepartmentId = 7 },

                // Admin/Facilities (DepartmentId = 8)
                new Milestone { Id = 49, Name = "Hiring/ de-hiring of premises/ Guest Houses", DepartmentId = 8 },
                new Milestone { Id = 50, Name = "Procurement/Disposal of furniture, fixtures, monthly consumables", DepartmentId = 8 },
                new Milestone { Id = 51, Name = "Travel Booking", DepartmentId = 8 },
                new Milestone { Id = 52, Name = "Handling office boys, security", DepartmentId = 8 },
                new Milestone { Id = 53, Name = "Learning & Development", DepartmentId = 8 },
                new Milestone { Id = 54, Name = "Audit/ Compliances", DepartmentId = 8 },
                new Milestone { Id = 55, Name = "Additional", DepartmentId = 8 },

                // Company Secretary (DepartmentId = 9)
                new Milestone { Id = 56, Name = "Secretarial", DepartmentId = 9 },
                new Milestone { Id = 57, Name = "Fund Raising", DepartmentId = 9 },
                new Milestone { Id = 58, Name = "Acquisition", DepartmentId = 9 },
                new Milestone { Id = 59, Name = "REIT", DepartmentId = 9 },
                new Milestone { Id = 60, Name = "Learning & Development", DepartmentId = 9 },
                new Milestone { Id = 61, Name = "Audit & Compliances", DepartmentId = 9 },
                new Milestone { Id = 62, Name = "Additional", DepartmentId = 9 },

                // ED (DepartmentId = 10)
                new Milestone { Id = 63, Name = "Business Development - Pipeline creation", DepartmentId = 10 },
                new Milestone { Id = 64, Name = "Business Development - Bidding", DepartmentId = 10 },
                new Milestone { Id = 65, Name = "New Product/service development", DepartmentId = 10 },
                new Milestone { Id = 66, Name = "Business Performance support and Analysis", DepartmentId = 10 },
                new Milestone { Id = 67, Name = "Learning & Development", DepartmentId = 10 },
                new Milestone { Id = 68, Name = "Audit & Compliances", DepartmentId = 10 },
                new Milestone { Id = 69, Name = "Additional", DepartmentId = 10 },

                // Business Vertical Head (DepartmentId = 11)
                new Milestone { Id = 70, Name = "Business Development - Pipeline creation", DepartmentId = 11 },
                new Milestone { Id = 71, Name = "Business Development - Bidding", DepartmentId = 11 },
                new Milestone { Id = 72, Name = "Project Quality Delivery", DepartmentId = 11 },
                new Milestone { Id = 73, Name = "Project and Business Administration", DepartmentId = 11 },
                new Milestone { Id = 74, Name = "Learning & Development", DepartmentId = 11 },
                new Milestone { Id = 75, Name = "Audit & Compliances", DepartmentId = 11 },
                new Milestone { Id = 76, Name = "Additional", DepartmentId = 11 },

                // Project Managers (DepartmentId = 12)
                new Milestone { Id = 77, Name = "Business Development - Pipeline creation", DepartmentId = 12 },
                new Milestone { Id = 78, Name = "Business Development - Bidding", DepartmentId = 12 },
                new Milestone { Id = 79, Name = "Project Quality Delivery", DepartmentId = 12 },
                new Milestone { Id = 80, Name = "Project Commercial success", DepartmentId = 12 },
                new Milestone { Id = 81, Name = "Learning & Development", DepartmentId = 12 },
                new Milestone { Id = 82, Name = "Audit & Compliances", DepartmentId = 12 },
                new Milestone { Id = 83, Name = "Additional", DepartmentId = 12 },

                // Bid Managers (DepartmentId = 13)
                new Milestone { Id = 84, Name = "Business Development - Pipeline creation", DepartmentId = 13 },
                new Milestone { Id = 85, Name = "Business Development - Bidding", DepartmentId = 13 },
                new Milestone { Id = 86, Name = "Bid Quality Delivery", DepartmentId = 13 },
                new Milestone { Id = 87, Name = "Bid Commercial success", DepartmentId = 13 },
                new Milestone { Id = 88, Name = "Learning & Development", DepartmentId = 13 },
                new Milestone { Id = 89, Name = "Audit & Compliances", DepartmentId = 13 },
                new Milestone { Id = 90, Name = "Additional", DepartmentId = 13 },

                // Project Coordinators (DepartmentId = 14)
                new Milestone { Id = 91, Name = "Business Development - Pipeline creation", DepartmentId = 14 },
                new Milestone { Id = 92, Name = "Business Development - Bidding", DepartmentId = 14 },
                new Milestone { Id = 93, Name = "Project Quality Delivery", DepartmentId = 14 },
                new Milestone { Id = 94, Name = "Project Commercial success", DepartmentId = 14 },
                new Milestone { Id = 95, Name = "Learning & Development", DepartmentId = 14 },
                new Milestone { Id = 96, Name = "Audit & Compliances", DepartmentId = 14 },
                new Milestone { Id = 97, Name = "Additional", DepartmentId = 14 },

                // Project Team Members (DepartmentId = 15)
                new Milestone { Id = 98, Name = "Learning & Development", DepartmentId = 15 },
                new Milestone { Id = 99, Name = "Audit & Compliances", DepartmentId = 15 },
                new Milestone { Id = 100, Name = "Additional", DepartmentId = 15 }
            );

            //modelBuilder.Entity<Employee>()
            //.HasMany(s => s.Projects)
            //.WithMany(c => c.Employees)
            //.UsingEntity(j => j.ToTable("EmployeeProjects"));
        }
    }
}

