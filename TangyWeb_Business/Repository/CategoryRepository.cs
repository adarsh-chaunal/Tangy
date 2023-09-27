using System;
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
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
            if(objDTO.Name != null) 
            { 
                var category = _mapper.Map<CategoryDTO, Category>(objDTO);
                category.CreatedDate = DateTime.Now;
                var addCategory = _db.Categories.Add(category);
                await _db.SaveChangesAsync();
                return _mapper.Map<Category, CategoryDTO>(addCategory.Entity);
            }
            return objDTO;
            
        }
        public async Task<int> Delete(int id)
        {
            var obj = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (obj != null)
            {
                _db.Categories.Remove(obj);
                return await _db.SaveChangesAsync();
            }
            return 0;

        }
        public async Task<CategoryDTO>  Get(int id)
        {
            var obj = await _db.Categories.FirstOrDefaultAsync(c=>c.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Category,CategoryDTO>(obj);
            }
            return new CategoryDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
        }

        public async Task<CategoryDTO> Update(CategoryDTO objDTO)
        {
            var obj = await _db.Categories.FirstOrDefaultAsync(u=>u.Id == objDTO.Id);
            if (obj != null)
            {  
                obj.Name = objDTO.Name;
                _db.Categories.Update(obj);
                await _db.SaveChangesAsync();
                return _mapper.Map<Category,CategoryDTO>(obj);
            }
            return objDTO;
        }
    }
}
