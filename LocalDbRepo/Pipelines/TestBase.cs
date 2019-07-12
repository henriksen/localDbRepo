using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EfLocalDb;

namespace LocalDbRepo.Pipelines {
    public class TestBase {

        static SqlInstance<CommunicationContext> instance;

        static TestBase () {
            instance = new SqlInstance<CommunicationContext> (
                constructInstance: builder => new CommunicationContext (builder.Options));
        }

        public Task<SqlDatabase<CommunicationContext>> LocalDb (
            string databaseSuffix = null, [CallerMemberName] string memberName = null) {
            return instance.Build (GetType ().Name, databaseSuffix, memberName);
        }
    }
}