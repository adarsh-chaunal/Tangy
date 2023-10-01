using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;

namespace Tangy_Business.Repository;
public class ProductPriceRepository : IProductPriceRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    public ProductPriceRepository(ApplicationDbContext db, IMapper mapper){
        _db = db;
        _mapper = mapper;
    }

    public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
    {
        if(objDTO != null)
        {
            ProductPrice ProdPriceObj = _mapper.Map<ProductPriceDTO, ProductPrice>(objDTO);
            var obj = _db.ProductsPrice.Add(ProdPriceObj);
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductPrice, ProductPriceDTO>(obj.Entity);
        }
        return objDTO;
    }

    public async Task<int> Delete(int id)
    {   
        var ProdPriceObj = await _db.ProductsPrice.FirstOrDefaultAsync(u => u.Id == id);
        if(ProdPriceObj != null)
        {
            _db.ProductsPrice.Remove(ProdPriceObj);
            return await _db.SaveChangesAsync();
        }
        return 0;
    }

    public async Task<ProductPriceDTO> Get(int id)
    {
        var ProdPriceObj = await _db.ProductsPrice.FirstOrDefaultAsync(u => u.Id == id);
        if (ProdPriceObj != null)
        {
            return _mapper.Map<ProductPrice, ProductPriceDTO>(ProdPriceObj);
        }
        return new ProductPriceDTO();
    }

    public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
    {
        if(id!=null || id>0)
        {
            return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductsPrice.Where(u => u.ProductId == id));
        }
        else
        {
            return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductsPrice);
        }
    }

    public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
    {
        ProductPrice ProdPriceObj = await _db.ProductsPrice.FirstOrDefaultAsync(u => u.Id == objDTO.Id);
        if (ProdPriceObj != null)
        {
            ProdPriceObj.Size = objDTO.Size;
            ProdPriceObj.Price = objDTO.Price;
            _db.ProductsPrice.Update(ProdPriceObj);
            await _db.SaveChangesAsync();
            return _mapper.Map<ProductPrice, ProductPriceDTO>(ProdPriceObj);
        }
        return objDTO;
    }
}
