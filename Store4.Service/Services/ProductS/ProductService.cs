using AutoMapper;
using Store4.core.Entities;
using Store4.Core.Dtos.Products;
using Store4.Core.Helper;
using Store4.Core.Mapping.Products;
using Store4.Core.Repositories.Contract;
using Store4.Core.specifications;
using Store4.Core.specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Service.Services.ProductS
{
	public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<PaginationResponse<ProductDto>> GetAllProducts(ProductSpecParams ProductSpec)
		{
			var spec = new ProductSpecifications(ProductSpec);
			var prodect = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
			var mappedproduct =  _mapper.Map<IEnumerable<ProductDto>>(prodect);
			var countspec = new ProductWithCountSpecifications(ProductSpec);
			var count = await _unitOfWork.Repository<Product, int>().GetCountWithAsync(countspec);

			return new PaginationResponse<ProductDto>(ProductSpec.PageSize, ProductSpec.pageindex, count, mappedproduct);

		}
		public async Task<ProductDto> GetProductById(int id)
		{
			var spec = new ProductSpecifications(id);
			return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec));
			//var product = await _unitOfWork.Repository<Product, int>().GetAsync(id);
			//var mappedproduct = _mapper.Map<ProductDto>(product);
			//return mappedproduct;
		}
		public async Task<IEnumerable<TypeBrandDto>> GetAllBrands()
		{
			return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
		}


		public async Task<IEnumerable<TypeBrandDto>> GetAllTypes()
		{
			return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductType, int>().GetAllAsync());

		}
	}
}
