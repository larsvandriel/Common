using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.EntityFramework
{
    public interface IDbUpdateConcurrencyConflictDetector
    {
        bool IsConcurrencyConflict(DbUpdateException exception);
    }
}
