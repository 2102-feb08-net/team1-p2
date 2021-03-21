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
        public virtual DbSet<ConditionStatus> ConditionStatuses { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<LoanReview> LoanReviews { get; set; }
        public virtual DbSet<LoanStatus> LoanStatuses { get; set; }
        public virtual DbSet<LoanedBook> LoanedBooks { get; set; }
        public virtual DbSet<OwnedBook> OwnedBooks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

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

                entity.Property(e => e.Country)
                    .HasMaxLength(1000)
                    .HasColumnName("country");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasColumnName("state");

                entity.Property(e => e.Zipcode).HasColumnName("zipcode");
            });

            modelBuilder.Entity<AvailabilityStatus>(entity =>
            {
                entity.ToTable("Availability_Status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("statusName");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("author");

                entity.Property(e => e.Isbn).HasColumnName("isbn");

                entity.Property(e => e.Thumbnail)
                    .HasMaxLength(1000)
                    .HasColumnName("thumbnail");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<ConditionStatus>(entity =>
            {
                entity.ToTable("Condition_Status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("statusName");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("genreName");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Genres)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Genre__bookId__6D2D2E85");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("addressId");

                entity.Property(e => e.BorrowerId).HasColumnName("borrowerId");

                entity.Property(e => e.DropoffDate).HasColumnName("dropoffDate");

                entity.Property(e => e.LenderId).HasColumnName("lenderId");

                entity.Property(e => e.LoanStatusId).HasColumnName("loanStatusId");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("ntext")
                    .HasColumnName("message");

                entity.Property(e => e.ReturnedDate).HasColumnName("returnedDate");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loans__addressId__7E57BA87");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.LoanBorrowers)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loans__borrowerI__7C6F7215");

                entity.HasOne(d => d.Lender)
                    .WithMany(p => p.LoanLenders)
                    .HasForeignKey(d => d.LenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loans__lenderId__7B7B4DDC");

                entity.HasOne(d => d.LoanStatus)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.LoanStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loans__loanStatu__7D63964E");
            });

            modelBuilder.Entity<LoanReview>(entity =>
            {
                entity.ToTable("Loan_Review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LoanId).HasColumnName("loanId");

                entity.Property(e => e.ReviewerId).HasColumnName("reviewerId");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.LoanReviews)
                    .HasForeignKey(d => d.LoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loan_Revi__loanI__09C96D33");

                entity.HasOne(d => d.Reviewer)
                    .WithMany(p => p.LoanReviews)
                    .HasForeignKey(d => d.ReviewerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loan_Revi__revie__08D548FA");
            });

            modelBuilder.Entity<LoanStatus>(entity =>
            {
                entity.ToTable("Loan_Status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("statusName");
            });

            modelBuilder.Entity<LoanedBook>(entity =>
            {
                entity.ToTable("Loaned_Books");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LoanId).HasColumnName("loanId");

                entity.Property(e => e.OwnedBookid).HasColumnName("ownedBookid");

                entity.HasOne(d => d.Loan)
                    .WithMany(p => p.LoanedBooks)
                    .HasForeignKey(d => d.LoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loaned_Bo__loanI__02284B6B");

                entity.HasOne(d => d.OwnedBook)
                    .WithMany(p => p.LoanedBooks)
                    .HasForeignKey(d => d.OwnedBookid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Loaned_Bo__owned__01342732");
            });

            modelBuilder.Entity<OwnedBook>(entity =>
            {
                entity.ToTable("Owned_Books");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AvailabilityStatusId).HasColumnName("availabilityStatusId");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.ConditionId).HasColumnName("conditionId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.AvailabilityStatus)
                    .WithMany(p => p.OwnedBooks)
                    .HasForeignKey(d => d.AvailabilityStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Owned_Boo__avail__789EE131");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OwnedBooks)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Owned_Boo__bookI__76B698BF");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.OwnedBooks)
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Owned_Boo__condi__77AABCF8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OwnedBooks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Owned_Boo__userI__75C27486");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164C06B4C68")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__Users__F3DBC57269D82558")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("addressId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__addressId__6774552F");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("Wishlist");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookId).HasColumnName("bookId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Wishlist__bookId__05F8DC4F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Wishlist__userId__0504B816");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
