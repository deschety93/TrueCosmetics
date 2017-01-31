namespace TrueCosmetics.BootstrapApp.Migrations
{
    using Data;
    using Data.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(
                r => r.Name,
                new IdentityRole() { Name = "Admin" },
                new IdentityRole() { Name = "Employee" },
                new IdentityRole() { Name = "Customer" }
            );

            context.Genders.AddOrUpdate(
                c => c.Name,
                new Gender() { Name = "Мъже"},
                new Gender() { Name = "Жени" },
                new Gender() { Name = "Деца" },
                new Gender() { Name = "Unisex" }
            );

            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Коса" },
                new Category() { Name = "Фризьорски аксесоари" },
                new Category() { Name = "Лице" },
                new Category() { Name = "Тяло" },
                new Category() { Name = "Епилация" },
                new Category() { Name = "Козметични апарати" },
                new Category() { Name = "Козметично оборудване" },
                new Category() { Name = "Маникюр и педикюр" },
                new Category() { Name = "Мигли и вежди" },
                new Category() { Name = "Еднократни материали" }
            );
            context.SaveChanges();

            var kosa = context.Categories.First(x => x.Name == "Коса");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Боя за коса", ParentCategory = kosa },
                new Category() { Name = "Шампоани и Балсами", ParentCategory = kosa },
                new Category() { Name = "Косопад", ParentCategory = kosa },
                new Category() { Name = "Термозащита/ UV защита", ParentCategory = kosa },
                new Category() { Name = "Олио за коса", ParentCategory = kosa },
                new Category() { Name = "Стилизиране и блясък", ParentCategory = kosa }
            );

            var fracc = context.Categories.First(x => x.Name == "Фризьорски аксесоари");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Сешоари", ParentCategory = fracc },
                new Category() { Name = "Преси", ParentCategory = fracc },
                new Category() { Name = "Маши", ParentCategory = fracc },
                new Category() { Name = "Машинки за подстригване", ParentCategory = fracc },
                new Category() { Name = "Ножици", ParentCategory = fracc },
                new Category() { Name = "Гребени / Четки", ParentCategory = fracc },
                new Category() { Name = "Аксесоари", ParentCategory = fracc }
            );

            var lice = context.Categories.First(x => x.Name == "Лице");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Почистващи продукти", ParentCategory = lice },
                new Category() { Name = "Пилинг", ParentCategory = lice },
                new Category() { Name = "Кремове", ParentCategory = lice },
                new Category() { Name = "Серуми", ParentCategory = lice },
                new Category() { Name = "Околоочен контур", ParentCategory = lice },
                new Category() { Name = "Маски", ParentCategory = lice },
                new Category() { Name = "Против бръчки", ParentCategory = lice },
                new Category() { Name = "Слънцезащитни", ParentCategory = lice },
                new Category() { Name = "Органични", ParentCategory = lice }
            );

            var epil = context.Categories.First(x => x.Name == "Епилация");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Коло маска", ParentCategory = epil },
                new Category() { Name = "Продукти за след епилация", ParentCategory = epil },
                new Category() { Name = "Подгряващи апарати", ParentCategory = epil },
                new Category() { Name = "Консумативи", ParentCategory = epil }
            );

            var manPed = context.Categories.First(x => x.Name == "Маникюр и педикюр");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Лак за нокти", ParentCategory = manPed },
                new Category() { Name = "Гел лак", ParentCategory = manPed },
                new Category() { Name = "Изграждащи гелове", ParentCategory = manPed },
                new Category() { Name = "Грижа за нокти и кожички", ParentCategory = manPed },
                new Category() { Name = "Грижа за ръце и крака", ParentCategory = manPed },
                new Category() { Name = "Пили", ParentCategory = manPed },
                new Category() { Name = "Изкуствени нокти", ParentCategory = manPed },
                new Category() { Name = "Четки", ParentCategory = manPed },
                new Category() { Name = "Инструменти", ParentCategory = manPed },
                new Category() { Name = "Декорации и аксескоари", ParentCategory = manPed },
                new Category() { Name = "Оборудване", ParentCategory = manPed }
            );
            context.SaveChanges();

            var shampoo = context.Categories.First(x => x.Name == "Шампоани и Балсами");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "Суха и изтощена", ParentCategory = shampoo },
                new Category() { Name = "Боядисана", ParentCategory = shampoo },
                new Category() { Name = "Къдрава", ParentCategory = shampoo },
                new Category() { Name = "Обем", ParentCategory = shampoo },
                new Category() { Name = "Всеки тип коса", ParentCategory = shampoo },
                new Category() { Name = "Приглаждащи", ParentCategory = shampoo },
                new Category() { Name = "Възстановяване и подхранване", ParentCategory = shampoo },
                new Category() { Name = "Против пърхот / чувствителене скалп", ParentCategory = shampoo },
                new Category() { Name = "Против косопад", ParentCategory = shampoo },
                new Category() { Name = "Дълбоко почистване", ParentCategory = shampoo },
                new Category() { Name = "Сухи шампоани", ParentCategory = shampoo },
                new Category() { Name = "Руса коса / Кичури", ParentCategory = shampoo }
            );
            context.SaveChanges();
            var female = context.Genders.First(x => x.Name == "Жени");
            var male = context.Genders.First(x => x.Name == "Мъже");
            var uni = context.Genders.First(x => x.Name == "Unisex");
            foreach (var item in context.Categories.Include(x => x.ParentCategory).ToList())
            {
                if(item.ParentCategory == null)
                {
                    context.GenderCategories.AddOrUpdate(
                        x => new { x.CategoryId, x.GenderId },
                        new GenderCategory() { CategoryId = item.Id, GenderId = uni.Id, Category = item, Gender = uni }
                    );
                }
                else
                {
                    context.GenderCategories.AddOrUpdate(
                        x => new { x.CategoryId, x.GenderId },
                        new GenderCategory() { CategoryId = item.Id, GenderId = female.Id, Category = item, Gender = female }
                    );
                    if (item.Name == "Шампоани и Балсами")
                    {
                        context.GenderCategories.AddOrUpdate(
                        x => new { x.CategoryId, x.GenderId },
                            new GenderCategory() { CategoryId = item.Id, GenderId = male.Id, Category = item, Gender = male }
                        );
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
