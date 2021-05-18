using FetchMe.Models;
using FetchMe.Models.BidModels;
using FetchMe.Models.BlogModels;
using FetchMe.Models.CameraBodyModels;
using FetchMe.Models.DateTimeSpanModels;
using FetchMe.Models.ExposureTimeModels;
using FetchMe.Models.FocalLengthModels;
using FetchMe.Models.FStopModels;
using FetchMe.Models.GalleryModels;
using FetchMe.Models.GigDateTimeModels;
using FetchMe.Models.GigDateTimeTypeModels;
using FetchMe.Models.GigLocationModels;
using FetchMe.Models.GigLocationTypeModels;
using FetchMe.Models.GigModels;
using FetchMe.Models.GigTagModels;
using FetchMe.Models.ISOModels;
using FetchMe.Models.LensModels;
using FetchMe.Models.LightSourceModels;
using FetchMe.Models.LikeModels;
using FetchMe.Models.LocationModels;
using FetchMe.Models.MessageModels;
using FetchMe.Models.PhotographerBadgeModels;
using FetchMe.Models.PhotographerLocationModels;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using FetchMe.Models.PhotographTagModels;
using FetchMe.Models.PostModels;
using FetchMe.Models.SupportModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using System;

namespace FetchMe.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Photographer> Photographers { get; set; }
        public DbSet<PhotographerLocation> PhotographerLocations { get; set; }
        public DbSet<PhotographerLike> PhotographerLikes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<GigDateTime> GigDateTimes { get; set; }
        public DbSet<GigDateTimeType> GigDateTimeTypes { get; set; }
        public DbSet<GigLocationType> GigLocationTypes { get; set; }
        public DbSet<GigLocation> GigLocations { get; set; }
        public DbSet<DateTimeSpan> DateTimeSpans { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Photograph> Photographs { get; set; }
        public DbSet<PhotographTag> PhotographTag { get; set; }
        public DbSet<PhotographTags> PhotographTags { get; set; }
        public DbSet<PhotographTagsPhotograph> PhotographTagsPhotographs { get; set; }
        public DbSet<ISO> ISOs { get; set; }
        public DbSet<CameraBody> CameraBodies { get; set; }
        public DbSet<FStop> FStops { get; set; }
        public DbSet<Lens> Lenses { get; set; }
        public DbSet<LightSource> LightSources { get; set; }
        public DbSet<ExposureTime> ExposureTimes { get; set; }
        public DbSet<FocalLength> FocalLengths { get; set; }
        public DbSet<PhotographLike> PhotographLikes { get; set; }
        public DbSet<Gig> Gig { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<PhotographerBadge> PhotographerBadges { get; set; }
        public DbSet<GigTag> GigTags { get; set; }
        public DbSet<GigTagGig> GigTagGigs { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<GigStatus> GigStatuses { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryImage> GalleryImages { get; set; }
        public DbSet<UserProfileImage> UserProfileImages { get; set; }
        public DbSet<UserBannerImage> UserBannerImages { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<SupportTicketResponse> SupportTicketResponses { get; set; }
        public DbSet<SupportTicketStatus> SupportTicketStatus { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //Photographer
            builder.Entity<Photographer>()
                .HasIndex(s => s.Slug)
                .IsUnique();

            //Photographer Banner Image
            builder.Entity<Photographer>()
                .HasOne(x => x.BannerImage)
                .WithMany()
                .HasForeignKey(x => x.BannerImageId);


            //Photographer Logo Image
            builder.Entity<Photographer>()
                .HasOne(a => a.LogoImage)
                .WithMany()
                .HasForeignKey(x => x.LogoImageId);

            //Photographer Tag

            //Photographer Likes
            builder.Entity<PhotographerLike>()
                .HasOne(x => x.Photographer)
                .WithMany(y => y.PhotographerLikes)
                .HasForeignKey(x => x.PhotographerId);

            //Photographer Profile Picture
            builder.Entity<Photograph>()
                .HasOne(c => c.Photographer)
                .WithMany(p => p.Photographs)
                .HasForeignKey(c => c.PhotographerId);

            //Photographer Serviceable City
            builder.Entity<PhotographerLocation>()
                .HasKey(a => new { a.PhotographerId, a.LocationId });

            builder.Entity<PhotographerLocation>()
                .HasOne(a => a.Photographer)
                .WithMany(b => b.Locations)
                .HasForeignKey(a => a.PhotographerId);

            builder.Entity<PhotographerLocation>()
                .HasOne(a => a.Location)
                .WithMany(b => b.Photographers)
                .HasForeignKey(a => a.LocationId);

            //Photograph Photographer
            builder.Entity<Photograph>()
                .HasOne(p => p.Photographer)
                .WithMany(b => b.Photographs)
                .HasForeignKey(p => p.PhotographerId);

            //Photograph Location
            builder.Entity<Photograph>()
                .HasOne(p => p.Location)
                .WithMany(b => b.Photographs)
                .HasForeignKey(p => p.LocationId);

            //Photograph Tag
            builder.Entity<PhotographTagsPhotograph>()
                .HasKey(a => new { a.PhotographId, a.PhotographTagId });

            builder.Entity<PhotographTagsPhotograph>()
                .HasOne(a => a.Photograph)
                .WithMany(b => b.PhotographTags)
                .HasForeignKey(a => a.PhotographId);

            builder.Entity<PhotographTagsPhotograph>()
                .HasOne(a => a.PhotographTag)
                .WithMany(b => b.TagPhotographs)
                .HasForeignKey(a => a.PhotographTagId);

            //camera body
            builder.Entity<Photograph>()
                .HasOne(c => c.CameraBody)
                .WithMany(p => p.Photographs)
                .HasForeignKey(c => c.CameraBodyValue);

            builder.Entity<Photograph>()
               .HasOne(c => c.ExposureTime)
               .WithMany(p => p.Photographs)
               .HasForeignKey(c => c.ExposureTimeValue);

            builder.Entity<Photograph>()
               .HasOne(c => c.FStop)
               .WithMany(p => p.Photographs)
               .HasForeignKey(c => c.FStopValue);

            builder.Entity<Photograph>()
               .HasOne(c => c.ISO)
               .WithMany(p => p.Photographs)
               .HasForeignKey(c => c.ISOValue);

            builder.Entity<Photograph>()
               .HasOne(c => c.LightSource)
               .WithMany(p => p.Photographs)
               .HasForeignKey(c => c.LightSourceValue);

            builder.Entity<Photograph>()
                .HasOne(c => c.Lens)
                .WithMany(p => p.Photographs)
                .HasForeignKey(c => c.LensValue);

            builder.Entity<Photograph>()
                .HasOne(c => c.FocalLength)
                .WithMany(p => p.Photographs)
                .HasForeignKey(c => c.FocalLengthValue);

            //photograph likes
            builder.Entity<PhotographLike>()
                .HasOne(x => x.Photograph)
                .WithMany(y => y.PhotographLikes)
                .HasForeignKey(x => x.PhotographId);


            //Photographer Stripe Id
            /*
            builder.Entity<StripeCustomer>()
                .HasKey(a => new { a.UserId, a.CustomerId });


            builder.Entity<UserSubscription>()
                .HasKey(k => new { k.UserId });

            //membership relationships
            builder.Entity<MembershipPlan>()
                .HasOne(x => x.Level)
                .WithMany(y => y.Plans)
                .HasForeignKey(x => x.MembershipLevelId);
            */

            /*
            //Support Tickets
            builder.Entity<SupportTicket>()
                .HasOne(x => x.Status)
                .WithMany(y => y.Tickets)
                .HasForeignKey(x => x.SupportTicketStatusId);


            builder.Entity<SupportTicketResponse>()
                .HasOne(x => x.Ticket)
                .WithMany(y => y.Resonses)
                .HasForeignKey(x => x.SupportTicketId);
            */

            //Photographer badges
            //Photograph Tag
            builder.Entity<PhotographerBadge>()
                .HasKey(a => new { a.PhotographerId, a.BadgeId });

            builder.Entity<PhotographerBadge>()
                .HasOne(a => a.Photographer)
                .WithMany(b => b.PhotographerBadges)
                .HasForeignKey(a => a.PhotographerId);

            builder.Entity<PhotographerBadge>()
                .HasOne(a => a.Badge)
                .WithMany(b => b.PhotographerBadges)
                .HasForeignKey(a => a.BadgeId);


            //Gigs

            builder.Entity<GigLocation>()
                .HasKey(a => new { a.GigId, a.LocationId });

            builder.Entity<GigLocation>()
                .HasOne(a => a.Gig)
                .WithMany(b => b.Locations)
                .HasForeignKey(a => a.GigId);

            builder.Entity<GigLocation>()
                .HasOne(a => a.Location)
                .WithMany(b => b.Gigs)
                .HasForeignKey(a => a.LocationId);

            builder.Entity<GigDateTime>()
                .HasKey(a => new { a.GigId, a.DateTimeSpanId });

            builder.Entity<GigDateTime>()
                .HasOne(a => a.Gig)
                .WithMany(b => b.GigDateTimes)
                .HasForeignKey(a => a.GigId);

            builder.Entity<GigDateTime>()
                .HasOne(a => a.DateTimeSpan)
                .WithMany(b => b.GigDateTimes)
                .HasForeignKey(a => a.DateTimeSpanId);

            /*
             * Gig Tags
             */
            //Photograph Tag
            builder.Entity<GigTagGig>()
                .HasKey(a => new { a.GigId, a.TagId });

            builder.Entity<GigTagGig>()
                .HasOne(a => a.Gig)
                .WithMany(b => b.Tags)
                .HasForeignKey(a => a.GigId);

            builder.Entity<GigTagGig>()
                .HasOne(a => a.Tag)
                .WithMany(b => b.Gigs)
                .HasForeignKey(a => a.TagId);

            /*
             * Gig Location types
             */
            builder.Entity<Gig>()
               .HasOne(a => a.LocationType)
               .WithMany(b => b.Gigs)
               .HasForeignKey(a => a.LocationTypeId);


            /*
             * Gig DateTime types
             */
            builder.Entity<Gig>()
              .HasOne(a => a.GigDateTimeType)
              .WithMany(b => b.Gigs)
              .HasForeignKey(a => a.GigDateTimeTypeId);

            //Bids
            //Bid Gig
            builder.Entity<Bid>()
              .HasOne(a => a.Gig)
              .WithMany(b => b.Bids)
              .HasForeignKey(a => a.GigId);

            //Bid Photographer
            builder.Entity<Bid>()
              .HasOne(a => a.Photographer)
              .WithMany(b => b.Bids)
              .HasForeignKey(a => a.PhotographerId);

            //Bid Message

            builder.Entity<Bid>()
              .HasOne(a => a.Message)
              .WithOne(b => b.Bid);

            //Gig Status
            builder.Entity<Gig>()
                .HasOne(a => a.GigStatus)
                .WithMany(b => b.Gigs)
                .HasForeignKey(a => a.GigStatusId);


            //Galleries
            builder.Entity<GalleryImage>()
                .HasKey(a => new { a.GalleryId, a.PhotographId });

            builder.Entity<GalleryImage>()
                .HasOne(a => a.Gallery)
                .WithMany(b => b.GalleryImages)
                .HasForeignKey(a => a.GalleryId);

            builder.Entity<GalleryImage>()
                .HasOne(a => a.Photograph)
                .WithMany(b => b.Galleries)
                .HasForeignKey(a => a.PhotographId);

            builder.Entity<Photographer>()
                .HasMany(a => a.Galleries)
                .WithOne(b => b.Photographer);




            //User Data
            builder.Entity<Gallery>()
                .HasOne(a => a.ApplicationUser)
                .WithMany(b => b.Galleries)
                .HasForeignKey(a => a.OwnerID);

            builder.Entity<UserBannerImage>()
                .HasOne(a => a.ApplicationUser)
                .WithOne(b => b.UserBannerImage)
                .HasForeignKey<UserBannerImage>(a => a.UserId);

            builder.Entity<UserProfileImage>()
                .HasOne(a => a.ApplicationUser)
                .WithOne(b => b.UserProfileImage)
                .HasForeignKey<UserProfileImage>(a => a.UserId);

            builder.Entity<Photographer>()
                .HasOne(x => x.User)
                .WithOne(y => y.Photographer)
                .HasForeignKey<Photographer>(x => x.OwnerID);

            /*
             * Support
             */
            //Support Tickets
            builder.Entity<SupportTicket>()
                .HasOne(x => x.Status)
                .WithMany(y => y.Tickets)
                .HasForeignKey(x => x.SupportTicketStatusId);


            builder.Entity<SupportTicketResponse>()
                .HasOne(x => x.Ticket)
                .WithMany(y => y.Responses)
                .HasForeignKey(x => x.SupportTicketId);

            /*
             * Blogs
             */
            builder.Entity<Post>()
                .HasOne(x => x.Blog)
                .WithMany(y => y.Posts)
                .HasForeignKey(x => x.BlogId);

            builder.Entity<Blog>()
                .HasOne(x => x.User)
                .WithMany(y => y.Blogs)
                .HasForeignKey(x => x.OwnerId);

            builder.Entity<Blog>()
                .HasOne(x => x.Thumbnail)
                .WithMany(x => x.Blogs)
                .HasForeignKey(x => x.ThumbnailId);

            builder.Entity<Post>()
                .HasOne(x => x.Thumbnail)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.ThumbnailId);

            /*
             * Seed data
             */

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole (){Id= "f486ace3-7532-4df0-867a-128f07c55713", Name="Photographer" , NormalizedName="PHOTOGRAPHER", ConcurrencyStamp = "sajkdhjkhasfu"  },
                new IdentityRole (){ Id = "4e52af71-092c-4f12-a2e5-feb18b274606", Name = "Admin" , NormalizedName= "ADMIN", ConcurrencyStamp = "sajdfejkhasfu"  }
                );
            
            builder.Entity<SupportTicketStatus>().HasData(
                new SupportTicketStatus() { Id = Guid.Parse("5be86e69-0ac0-4322-ae59-d9e4eaabfc9b"), Status = "New" },
                new SupportTicketStatus() { Id = Guid.Parse("eea30f4e-1d22-4aa0-94c2-6f2be2633bd0"), Status = "Pending User Response" },
                new SupportTicketStatus() { Id = Guid.Parse("d4919e2f-d00a-45e1-bff3-78e34ac98ea3"), Status = "Pending Support Response" },
                new SupportTicketStatus() { Id = Guid.Parse("8c108fb0-e003-4028-a9dc-48c5ee46936b"), Status = "Closed" }
                );
            
            
        }

    }
}
