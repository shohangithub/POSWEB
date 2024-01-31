using GQLAttribute;
using GQLGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[GraphQLEntity]
public class PersonModel
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Address { get; set; }
}

[GraphQLEntity]
public class UserModel
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Address { get; set; }
}