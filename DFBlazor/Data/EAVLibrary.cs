namespace DFBlazor.Data {
    public class EAVLibrary {

        public bool SaveEntity(Entity e) {
            return true;
        }

        public Entity GetEntityByID(int id) {
            return new Entity();
        }

        public IList<Entity> GetEntities() { 
        return new List<Entity>();
        }

        public bool UpdateEntity(Entity e) {
            return true;
        }


    }
}
