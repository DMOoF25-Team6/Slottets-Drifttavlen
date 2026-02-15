// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Domain.Tests;

[TestClass]
public sealed class Test1
{
    [TestInitialize]
    public void TestInit()
    {
        // This method is called before each test method.
    }

    [TestCleanup]
    public void TestCleanup()
    {
        // This method is called after each test method.
    }

    [TestMethod]
    public void SimpleAdditionTest()
    {
        // Assert that basic arithmetic works as expected.
        int testvar = 2 + 2;

        // Test that 2 + 2 equals 4
        Assert.AreEqual(4, testvar);
    }
}
