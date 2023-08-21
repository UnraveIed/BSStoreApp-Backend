using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;

        public RepositoryManager(RepositoryContext context, ICategoryRepository categoryRepository, IBookRepository bookRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
            _bookRepository = bookRepository;
        }

        public IBookRepository Book => _bookRepository;

        public ICategoryRepository Category => _categoryRepository;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        #region Old Codes
        // Old codes
        //private readonly Lazy<IBookRepository> _bookRepository;
        //private readonly Lazy<ICategoryRepository> _categoryRepository;

        //public RepositoryManager(RepositoryContext context)
        //{
        //    _context = context;
        //    _bookRepository = new Lazy<IBookRepository>(()=> new BookRepository(context));
        //    _categoryRepository = new Lazy<ICategoryRepository>(()=> new CategoryRepository(context));
        //}

        //public IBookRepository Book => _bookRepository.Value;

        //public ICategoryRepository Category => _categoryRepository.Value;

        //public async Task SaveAsync()
        //{
        //    await _context.SaveChangesAsync();
        //}
        #endregion
    }
}
