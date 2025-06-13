public class EfCoreLearnTests
{
    private TestDbContext _context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new TestDbContext(options);
    }

    [Test]
    public void CanAddAndRetrieveEntity()
    {
        // Learning: How does EF Core track entities?
        var user = new User { Name = "John" };
        _context.Users.Add(user);
        _context.SaveChanges();

        var retrieved = _context.Users.First(u => u.Name == "John");
        Assert.AreEqual("John", retrieved.Name);
        Assert.Greater(retrieved.Id, 0); // Learning: ID is auto-generated
    }

    [Test]
    public void EntityStateTracking()
    {
        // Learning: How does change tracking work?
        var user = new User { Name = "John" };
        
        Assert.AreEqual(EntityState.Detached, _context.Entry(user).State);
        
        _context.Users.Add(user);
        Assert.AreEqual(EntityState.Added, _context.Entry(user).State);
        
        _context.SaveChanges();
        Assert.AreEqual(EntityState.Unchanged, _context.Entry(user).State);
        
        user.Name = "Jane";
        Assert.AreEqual(EntityState.Modified, _context.Entry(user).State);
    }

    [Test]
    public void LazyLoadingBehavior()
    {
        // Learning: When are related entities loaded?
        var user = new User { Name = "John" };
        var order = new Order { UserId = user.Id, Amount = 100 };
        
        _context.Users.Add(user);
        _context.Orders.Add(order);
        _context.SaveChanges();
        
        var retrievedOrder = _context.Orders.First();
        // Learning: User is null without explicit loading
        Assert.IsNull(retrievedOrder.User);
        
        // Must explicitly include
        var orderWithUser = _context.Orders
            .Include(o => o.User)
            .First();
        Assert.IsNotNull(orderWithUser.User);
    }
}
