using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDto>()
            .ForMember("PictureUrl", opt
                => opt.MapFrom<CatalogItemPictureResolver, string>(c => c.PictureFileName));
        CreateMap<CatalogBrand, CatalogBrandDto>();
        CreateMap<CatalogType, CatalogTypeDto>();
        CreateMap<CatalogModel, CatalogModelDto>();
        CreateMap<CatalogSubType, CatalogSubTypeDto>();
        CreateMap<CatalogItem, CatalogModelDto>()
            .ForMember(nameof(CatalogModelDto.Model), opt
            => opt.MapFrom(x => x.CatalogModel.Model))
            .ForMember(nameof(CatalogModelDto.Id), opt
            => opt.MapFrom(x => x.CatalogModelId))
            .ForMember(nameof(CatalogModelDto.CatalogBrand), opt
            => opt.MapFrom(x => x.CatalogModel.CatalogBrand));
        CreateMap<CatalogItem, CatalogSubTypeDto>()
            .ForMember(nameof(CatalogSubTypeDto.SubType), opt
            => opt.MapFrom(x => x.CatalogSubType.SubType))
            .ForMember(nameof(CatalogSubTypeDto.Id), opt
            => opt.MapFrom(x => x.CatalogSubTypeId))
            .ForMember(nameof(CatalogSubTypeDto.CatalogType), opt
            => opt.MapFrom(x => x.CatalogSubType.CatalogType));
        CreateMap<CatalogItem, CatalogTypeDto>()
            .ForMember(nameof(CatalogTypeDto.Type), opt
            => opt.MapFrom(x => x.CatalogSubType.CatalogType.Type))
            .ForMember(nameof(CatalogTypeDto.Id), opt
            => opt.MapFrom(x => x.CatalogSubType.CatalogTypeId));
        CreateMap<CatalogItem, CatalogBrandDto>()
            .ForMember(nameof(CatalogBrandDto.Brand), opt
            => opt.MapFrom(x => x.CatalogModel.CatalogBrand.Brand))
            .ForMember(nameof(CatalogBrandDto.Id), opt
            => opt.MapFrom(x => x.CatalogModel.CatalogBrandId));
    }
}