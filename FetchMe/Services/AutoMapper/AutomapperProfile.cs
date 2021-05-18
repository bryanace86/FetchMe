using AutoMapper;
using FetchMe.Data.ValueResolvers;
using FetchMe.Models;
using FetchMe.Models.BlogModels;
using FetchMe.Models.CameraBodyModels;
using FetchMe.Models.ExposureTimeModels;
using FetchMe.Models.FocalLengthModels;
using FetchMe.Models.FStopModels;
using FetchMe.Models.GalleryModels;
using FetchMe.Models.GigTagModels;
using FetchMe.Models.ISOModels;
using FetchMe.Models.LensModels;
using FetchMe.Models.LightSourceModels;
using FetchMe.Models.LocationModels;
using FetchMe.Models.PhotographerLocationModels;
using FetchMe.Models.PhotographerModels;
using FetchMe.Models.PhotographModels;
using FetchMe.Models.PhotographTagModels;
using FetchMe.Models.PostModels;
using FetchMe.Models.SupportModels;
using FetchMe.Models.TagModels;
using FetchMe.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Bcpg;
using System;
using System.Linq;
using System.Security.Claims;

namespace FetchMe.Services.AutoMapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {

            //Photographers
            CreateMap<Photographer, PhotographerDto>().ReverseMap();
            CreateMap<Photographer, PhotographerCreateDto>().ReverseMap();
            CreateMap<PhotographerUpdateDto, Photographer>()
                .ForMember(p => p.OwnerID, opt => opt.Ignore());
            CreateMap<PhotographerDto, PhotographerUpdateDto>();
            CreateMap<Photographer, PhotographerDetailsDto>();

            CreateMap<Photographer, PhotographerIndexViewModel>()
                .ForMember(m => m.IsLiked, opt => opt.MapFrom(x => x.PhotographerLikes.Any()));

            //Photographers Locations
            CreateMap<Location, PhotographersServiceableLocationCreateIndexItem>()
                .ForMember(m => m.PhotographerHas, opt => opt.MapFrom(x => x.Photographers.Any()))
                .ReverseMap();

            //Photographs
            CreateMap<Photograph, PhotographDto>().ReverseMap();
            CreateMap<Photograph, PhotographCreateDto>().ReverseMap();
            CreateMap<PhotographUpdateDto, Photograph>().ReverseMap()
                .ForMember(p => p.ImageUrl, opt => opt.Ignore());
            CreateMap<PhotographDto, PhotographUpdateDto>()
                .ReverseMap();

            //UserProfileImage
            CreateMap<UserProfileImage, UserProfileImageDto>().ReverseMap();

            //ApplicationUser Mappings
            CreateMap<ApplicationUser, ApplicationUserDetailsVm>();
            //.ForMember(m => m.Galleries, opt => opt.MapFrom(x => x.Galleries));

            CreateMap<ApplicationUser, ApplicationUserCompleteness>()
                .ForMember(m => m.HasSlug, opt => opt.MapFrom(x => !String.IsNullOrWhiteSpace(x.Slug)))
                .ForMember(m => m.HasPhotographer, opt => opt.MapFrom(x => x.Photographer != null))
                .ForMember(m => m.HasImages, opt => opt.MapFrom(x => x.Photographer.Photographs.Any()))
                ;

            CreateMap<Photograph, PhotographIndexViewModel>()
                .ForMember(m => m.PhotographersName, opt => opt.MapFrom(x=>x.Photographer.DisplayName))
                .ForMember(m => m.PhotographersSlug, opt => opt.MapFrom(x => x.Photographer.Slug))
                .ForMember(m => m.PhotographTagIds, opt => opt.MapFrom(x => x.PhotographTags.Select(y=>y.PhotographTagId)))
                .ForMember(m => m.FormattedAddress, opt => opt.MapFrom(x => x.Location.FormattedAddress))
                .ForMember(m => m.IsLikedByUser, opt => opt.MapFrom(x => x.PhotographLikes.Any()))
                //.ForMember(m => m.IsOwner, opt => opt.MapFrom<IdentityResolver>()) //opt.MapFrom<IdentityResolver>()
                ;

            //Galleries
            CreateMap<GalleryImage, GalleryImageVM>().ReverseMap();
            CreateMap<Gallery, UpsertImageInGalleryVM>()
                .ForMember(m => m.IsProfessional, opt => opt.MapFrom(x => (x.PhotographerId != null)))
                .ForMember(m => m.IsInGallery, opt => opt.MapFrom(x => true))
                ;
            CreateMap<GalleryIndexItemVM, Gallery>().ReverseMap()
                .ForMember(m => m.Thumbnail, opt => opt.MapFrom(x=>x.GalleryImages.FirstOrDefault().Photograph.ImageUrl));

            //Tags
            CreateMap<Select2Tag, FStop>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, ExposureTime>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, ISO>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, FocalLength>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, CameraBody>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, Lens>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, LightSource>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, PhotographTag>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, PhotographTags>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, GigTag>().ReverseMap()
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.Id));

            CreateMap<Select2Tag, Photographer>().ReverseMap()
                .ForMember(m => m.Id, opt => opt.MapFrom(x => x.DisplayName))
                .ForMember(m => m.Text, opt => opt.MapFrom(x => x.DisplayName));

            /*
             * Support
             */
            CreateMap<SupportTicket, SupportTicketDetails>().ReverseMap();
            CreateMap<SupportTicket, SupportTicketCreate>().ReverseMap();
            CreateMap<SupportTicketStatus, SupportTicketStatusVM>().ReverseMap();
            CreateMap<SupportTicketResponse, SupportTicketResponseCreate>().ReverseMap();
            CreateMap<SupportTicketResponse, SupportTicketResponseDetails>().ReverseMap();

            /*
             * Blogs
             */
            CreateMap<BlogVM, Blog>().ReverseMap()
                .ForPath(m => m.PostIndex.Posts, opt => opt.MapFrom(x => x.Posts));
            CreateMap<BlogCreateVM, Blog>().ReverseMap();
            CreateMap<BlogUpdateVM, Blog>().ReverseMap();
            CreateMap<BlogIndexItemVM, Blog>().ReverseMap();

            /*
             * Posts
             */
            CreateMap<PostVM, Post>().ReverseMap();
            CreateMap<PostCreateVM, Post>().ReverseMap();
            CreateMap<PostUpdateVM, Post>().ReverseMap();
            CreateMap<PostIndexItemVM, Post>().ReverseMap();
        }
    }

}
