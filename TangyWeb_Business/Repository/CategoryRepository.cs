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
        public CategoryDTO Create(CategoryDTO objDTO)
        {
            var category = _mapper.Map<CategoryDTO, Category>(objDTO);
            category.CreatedDate = DateTime.Now;
            var addCategory = _db.Add(category);
            _db.SaveChanges();
            return _mapper.Map<Category, CategoryDTO>(addCategory.Entity);
        }
        int ICategoryRepository.Delete(int id)
        {
            var obj = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (obj != null)
            {
                _db.Categories.Remove(obj);
                return _db.SaveChanges();
            }
            return 0;

        }
        CategoryDTO ICategoryRepository.Get(int id)
        {
            var obj = _db.Categories.FirstOrDefault(c=>c.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Category,CategoryDTO>(obj);
            }
            return new CategoryDTO();
        }

        IEnumerable<CategoryDTO> ICategoryRepository.GetAll()
        {
            return _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_db.Categories);
        }

        CategoryDTO ICategoryRepository.Update(CategoryDTO objDTO)
        {
            var obj = _db.Categories.FirstOrDefault(u=>u.Id == objDTO.Id);
            if (obj != null)
            {  
                obj.Name = objDTO.Name;
                _db.Categories.Update(obj);
                return _mapper.Map<Category,CategoryDTO>(obj);
            }
            return objDTO;
        }
    }
}
