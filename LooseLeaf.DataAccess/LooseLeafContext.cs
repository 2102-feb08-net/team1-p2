using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LooseLeaf.DataAccess
{
    public partial class LooseLeafContext : DbContext
    {
        public LooseLeafContext()
        {
        }

        public LooseLeafContext(DbContextOptions<LooseLeafContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<LoanStatus> LoanStatuses { get; set; }
        public virtual DbSet<LoanedBook> LoanedBooks { get; set; }
        public virtual DbSet<OwnedBook> OwnedBooks { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("address1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(1000)
                    .HasColumnName("address2");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("city");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("state");

                entity.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("zipcode");
            });

            modelBuilder.Entity<AvailabilityStatus>(entity =>
            {
                entity.ToTable("Availability_Status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvailabilityStatus1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("availabilityStatus");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("author");

                entity.Property(e => e.Genreid).HasColumnName("genreid");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("isbn");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Genreid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Books__genreid__1AD3FDA4");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Genre1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("genre");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Dropoffdate)
                    .HasColumnType("datetime")
                    .HasColumnName("dropoffdate");

                entity.Property(e => e.IsRecommended).HasColumnName("isRecommended");

                entity.Property(e => e.Ispublic).HasColumnName("ispublic");

                entity.Property(e => e.LoanStatusId).HasColumnName("loanStatusId");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("message");

                entity.Property(e => e.OwnedBookid).HasColumnName("owned_bookid");

                entity.Property(e => e.Returneddate)
                    .HasColumnType("datetime")
                    .HasColumnName("returneddate");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.LoanStatus)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.LoanStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loans__loanStatu__282DF8C2");

                entity.HasOne(d => d.OwnedBook)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.OwnedBookid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loans__owned_boo__2739D489");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loans__userid__2645B050");
            });

            modelBuilder.Entity<LoanStatus>(entity =>
            {
                entity.ToTable("Loan_Status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LoanStatus1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("loanStatus");
            });

            modelBuilder.Entity<LoanedBook>(entity =>
            {
                entity.ToTable("Loaned_Books");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Loanid).HasColumnName("loanid");

                entity.Property(e => e.OwnedBookid).HasColumnName("owned_bookid");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.LoanedBooks)
                    .HasForeignKey(d => d.Loanid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loaned_Bo__loani__2BFE89A6");

                entity.HasOne(d => d.OwnedBook)
                    .WithMany(p => p.LoanedBooks)
                    .HasForeignKey(d => d.OwnedBookid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loaned_Bo__owned__2B0A656D");
            });

            modelBuilder.Entity<OwnedBook>(entity =>
            {
                entity.ToTable("Owned_Books");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvailabilityStatusId).HasColumnName("availabilityStatusId");

                entity.Property(e => e.Bookid).HasColumnName("bookid");

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("condition");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.AvailabilityStatus)
                    .WithMany(p => p.OwnedBooks)
                    .HasForeignKey(d => d.AvailabilityStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Owned_Boo__avail__236943A5");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OwnedBooks)
                    .HasForeignKey(d => d.Bookid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Owned_Boo__booki__22751F6C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OwnedBooks)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Owned_Boo__useri__2180FB33");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Addressid).HasColumnName("addressid");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.Property(e => e.Userpassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userpassword");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Addressid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__addressid__160F4887");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}