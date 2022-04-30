public class Secretary{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    public Secretary(int id, string name, string email, string password){
        this.id = id;
        this.name = name;
        this.email = email;
        this.password = password;
    }

    public override string ToString()
    {
        return "Secretary [id: " + id + ", name: " + name + ", email: " + email + ", password: " + password + "]";
    }

}