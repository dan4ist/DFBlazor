using DFBlazor.Data;
using DFBlazor.Repos;
using DFBlazor.Shared;

namespace DFBlazor.Services {
    public class NoteService {

        public INoteRepo _noteRepo;
        private readonly IConfiguration _configuration;
        public NoteService(INoteRepo noteRepo, IConfiguration configuration) {
            _noteRepo = noteRepo;
            _configuration = configuration;
        }

        public async Task<NoteItem> GetNoteItemByID(int id) {
            return await _noteRepo.GetNoteItemByID(id);
        }

        public async Task<IList<NoteItem>> GetAllNotes() {
            return await _noteRepo.GetAllNotes();
        }

        public async Task<IList<NoteItem>> GetNotesByAuthorID(int id) {
            return await _noteRepo.GetNotesByAuthorID(id);
        }

        public async Task<NoteItem> SaveNote(NoteItem n) {


            //handle anonymous note
            if (n.Author == null && n.AuthorID == null) {
                n.Author = "anon";
                n.AuthorID = "0";
            }

            //set created/updated
            if (n.Created == null) {
                n.Created = DateTime.Now;
            } else {
                n.Updated = DateTime.Now;
            }

            if (n.ID == 0) {    //ADD NEW NOTE
                n.ID = await _noteRepo.AddNote(n);
                return n;
            } else {            //UPDATE EXISTING NOTE
                await _noteRepo.UpdateNote(n);
                return n;
            }
        }

        public async Task<bool> DeleteNote(NoteItem n) {
            return await _noteRepo.DeleteNote(n.ID);
        }
    }
}
