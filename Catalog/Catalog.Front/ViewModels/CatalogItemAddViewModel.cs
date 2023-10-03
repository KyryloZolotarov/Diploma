using Catalog.Front.Models.Dtos;
using Catalog.Front.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.ViewModels
{
    public class CatalogItemAddViewModel
    {
        public CatalogItemDto Item { get; set; }
        private readonly ICatalogItemService _catalogItemService;
        public CatalogItemAddViewModel(ICatalogItemService catalogItemService)
        {
            _catalogItemService = catalogItemService;
            Item = new CatalogItemDto();
        }
    }
}
