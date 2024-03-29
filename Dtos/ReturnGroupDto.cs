﻿using System.Collections.Generic;

namespace BoredApi.Dtos
{
    public class ReturnGroupDto
    {
        public string Name { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public List<int>? Users { get; set; } = new List<int>();
    }
}
