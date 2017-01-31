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
                new Gender() { Name = "����"},
                new Gender() { Name = "����" },
                new Gender() { Name = "����" },
                new Gender() { Name = "Unisex" }
            );

            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "����" },
                new Category() { Name = "���������� ���������" },
                new Category() { Name = "����" },
                new Category() { Name = "����" },
                new Category() { Name = "��������" },
                new Category() { Name = "���������� �������" },
                new Category() { Name = "���������� ����������" },
                new Category() { Name = "������� � �������" },
                new Category() { Name = "����� � �����" },
                new Category() { Name = "���������� ���������" }
            );
            context.SaveChanges();

            var kosa = context.Categories.First(x => x.Name == "����");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "��� �� ����", ParentCategory = kosa },
                new Category() { Name = "�������� � �������", ParentCategory = kosa },
                new Category() { Name = "�������", ParentCategory = kosa },
                new Category() { Name = "�����������/ UV ������", ParentCategory = kosa },
                new Category() { Name = "���� �� ����", ParentCategory = kosa },
                new Category() { Name = "����������� � ������", ParentCategory = kosa }
            );

            var fracc = context.Categories.First(x => x.Name == "���������� ���������");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "�������", ParentCategory = fracc },
                new Category() { Name = "�����", ParentCategory = fracc },
                new Category() { Name = "����", ParentCategory = fracc },
                new Category() { Name = "������� �� ������������", ParentCategory = fracc },
                new Category() { Name = "������", ParentCategory = fracc },
                new Category() { Name = "������� / �����", ParentCategory = fracc },
                new Category() { Name = "���������", ParentCategory = fracc }
            );

            var lice = context.Categories.First(x => x.Name == "����");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "���������� ��������", ParentCategory = lice },
                new Category() { Name = "������", ParentCategory = lice },
                new Category() { Name = "�������", ParentCategory = lice },
                new Category() { Name = "������", ParentCategory = lice },
                new Category() { Name = "��������� ������", ParentCategory = lice },
                new Category() { Name = "�����", ParentCategory = lice },
                new Category() { Name = "������ ������", ParentCategory = lice },
                new Category() { Name = "�������������", ParentCategory = lice },
                new Category() { Name = "���������", ParentCategory = lice }
            );

            var epil = context.Categories.First(x => x.Name == "��������");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "���� �����", ParentCategory = epil },
                new Category() { Name = "�������� �� ���� ��������", ParentCategory = epil },
                new Category() { Name = "���������� �������", ParentCategory = epil },
                new Category() { Name = "�����������", ParentCategory = epil }
            );

            var manPed = context.Categories.First(x => x.Name == "������� � �������");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "��� �� �����", ParentCategory = manPed },
                new Category() { Name = "��� ���", ParentCategory = manPed },
                new Category() { Name = "���������� ������", ParentCategory = manPed },
                new Category() { Name = "����� �� ����� � �������", ParentCategory = manPed },
                new Category() { Name = "����� �� ���� � �����", ParentCategory = manPed },
                new Category() { Name = "����", ParentCategory = manPed },
                new Category() { Name = "���������� �����", ParentCategory = manPed },
                new Category() { Name = "�����", ParentCategory = manPed },
                new Category() { Name = "�����������", ParentCategory = manPed },
                new Category() { Name = "��������� � ����������", ParentCategory = manPed },
                new Category() { Name = "����������", ParentCategory = manPed }
            );
            context.SaveChanges();

            var shampoo = context.Categories.First(x => x.Name == "�������� � �������");
            context.Categories.AddOrUpdate(
                c => c.Name,
                new Category() { Name = "���� � ��������", ParentCategory = shampoo },
                new Category() { Name = "���������", ParentCategory = shampoo },
                new Category() { Name = "�������", ParentCategory = shampoo },
                new Category() { Name = "����", ParentCategory = shampoo },
                new Category() { Name = "����� ��� ����", ParentCategory = shampoo },
                new Category() { Name = "�����������", ParentCategory = shampoo },
                new Category() { Name = "�������������� � �����������", ParentCategory = shampoo },
                new Category() { Name = "������ ������ / ������������� �����", ParentCategory = shampoo },
                new Category() { Name = "������ �������", ParentCategory = shampoo },
                new Category() { Name = "������� ����������", ParentCategory = shampoo },
                new Category() { Name = "���� ��������", ParentCategory = shampoo },
                new Category() { Name = "���� ���� / ������", ParentCategory = shampoo }
            );
            context.SaveChanges();
            var female = context.Genders.First(x => x.Name == "����");
            var male = context.Genders.First(x => x.Name == "����");
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
                    if (item.Name == "�������� � �������")
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
