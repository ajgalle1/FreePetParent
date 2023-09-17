namespace FreePetParentWeb.Models
{
    public class Parent
    {
        public int Id { get; set; }
        public string ParentName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }

        public string PetDescription { get; set; }
        public string PetRequirements { get; set; }
        public Parent()
        {
            //constructor
        }
    }
}
