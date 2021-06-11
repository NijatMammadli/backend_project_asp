﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend_project_asp.Models
{
    public class Position
    {
        public int Id { get; set; }

        public string PositionName { get; set; }

        public bool IsDeleted { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}
