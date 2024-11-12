using AutoMapper;
using SanaCommerce.Application.DTOs.Create;
using SanaCommerce.Application.DTOs.Read;
using SanaCommerce.Application.DTOs.Update;
using SanaCommerce.Domain.Entities;

namespace SanaCommerce.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Category Mappings
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();
        CreateMap<Category, CategoryReadDto>();

        // Product Mappings
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<Product, ProductReadDto>()
            .ForMember(dest => dest.Categories,
                opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)));

        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>();
        CreateMap<Order, OrderReadDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => 
                src.OrderProducts != null 
                    ? src.OrderProducts.Where(op => op.Product != null)
                        .Select(op => op.Product.ProductCode).ToList() 
                    : new List<string>()))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => 
                src.Customer != null ? src.Customer.Id : null));

        CreateMap<CustomerCreateDto, Customer>();
        CreateMap<CustomerUpdateDto, Customer>();
        CreateMap<Customer, CustomerReadDto>();
    }
}