using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Helper
{
	public class PaginationResponse<TEntity>
	{
		public PaginationResponse(int pagSize, int pageIndex, int count, IEnumerable<TEntity> data)
		{
			PagSize = pagSize;
			PageIndex = pageIndex;
			Count = count;
			Data = data;
		}

		public int PagSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
