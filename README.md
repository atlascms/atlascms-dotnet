
This is the Atlas Headless CMS .NET Client SDK (Preview)

**!!! NOTE !!!**

**AtlasCMS is planning to be RTM for all by Jan '23.
We are currently on RC phase with just selected customers and professionals working with it.
We decided to don't have a WebSite or Documentation online as far as we go RTM.**

**If you want to try Atlas CMS and be part of the selected community, totally free, before the RTM, write to support@atlascms.io**

## What is Atlas CMS
**Atlas** is a Cloud based SaaS Headless CMS. It has been created by developers for Developers, Agencies and Content Creators  for the creation of their digital projects.

It wants to be simple, feature rich and with lighting speed performances and it contains the following features:

- RESTful APIs  
- GraphQL Content Delivery
- Powerful filters to search any part of the contents
- Visual Model Builder with automatic API creation
- Components
- 30+ ready to go field types
- Media Library and Media Analyzer
- Image Editor
- Image Server (resize, crop, format change, WebP)
- Admin Users and Permissions
- Project Users and Permission
- Multilanguage
- Webhooks
- ....many more



## Getting started

We recommend you use the NuGet Package Manager to add the library to your .NET Application using one of the following options:

- In Visual Studio, open Package Manager Console window and run the following command:

  ```powershell
  PM> Install-Package AtlasCMS
  ```

- In a command-line, run the following .NET CLI command:

  ```console
  > dotnet add package AtlasCMS
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
var myContent = await client.GetContent<object>("<model-api-key>", "<content-id>");
```
The above request will return a object of type `Content<object>` where its definition is the following:

```csharp
public class Content<T> where T : class
 {
     public string Id { get; set; }
     public string Locale { get; set; }
     public DateTime CreatedAt { get; private set; }
     public string ModelId { get; private set; }
     public string ModelKey { get; private set; }
     public string CreatedBy { get; private set; }
     public DateTime ModifiedAt { get; private set; }
     public string ModifiedBy { get; private set; }
     public string Hash { get; private set; }
     public T Attributes { get; set; }
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

var myBook = await client.GetContent<Book>("<model-api-key>", "<content-id>");
```
Now in the Attribute property of the `Content<Book>` you can use your strongly typed class.
