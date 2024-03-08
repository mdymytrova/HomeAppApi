using HomeAppApi.Data;
using HomeAppApi.Models;

namespace HomeAppApi
{
    public class Seed
    {
        private readonly DataContext dataContext;

        public Seed(DataContext context)
        {
            this.dataContext = context;
        }

        public void SeedDataContext()
        {
            if (!dataContext.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Lake"
                    },
                    new Category()
                    {
                        Name = "Country"
                    }
                };
                dataContext.Categories.AddRange(categories);
                dataContext.SaveChanges();

            }
        }

    }
}
