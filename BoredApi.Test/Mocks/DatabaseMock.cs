﻿using BoredApi.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace BoredApi.Test.Mocks
{
    public static class DatabaseMock
    {
        public static BoredApiContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<BoredApiContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new BoredApiContext(dbContextOptions);
            }
        }
    }
}
