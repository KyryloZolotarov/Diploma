﻿using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.BasketViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<BasketItemFromCatalog> BasketItems { get; set; }
    }
}
