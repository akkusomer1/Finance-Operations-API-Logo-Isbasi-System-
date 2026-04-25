using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Infrastructure.Repositories
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private readonly IRepositoryAsync<ProductOrder> _repository;

        public ProductOrderRepository(IRepositoryAsync<ProductOrder> repository)
        {
            _repository = repository;
        }

        public IQueryable<ProductOrder> ProductOrders => _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id);

        public async Task DeleteAsync(ProductOrder order)
        {
            order.Status = EntityStatus.Deleted;
            await _repository.UpdateAsync(order);
            //await _repository.DeleteAsync(Order);
        }

        public async Task<ProductOrder> GetByIdAsync(int orderId)
        {
            return await _repository.Entities.Where(p => p.Id == orderId & p.Status != EntityStatus.Deleted).FirstOrDefaultAsync();
        }

        public async Task<List<ProductOrder>> GetListAsync()
        {
            return await _repository.Entities.Where(p => p.Status != EntityStatus.Deleted).OrderBy(o => o.DisplayOrder).ThenBy(o => o.Id).ToListAsync();
        }

        public async Task<int> InsertAsync(ProductOrder order)
        {
            await _repository.AddAsync(order);
            return order.Id;
        }

        public async Task UpdateAsync(ProductOrder order)
        {
            await _repository.UpdateAsync(order);
        }
    }
}
