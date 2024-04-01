using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
       
        

            private ApplicationDbContext _db;

            //That way, whatever DB context we get here, we will pass to the repository and the error goes away.
            public ProductRepository(ApplicationDbContext db) : base(db)
            {

                _db = db;

            }

            public void update(Product obj)
            {
                _db.Products.Update(obj);
            }

       
    }


    }

