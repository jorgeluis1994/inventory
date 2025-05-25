﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
