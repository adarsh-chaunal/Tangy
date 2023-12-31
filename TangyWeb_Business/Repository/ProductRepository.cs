﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;
using AutoMapper;
using Tangy_Business.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Tangy_Business.Repository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            if (objDTO.Name != null) //// check if .Name is necessery here
            {
                var product = _mapper.Map<ProductDTO, Product>(objDTO);
                var addProduct = _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(addProduct.Entity);
            }
            return objDTO;
                    
        }
        public async Task<int> Delete(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                _db.Products.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;

        }
        public async Task<ProductDTO> Get(int id)
        {
            var obj = await _db.Products.Include(u=>u.Category).Include(u=>u.ProductPrice).FirstOrDefaultAsync(c=>c.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Product, ProductDTO>(obj);
            }
            return new ProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(u => u.Category).Include(u => u.ProductPrice));
        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var objFromDb = await _db.Products.FirstOrDefaultAsync(u=>u.Id == objDTO.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = objDTO.Name;
                objFromDb.Description = objDTO.Description;
                objFromDb.ImageUrl = objDTO.ImageUrl;
                objFromDb.CategoryId = objDTO.CategoryId;
                objFromDb.Color = objDTO.Color;
                objFromDb.ShopFavorites = objDTO.ShopFavorites;
                objFromDb.CustomerFavorites = objDTO.CustomerFavorites; ;
                _db.Products.Update(objFromDb);
                await _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(objFromDb);
            }
            return objDTO;
        }

    }
}
