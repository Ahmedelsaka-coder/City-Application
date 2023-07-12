using CityApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace CityApp.Controllers
{
   public enum SortDirection
    {
        Ascending,
        Descending
    }
    public class CityController : Controller
    {
        CityDatabaseContext dbContext = new CityDatabaseContext();
        public IActionResult Index(string SortField , string CurrentSortField, SortDirection SortDirection, string searchByName)
        {
            //List<City> cities = dbContext.Cities.ToList();
            var cities = GetCities();
            if (!string.IsNullOrEmpty(searchByName))
                cities = cities.Where(e => e.EnglishName.ToLower().Contains(searchByName.ToLower())).ToList();
            return View(this.SortCities(cities,SortField,CurrentSortField,SortDirection));
        }

        private List<City> GetCities()
        {
            var cities = (from city in dbContext.Cities
                          join region in dbContext.Regions on city.Regionid equals region.RegionID
                          select new City
                          {
                              CityId = city.CityId,
                              Code = city.Code,
                              ArabicName = city.ArabicName,
                              EnglishName = city.EnglishName,
                              Notes = city.Notes,
                              isActive = city.isActive,
                              Regionid = city.Regionid,
                              RegionName = region.RegionName
                          }).ToList();
            return cities;
        }

        public IActionResult Create()
        {
            ViewBag.Regions = this.dbContext.Regions.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(City model)
        {
            ModelState.Remove("CityId");
            ModelState.Remove("isActive");
            ModelState.Remove("Region");
            ModelState.Remove("RegionName");
            if(ModelState.IsValid)
            {
                dbContext.Cities.Add(model);
                    dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.Regions = this.dbContext.Regions.ToList();
            return View();

        }
        public IActionResult Edit(int ID)
        {
            City data = this.dbContext.Cities.Where(e => e.CityId == ID).FirstOrDefault();
            ViewBag.Regions=this.dbContext.Regions.ToList();
            return View("Create",data);
        }
        [HttpPost]
        public IActionResult Edit(City model)
        {
            ModelState.Remove("CityId");
            ModelState.Remove("isActive");
            ModelState.Remove("Region");
            ModelState.Remove("RegionName");
            if (ModelState.IsValid)
            {
                dbContext.Cities.Update(model);
                dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.Regions = this.dbContext.Regions.ToList();
            return View("Create,model");
        }

        public IActionResult Delete(int ID)
        {
            City data = this.dbContext.Cities.Where(e => e.CityId == ID).FirstOrDefault();
            if(data!=null)
            {
                dbContext.Cities.Remove(data);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        private List<City> SortCities(List<City> cities, string sortField, string currentSortField, SortDirection sortDirection)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "ArabicName";
                ViewBag.SortDirection = SortDirection.Ascending;
            }
            else
            {
                if (currentSortField == sortField)
                    ViewBag.SortDirection = sortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
                else
                    ViewBag.SortDirection = SortDirection.Ascending;
                ViewBag.SortField = sortField;

            }
            var propertyInfo = typeof(City).GetProperty(ViewBag.SortField);
            if (ViewBag.SortDirection == SortDirection.Ascending)
                cities = cities.OrderBy(e => propertyInfo.GetValue(e, null)).ToList();
            else
                cities = cities.OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();
            return cities;

        }
    }
}
