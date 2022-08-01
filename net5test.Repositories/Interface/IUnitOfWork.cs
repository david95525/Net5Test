using System;
using System.Threading.Tasks;


namespace net5test.Repositories.Interface
{

    public interface IUnitOfWork : IDisposable
        {
            /// <summary>
            ///
            /// </summary>
            //IGenericRepository<Member> MemberRepository { get; }

            /// <summary>
            /// DB Context
            /// </summary>
            //DbContext Context { get; }

            /// <summary>
            /// Saves the change.
            /// </summary>
            /// <returns></returns>
            Task<int> SaveChangeAsync();
        }
    
}
