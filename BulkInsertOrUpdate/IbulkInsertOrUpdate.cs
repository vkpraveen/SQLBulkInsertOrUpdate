using System.Collections.Generic;

namespace BulkInsertOrUpdate
{
    public interface IBulkInsertOrUpdate
    {
        int BulkInsertUpdate(List<MyBulkClass> recordsList);
    }
}