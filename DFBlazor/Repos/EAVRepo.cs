using DFBlazor.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DFBlazor.Repos {
    public class EAVRepo {
        public class EAVRepository {
            private readonly string _connectionString;

            public EAVRepository(string connectionString) {
                _connectionString = connectionString;
            }

            public void AddEntity(EntityModel entity) {
                
            }

            public void UpdateEntity(EntityModel entity) {
                
            }

            public void DeleteEntity(int id) {
                
            }

            public EntityModel GetEntity(int id) {
                return new EntityModel();
            }
        }
    }
} 
