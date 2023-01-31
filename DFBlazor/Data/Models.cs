namespace DFBlazor.Data {

    public class Entity {
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<Attribute> Attributes { get; set; }
    }

    public class Attribute {
        public int ID { get; set; }
        public int EntityID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class Value {
        public int ID { get; set; }
        public int AttributeID { get; set; }
        public int EntityID { get; set; }
        public string Data { get; set; } 
    }
}
