namespace DFBlazor.Data {
    public class EAVLibrary {

        public async Task<bool> SaveEntity(EntityModel e) {
            return true;
        }

        public EntityModel GetEntityByID(int id) {
            return new EntityModel();
        }

        public IList<EntityModel> GetEntities() { 
        return new List<EntityModel>();
        }

        public bool UpdateEntity(EntityModel e) {
            return true;
        }



        public async Task<bool> SaveAttribute(AttributeModel e) {
            return true;
        }

        public AttributeModel GetAttributeByID(int id) {
            return new AttributeModel();
        }

        public IList<AttributeModel> GetAttributes() {
            return new List<AttributeModel>();
        }

        public bool UpdateAttribute(AttributeModel e) {
            return true;
        }



        public async Task<bool> SaveValue(ValueModel e) {
            return true;
        }

        public ValueModel GetValueByID(int id) {
            return new ValueModel();
        }

        public IList<ValueModel> GetValues() {
            return new List<ValueModel>();
        }

        public bool UpdateValue(ValueModel e) {
            return true;
        }
    }
}
