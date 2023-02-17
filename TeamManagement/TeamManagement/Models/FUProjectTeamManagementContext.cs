using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TeamManagement.Models
{
    public partial class FUProjectTeamManagementContext : DbContext
    {
        public FUProjectTeamManagementContext()
        {
        }

        public FUProjectTeamManagementContext(DbContextOptions<FUProjectTeamManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseTeam> CourseTeams { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Participant> Participants { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TeacherCourse> TeacherCourses { get; set; }
        public virtual DbSet<TeacherTeam> TeacherTeams { get; set; }
        public virtual DbSet<TeacherTopic> TeacherTopics { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamTopic> TeamTopics { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =(local); database = FUProjectTeamManagement;uid=sa;pwd=1;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.KeyEnroll)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sem)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SemId)
                    .HasConstraintName("FK__Course__SemId__5AEE82B9");

                entity.HasOne(d => d.Sub)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SubId)
                    .HasConstraintName("FK__Course__SubId__59FA5E80");
            });

            modelBuilder.Entity<CourseTeam>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.TeamId })
                    .HasName("PK__Course_T__480EDFDE2CF38614");

                entity.ToTable("Course_Team");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseTeams)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course_Te__Cours__6C190EBB");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.CourseTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course_Te__TeamI__6D0D32F4");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId)
                    .HasName("PK__Departme__014881AEACFA8C09");

                entity.ToTable("Department");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("Participant");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Participant_Course");

                entity.HasOne(d => d.Stu)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.StuId)
                    .HasConstraintName("FK_Participant_Student");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("FK_Participant_Team");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.HasKey(e => e.SemId)
                    .HasName("PK__Semester__16D6C7AADE6DA278");

                entity.ToTable("Semester");

                entity.Property(e => e.SemName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StuId)
                    .HasName("PK__Student__6CDFAB957983AFA6");

                entity.ToTable("Student");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.StuCode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StuEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StuGender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StuName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StuPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubId)
                    .HasName("PK__Subject__4D9BB84A6052D6E3");

                entity.ToTable("Subject");

                entity.Property(e => e.SubName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK__Subject__DeptId__4BAC3F29");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("Teacher");

                entity.Property(e => e.TeacherEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TeacherName).HasMaxLength(100);

                entity.Property(e => e.TeacherPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TeacherCourse>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.CourseId })
                    .HasName("PK__Teacher___81608E7E1ECBFE13");

                entity.ToTable("Teacher_Course");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeacherCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teacher_C__Cours__693CA210");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherCourses)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teacher_C__Teach__68487DD7");
            });

            modelBuilder.Entity<TeacherTeam>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.TeamId })
                    .HasName("PK__Teacher___6CD1F71D88544F78");

                entity.ToTable("Teacher_Team");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherTeams)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teacher_T__Teach__5441852A");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeacherTeams)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teacher_T__TeamI__5535A963");
            });

            modelBuilder.Entity<TeacherTopic>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.TopicId })
                    .HasName("PK__Teacher___2DD0B99148B5441F");

                entity.ToTable("Teacher_Topic");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherTopics)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teacher_T__Teach__60A75C0F");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TeacherTopics)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Teacher_T__Topic__619B8048");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TeamTopic>(entity =>
            {
                entity.HasKey(e => new { e.TeamId, e.TopicId })
                    .HasName("PK__Team_Top__D218076C801C451C");

                entity.ToTable("Team_Topic");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamTopics)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_Topi__TeamI__6477ECF3");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.TeamTopics)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Team_Topi__Topic__656C112C");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Topic__CourseId__5DCAEF64");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
