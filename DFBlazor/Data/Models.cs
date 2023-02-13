namespace DFBlazor.Data {

    public class EntityModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<AttributeModel> Attributes { get; set; }
        public IList<EntityModel> ChildEntities { get; set; }
    }

    public class AttributeModel {
        public int ID { get; set; }
        public int EntityID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IList<ValueModel> Values { get; set; }
        public IList<EntityModel> Entities { get; set;}
    }

    public class ValueModel {
        public int ID { get; set; }
        public int AttributeID { get; set; }
        public int EntityID { get; set; }
        public string Data { get; set; }
        public IList<EntityModel> Entities { get; set; }
        public IList<AttributeModel> Attributes { get; set; }

    }

    public class JobModel {
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public IList<Project> Projects { get; set; }

        public string EndString { get { 
                if (End != null) {
                    return Convert.ToDateTime(End).ToString("MM/yyyy");

                } else {
                    return "Present";
                }
            } }
    }

    public class Project {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Technologies { get; set; }

    }
}
