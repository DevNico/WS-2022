﻿using Microsoft.EntityFrameworkCore;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Infrastructure.Data;

namespace ServiceReleaseManager.Api;

public static class SeedData
{
  public static readonly Organisation TestOrganisation1 =
    new("Test Organisation");

  // public static readonly ToDoItem ToDoItem1 = new ToDoItem
  // {
  //   Title = "Get Sample Working", Description = "Try to get the sample to build."
  // };
  //
  // public static readonly ToDoItem ToDoItem2 = new ToDoItem
  // {
  //   Title = "Review Solution",
  //   Description =
  //     "Review the different Organisations in the solution and how they relate to one another."
  // };
  //
  // public static readonly ToDoItem ToDoItem3 = new ToDoItem
  // {
  //   Title = "Run and Review Tests",
  //   Description = "Make sure all the tests run and review what they are doing."
  // };

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
      serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
    if (dbContext.Organisations.Any())
    {
      return; // DB has been seeded
    }

    PopulateTestData(dbContext);
  }

  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Organisations)
    {
      dbContext.Remove(item);
    }

    dbContext.SaveChanges();

    dbContext.Organisations.Add(TestOrganisation1);
    dbContext.SaveChanges();
  }
}