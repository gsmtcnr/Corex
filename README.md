# Corex
It provides us with infrastructure for the projects we will develop with .Net5.
With many helpful methods, the bug risk of our project is minimized. lowers the level.
It enables the developers who will be involved in the project to adapt easily.


# utility

You can use Reflection, WebClient, HttpWebRequest etc. helper methods.

# pattern

It is the main model structure of our projects. All of the entity and dto models we will use in the project must derive from "IModel".
The uniqeidentifer value of each of our projects is unique in some cases as "int" and in others as "guid". Or, this value can change from table to table, specific to the project.
Therefore, IModel > TKey takes the value generic. Ex: If you are using int as PK, you should make UserModel<int>.
Each method must return from the objects contained in the "Results".


>DtoModel

Base class for data transfer objects
>EntityModel

Base class for Entity objects
# data
It helps us to set up a database ORM structure with a repository pattern in the data layer.
>EntityFramework

You can set up your ORM structure with MSSQL database.

# Exception Handling

 You can handle and catch errors of Authentication, Business, Validation and Data operations.
"BaseExceptionManager" will be ready to use in your project when inherited.

# validation

In particular, Dto, InputModel, etc. objects taken from the page should be validated. Baseclasses that will help us in validation processes are available here.

# log
It helps us to establish the structure of the environment where we will log before/after the operations we have determined in our project.

> AzureTableStorage

 If you have a resource on Azure Table Storage, you can specify the connectionString and make the data you want to log as "IAzureLogData" and do your logging.
 
> MSSQL

 In order to log the table you created in the logging process in SQL, you will be expected to provide the SqlCommand related to the "CreateCommand" method in the "BaseSQLLogger".
It will create and execute the connection object for you.

# mapper

 Between the Entity layer and the Dto layer, our objects usually contain the same values. We use these values ​​to map them together.
 >Mapsterx
 
 It will help to handle our concrete object that you will create by inheriting BaseMapster.
 >AutoMapperx
 
 Very soon..

# Serializer
ExpressionSerializer provides base class for your JsonSerializer operations.
# >Serializer.JsonSerializer
>NSoft
 
 It performs serialize and deserialize operations using the NewtownSoft library.

>SJlast

 If you want to use system.text.json instead of using a third-party library, you can choose it.

# >Serializer.ExpressionSerializer
 >SLinq
 
 Standard json serialier libraries cannot help us to serialize expressions we write using Linq, such as "s=>s.Id==12". That's why you can serialize your linq queries with "SLinq".
 
# Template Render

 The operations we want to create dynamic string values ​​will help us to provide the rendering infrastructure.
 
> HBars
 
 Used the HandleBarsDotNet library. You'll just need to inherit "BaseHandleBarsRender" for use.
 
# Push Senders
  
Mobile devices have unique values. Sending notifications to these values ​​will help us provide the infrastructure for the operations we want.

> OneSignal

  It uses the OneSignal library. When you inherit "BaseOneSignalPushSender" for use, it will be sufficient to provide "ApiKey,ApiId,ApiUrl" information.

# SMS Sender
   
Sending sms will help us to provide the infrastructure for the operations we need to do.

> PostaGuverici

Posta Güvercini is a very widely used SMS service in Turkey. If you have a Posta Guverci account, it will be ready to use if you inherit "BasePostaGuverciniSmsSender" and set the userName,password and url information.

# Email Sender
  
> SendGrid

 Azure SendGrid library is used. It will be available if you specify your Azure SendGrid account information after inheriting "BaseSendGridEmailSender".
 
> Smtp

It allows sending mail using System.Net.Mail. It will be ready to use if you specify your information after inheriting "BaseSMTPEmailSender".

# Cloud File
  
It is an infrastructure that will allow you to upload your files to the cloud.
> AzureBlobStorage

It is a structure that you can use simply by setting the connectionString information of your Azure Blob Storage account. You should just inherit "BaseAzureBlobStorage".

# PDF Converter

It is an infrastructure that will enable us to convert our HTML-formatted pages to PDF.

> DinkToPDFConverter
  
Used the DinkToPDF library. You need to inherit "BaseCloudDinkToPDFConverter" so that the created PDF can be saved on the cloud.
It will ask you to give "IUploadAsync", and you will give the concrete object that you made cloudFile while overriding this method.

If you want to save to a physical file path, inheriting "BasePhysicalDinkToPDFConverter" will suffice.

> SelectPDFConverter

Used the SelectPDF library. You need to inherit "BaseCloudSelectPDFConverter" so that the created PDF can be saved on the cloud.
It will ask you to give "IUploadAsync", and you will give the concrete object that you made cloudFile while overriding this method.

If you want to save to a physical file path, inheriting "BasePhysicalSelectPDFConverter" will suffice.

# Encryption
Url, user password, etc. values ​​are encryption infrastructure.

# > Encryption.Cipher
We use a string variable when doing encryption. However, when we say encrypt with the value "X", it will be a more reliable encryption for us.

> SHA256

Inheriting "BaseSHA256CipherEncryption" will be enough to use Decrypt,Encrypt methods.

# cache

In certain situations, we want to access our data without constantly going to the main database. This will bring many benefits to our project.
Note: If you want to set a value in front of each cacheKey during caching. You can use prefix property.
> Memory

When you inherit the "BaseMemoryCacheManager", it caches your data using the RAM of the server where your application is running.

> Redis

"BaseRedisCacheManager" keeps your data on Redis by connecting to your Redis server with the connectionString you specify when you inherit.

# DocumentDB

There are many technologies available to store the data of our project. We can say that DocumentDBs are the most performing ones.

> DocumentDB.MongoDB.V1
  MongoDB is a structure with its own infrastructure and an architecture within itself. This is v1 version.

# Operation

Our operations are teams that will take on every single job completely. BusinessOperation, DataOperation, ValidationOperation and their manager Operation.Manager.

> Business Operations

We should inherit "BaseBusinessOperation" for the businessOperation layer where we will perform calculations etc.

> Data Operation

We should inherit "BaseDataOperation" for the layer that will talk to the Data Repository layer and bring the data to operation.manager. We should give the entity, repository, key values ​​we will use as generic.

> Validation Operation

It is the operation layer that extracts the list of validations made for each object in the validation layer and forwards it to operation.manager. By inheriting "BaseValidationOperation", the model whose validation list will be collected is set as generic.

> Operation Manager

It is the only layer where all operations are gathered. It is the only layer that establishes a use-case relationship with ApplicationLayer.
It contains a concrete ExceptionManager and a "BaseOperationManager" that can serve each object.
It presents you all CRUD operations when BaseOperationManager is given TKey,TEntity,TModel ie <int,UserEntity,UserModel>.
Get, GetList, Insert, Update, Delete etc.

# Princess
 It provides the infrastructure for the presentation layer. It helps to manage settings with BaseStartup.
 
> MVC

Host features used in MVC projects. You can set "UseSession:true" if you want to use Session, and "UseAuthentication:true" if you want the Authentication settings to be made automatically.

> API

Very soon..
 







