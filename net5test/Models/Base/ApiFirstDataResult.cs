

namespace net5test.Models.Base
{
    public class ApiFirstDataResult<T> : ApiResult
    {
        /// <summary>
        /// ID
        /// </summary>
        public int account_id { get; set; }

        /// <summary>
        /// 回傳資料
        /// </summary>
        public T data { set; get; }
    }
}
