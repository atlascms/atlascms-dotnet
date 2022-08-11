## Getting started

We recommend you use the NuGet Package Manager to add the library to your .NET Application using one of the following options:

- In Visual Studio, open Package Manager Console window and run the following command:

  ```powershell
  PM> Install-Package [STILL TO DEFINE]
  ```

- In a command-line, run the following .NET CLI command:

  ```console
  > dotnet add package [STILL TO DEFINE]
## Use the Client

```csharp
var options = new AtlasOptions 
{
    BaseUrl = "<Atlas CMS Base Url>",
	ApiKey = "<Your Api Key or Token>"
};

var client = new AtlasClient(options);
```
Now you can use all the features exposed by the client. For Example if you want to read a content you can

```csharp
var myContent = await client.GetContent<object>("<model-key>", "<content-id>");
```
The above request will return a object of type `Content<object>` where its definition is the following:

```csharp
public class Content<T> where T : class
 {
     /// <summary>
     /// Content Id
     /// </summary>
     public string Id { get; set; }

     /// <summary>
     /// Content Locale
     /// </summary>
     public string Locale { get; set; }

     /// <summary>
     /// Created At (UTC Date)
     /// </summary>
     public DateTime CreatedAt { get; private set; }

     /// <summary>
     /// The Model Id to which the content belongs
     /// </summary>
     public string ModelId { get; private set; }

     /// <summary>
     /// The Model Key to which the content belongs
     /// </summary>
     public string ModelKey { get; private set; }

     /// <summary>
     /// Created By
     /// </summary>
     public string CreatedBy { get; private set; }

     /// <summary>
     /// Modified At (UTC Date)
     /// </summary>
     public DateTime ModifiedAt { get; private set; }

     /// <summary>
     /// Modified By
     /// </summary>
     public string ModifiedBy { get; private set; }

     /// <summary>
     /// Content Hash
     /// </summary>
     public string Hash { get; private set; }

     /// <summary>
     /// Content Data
     /// </summary>
     public T Attributes { get; set; }

     /// <summary>
     /// Available Translations
     /// </summary>
     public List<ContentLocale> Locales { get; private set; } = new List<ContentLocale>();
 }
```
**Strongly Typed Attributes**
Each Content in Atlas has Attributes that are the dynamic values based on the Model.
If you don't want to work with *objects* you can create your own class that match the Model Field keys in Atlas and use it.
For example, if you created in Atlas a **Model** called *Book* with the following keys:

 - Title (text)
 - ISBN (text)
 - NumberOfPages (num int)
 - Tags (array of strings)

you can use the following approach:

```csharp
public class Book
{
	public string Title { get; set; }
	public string ISBN { get; set; }
	public int NumberOfPages { get; set; }
	public string[] Tags { get; set; }
}

var myBook = await client.GetContent<Book>("<model-key>", "<content-id>");
```
Now in the Attribute property of the `Content<Book>` you can use your strongly typed class.
