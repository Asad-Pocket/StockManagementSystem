﻿using AutoMapper;
using StockManagementSystem.BusinessObject;
using StockManagementSystem.Models;
using StockManagementSystem.UnitOfWorks;
using ItemBo = StockManagementSystem.BusinessObject.Item;
using ItemEo = StockManagementSystem.Models.Item;
using CategoryBo = StockManagementSystem.BusinessObject.Category;
using CategoryEo = StockManagementSystem.Models.Category;
using CompanyBo = StockManagementSystem.BusinessObject.Company;
using CompanyEo = StockManagementSystem.Models.Company;
using System.Collections.Generic;
using System.ComponentModel.Design;
using StockManagementSystem.BusinessObjects;

namespace StockManagementSystem.Service
{
    public class ItemService : IItemService
    {
        private readonly IApplicationUnitOfWorks _UnitOfWork;
        private readonly IMapper _mapper;
        public ItemService(IApplicationUnitOfWorks works, IMapper mapper)
        {
            _UnitOfWork = works;
            _mapper = mapper;
        }
        public void DeleteQuantity(List<StockOutRecord> items)
        {

            foreach (var item in items)
            {
                int id = item.ItemId;
                var itemtodelete = _UnitOfWork._items.GetById(id);
                itemtodelete.Quantity -= item.QuantityToChange;
                _UnitOfWork._items.Update(itemtodelete);
                _UnitOfWork.Save();
            }
        }
        public void Create(ItemBo item)
        {
            var obj = _mapper.Map<ItemEo>(item);
            _UnitOfWork._items.Insert(obj);
            _UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            _UnitOfWork._items.Delete(id);
            _UnitOfWork.Save();
        }

        public IEnumerable<ItemEo> GetAllItem()
        {
            var items = _UnitOfWork._items.GetAllInItem().ToList();
            return items;
        }
        public Dictionary<string, int> CalculateCategoryCounts()
        {
            var items = _UnitOfWork._items.GetAllInItem().ToList(); // Assuming you have an ItemRepository

            var categoryCounts = new Dictionary<string, int>();

            foreach (var item in items)
            {
                var categoryName = item.Category.Type;// Assuming Category is a navigation property
                if (categoryCounts.ContainsKey(categoryName))
                {
                    categoryCounts[categoryName]++;
                }
                else
                {
                    categoryCounts[categoryName] = 1;
                }
            }
            
            return categoryCounts;
        }
        public ItemBo GetById(int id)
        {
            var item = _UnitOfWork._items.GetById(id);
            var obj = _mapper.Map<ItemBo>(item);
            return obj;
        }

        public List<CategoryBo> GetCategory()
        {
            var _list  = _UnitOfWork._categories.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CategoryEo>, List<CategoryBo>>(_list);
            return mappedList;
        }

        public List<CompanyBo> GetCompany()
        {
            var _list = _UnitOfWork._companies.GetAllItem().ToList();
            var mappedList = _mapper.Map<List<CompanyEo>, List<CompanyBo>>(_list);
            return mappedList;
        }

        public void Update(ItemBo _item)
        {
            var obj = _mapper.Map<ItemEo>(_item);
            _UnitOfWork._items.Update(obj);
            _UnitOfWork.Save();
        }

        public List<ItemBo> GetItemsByCompanyId(int companyId)
        {
            List<ItemEo> items = _UnitOfWork._items.GetItemByCompany(companyId).ToList();
            List<ItemBo> _clist = _mapper.Map<List<ItemEo>, List<ItemBo>>(items);
            return _clist;
        }

        public void AddItemQuantity(int selectedItemId, int stockNewQuantity)
        {
            ItemEo item = _UnitOfWork._items.GetById(selectedItemId);
            item.Quantity = item.Quantity + stockNewQuantity;
            _UnitOfWork._items.Update(item);
            _UnitOfWork.Save();
        }
        public void DeleteItemQuantity(int selectedItemId, int stockInQuantity)
        {
            ItemEo item = _UnitOfWork._items.GetById(selectedItemId);
            item.Quantity = item.Quantity - stockInQuantity;
            _UnitOfWork._items.Update(item);
            _UnitOfWork.Save();
        }

        public IEnumerable<ItemEo> GetItemByCompanyAndCategory(int companyId, int categoryId)
        {
            List<ItemEo> _clist = _UnitOfWork._items.GetItemByCompanyAndCategory(companyId,categoryId).ToList();
            return _clist;
        }
        public IEnumerable<ItemEo> GetItemByCompany ( int companyId)
        {
            List<ItemEo> _clist = _UnitOfWork._items.GetItemByCompany(companyId).ToList();  
            return _clist;
        }
        public IEnumerable<ItemEo> GetItemByCategory(int categoryId)
        {
            List<ItemEo> _clist = _UnitOfWork._items.GetItemByCategory(categoryId).ToList();
            return _clist;
        }
    }
}
