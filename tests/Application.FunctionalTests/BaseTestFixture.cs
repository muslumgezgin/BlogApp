using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Application.FunctionalTests;

using static Testing;
[TestFixture]
public class BaseTestFixture
{
    [SetUp]
    public async Task SetUp()
    {
        await ResetState();
    }

}
