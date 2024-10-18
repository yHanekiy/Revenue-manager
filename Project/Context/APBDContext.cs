using Microsoft.EntityFrameworkCore;
using Project.Model;
using Project.Model.Configuration;

namespace Project.Context;

public class APBDContext : DbContext
{
    public DbSet<ClientPhysical> ClientPhysicals { get; set; }
    public DbSet<ClientFirm> ClientFirms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<AbstractClient> AbstractClients { get; set; }




    public APBDContext()
    {
    }

    public APBDContext(DbContextOptions<APBDContext> options) : base(options)
    {
    }
    
    
    //Need to add connection String to a Database
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder.UseSqlServer();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientFirmConfig).Assembly);
    }
}