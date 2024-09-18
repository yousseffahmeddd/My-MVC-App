using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected AppDbContext _context; // = new AppDbContext(); //Null

        public GenericRepository(AppDbContext context) //Ask CLR to create object from AppDbContext
        {
            //_context = new AppDbContext();
            _context = context;
        }

        public void Add(T entity)
        {
            if (entity is not null)
            {
               _context.AddAsync(entity);
                
            }
        }

        public void Delete(T entity)
        {
            if (entity is not null)
            {
                _context.Remove(entity);
               

            }
            
        }

        public async Task<T> Get(int Id)
        {
            var result = await _context.Set<T>().FindAsync(Id);
            return result;

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            //var department = _context.Departments.ToList();

            if (typeof(T) == typeof(Employee))
            {
                return(IEnumerable<T>) await _context.Employees.Include(E=>E.Department).ToListAsync();
            }
            else
            {
                return await _context.Set<T>().ToListAsync();
            }

        }

        public void Update(T entity)
        {
            _context.Update(entity);
            

        }
    }
}
