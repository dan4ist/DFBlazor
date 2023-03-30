using Dapper;
using System.Data;
using DFBlazor.Data;
using System.Data.SqlClient;
using Microsoft.Identity.Client;

namespace DFBlazor.Repos {

    public interface INoteRepo {

        Task<int> AddNote(NoteItem note);
        Task<bool> UpdateNote(NoteItem note);
        Task<bool> DeleteNote(int id);
        Task<NoteItem> GetNoteItemByID(int id);
        Task<IList<NoteItem>> GetAllNotes();
        Task<IList<NoteItem>> GetNotesByAuthorID(int id);
    }

    public class NoteRepo : INoteRepo {
        private readonly IConfiguration _config;

        public NoteRepo(IConfiguration config) {
            _config = config;
        }

        public async Task<int> AddNote(NoteItem note) {
            string query = "";

            var parameters = new DynamicParameters();
            parameters.Add("name", note.Name, DbType.String);
            parameters.Add("authorid", note.Author, DbType.String);
            parameters.Add("author", note.Author, DbType.String);
            parameters.Add("html", note.HTML, DbType.String);
            parameters.Add("created", note.Created, DbType.DateTime);
            parameters.Add("modified", note.Updated, DbType.DateTime);

            using (var conn = new SqlConnection(_config.GetConnectionString("DFDB"))) {
                if (conn.State == ConnectionState.Closed) {
                    conn.Open();
                }

                try {
                    return await conn.ExecuteScalar<Task<int>>(query, parameters);
                } catch (Exception ex) {
                    //handle this somehow
                    throw;
                } finally {
                    if (conn.State == ConnectionState.Open) {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<bool> UpdateNote(NoteItem note) {
            string query = "";

            var parameters = new DynamicParameters();
            parameters.Add("id", note.ID, DbType.Int32);
            parameters.Add("name", note.Name, DbType.String);
            parameters.Add("authorid", note.Author, DbType.String);
            parameters.Add("author", note.Author, DbType.String);
            parameters.Add("html", note.HTML, DbType.String);
            parameters.Add("created", note.Created, DbType.DateTime);
            parameters.Add("modified", note.Updated, DbType.DateTime);

            using (var conn = new SqlConnection(_config.GetConnectionString("DFDB"))) {
                if (conn.State == ConnectionState.Closed) {
                    conn.Open();
                }

                try {
                    await conn.ExecuteAsync(query, parameters);
                    return true;
                } catch (Exception ex) {
                    //handle this somehow
                    throw;
                } finally {
                    if (conn.State == ConnectionState.Open) {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<bool> DeleteNote(int id) {
            string query = "";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);

            using (var conn = new SqlConnection(_config.GetConnectionString("DFDB"))) {
                if (conn.State == ConnectionState.Closed) {
                    conn.Open();
                }

                try {
                    await conn.ExecuteAsync(query, parameters);
                    return true;
                } catch (Exception ex) {
                    //handle this somehow
                    throw;
                } finally {
                    if (conn.State == ConnectionState.Open) {
                        conn.Close();
                    }
                }
            }
        }

        public async Task<NoteItem> GetNoteItemByID(int id) {
            var item = new NoteItem();
            return item;
        }

        public async Task<IList<NoteItem>> GetAllNotes() {
            IList<NoteItem> items = new List<NoteItem>();
            return items;
        }

        public async Task<IList<NoteItem>> GetNotesByAuthorID(int id) {
            IList<NoteItem> items = new List<NoteItem>();
            return items;
        }
    }

}
