# Corex
.Net5 ile geliştireceğimiz projeler içi bize alt yapı imkanı sunar. 
Bir çok yardımcı methodlar ile projemizin bug riskini min. seviyeye indirir.
Projeye dahil olacak geliştircilerin kolay adapte olmasını sağlar.


# Utility

Reflection, WebClient, HttpWebRequest v.b yardımcı methodları kullanabilirsiniz.

# Model
>DtoModel

Data transfer objeleri için temel sınıf
>EntityModel

Entity objeleri içi temel sınıf
# Data 
Data katmanında repository pattern ile veri tabanı ORM yapısı kurmamıza yardımcı olur.
>EntityFramework

MSSQL veri tabanı ile ORM yapınızı kurabilirisniz.

# Exception Handling

 Authentication, Business, Validation ve Data operasyonlarının hatalarını handle edip yakalayabileceğiniz.
"BaseExceptionManager" miras alındığında projenizde kullanmaya hazır olacaktır.

# Validation

Özellikle sayfadan alınan Dto, InputModel v.b objelerine validasyon yapılması gerekir. Validasyon işlemlerinde bize yardımcı olacak baseclasslar buraya mevcut.

# Log
Projemizde belirlediğimiz operayonlardan önce/sonra loglama yapacağımız ortamın yapısını kurmamıza yardımcı olur.

> AzureTableStorage

 Azure Table Storage üzerinde kaynağınız varsa connectionString belirterek loglamak istediğiniz datayı "IAzureLogData" haline getirip loglama işleminizi yapabilirsiniz.
 
> MSSQL

 SQL'de loglama işleminde oluşturduğunuz tabloya log atmak için "BaseSQLLogger" da "CreateCommand" methodu ile ilgili SqlCommand'ını vermeniz beklenecektir. 
Sizin için connection nesnesini kendisi oluşturup execute edecektir.

# Mapper

 Entity katmanı ile Dto katmanı arasında nesnelerimiz genellikle aynı değerleri barındırır. Bu değerleri birbirine maplemek için kullanırız.
 >Mapsterx
 
 BaseMapster'ı miras alarak oluşturacağınız somut nesne işlemlerimizi yapmaya yardımcı olacaktır.
 >AutoMapperx
 
 Çok yakında..

# Serializer
ExpressionSerializer, JsonSerializer işlemleriniz için base class sağlar.
# >Serializer.JsonSerializer
>NSoft
 
 NewtownSoft kütüphanesini kullanarak serialize, deserialize işlemlerimlerinizi yapar.

>SJson

 Thirdparty bir kütüphane kullanmak yerine system.text.json kullanmak istiyorum derseniz tercih edebilirsiniz.

# >Serializer.ExpressionSerializer
 >SLinq
 
 Linq kullanarak yazdığımız expressionları örn : "s=>s.Id==12" gibi serialize etme konusunda standart json serialier kütüphaneleri bize yardımcı olamaz. Bu yüzden "SLinq" ile linq sorgularılarınızı serialize edebilirsiniz.
 
# Template Render

 Dinamik string değerler oluşturmak istediğimiz operasyonlar bize render alt yapısını sağlamakta yardımcı olacaktır.
 
> HBars
 
 HandleBarsDotNet kütüphanesini kullanılır. Kullanım için "BaseHandleBarsRender" miras almanız yeterli olacaktır.
 
# Push Sender
  
Mobil cihazların unique değerleri vardır. Bu değerlere bildirim göndermek istediğimiz operasyonlar için bize alt yapısını sağlamakta yardımcı olacaktır.
	
> OneSignal

  OneSignal kütüphanesini kullanılır. Kullanım için "BaseOneSignalPushSender" miras aldığınızda "ApiKey,ApiId,ApiUrl" bilgilerini vermeniz yeterli olacaktır.
	
# Sms Sender
   
Sms gönderimi yapmamız gereken operasyonlar için bize alt yapısını sağlamakta yardımcı olacaktır.
	
> PostaGuverici
	
Türkiye'de Posta Güvercini çok yaygın kullanılan bir SMS servisidir. Posta Guverci hesabınız varsa eğer "BasePostaGuverciniSmsSender" miras alarak userName,password ve url bilgilerini set etmeniz halinde kullanıma hazır olacaktır.
	
# Email Sender
  
> SendGrid

 Azure SendGrid kütüphanesini kullanılır. Azure SendGrid hesap bilgilerinizi "BaseSendGridEmailSender" miras aldıktan sonra belirtmeniz halinde kullanıma hazır olacaktır.
 
> Smtp

System.Net.Mail kullanarak mail gönderimi sağlar. Bilgilerinizi "BaseSMTPEmailSender" miras aldıktan belirtmeniz halinde kullanıma hazır olacaktır.

# Cloud File
  
Dosyalarınızı cloud upload yapmanızı sağlayacak bir alt yapıdır.

> AzureBlobStorage

Azure Blob Storage hesabınızın connectionString bilgisini set ederek basitçe kullancabileceğiniz bir yapıdadır. "BaseAzureBlobStorage" miras almanız yeterli olacaktır.

# PDF Converter

HTML formatında olan sayfalarımızı PDF'e çevirmemizi sağlayacak bir alt yapıdır.

> DinkToPDFConverter
  
DinkToPDF kütüphanesini kullanılır. Oluşturulan PDF'in cloud üzerinde kayıt edilebilmesi için "BaseCloudDinkToPDFConverter" miras almanız gerekmektedir.
Sizden "IUploadAsync" vermenizi talep edecek, sizde bu methodu override ederken cloudFile yaptığınız somut nesneyi vereceksiniz.

Fiziksel bir dosya yoluna kayıt etmek isterseniz eğer "BasePhysicalDinkToPDFConverter" miras almanız size yeterli olacaktır.

> SelectPDFConverter

SelectPDF kütüphanesini kullanılır. Oluşturulan PDF'in cloud üzerinde kayıt edilebilmesi için "BaseCloudSelectPDFConverter" miras almanız gerekmektedir.
Sizden "IUploadAsync" vermenizi talep edecek, sizde bu methodu override ederken cloudFile yaptığınız somut nesneyi vereceksiniz.

Fiziksel bir dosya yoluna kayıt etmek isterseniz eğer "BasePhysicalSelectPDFConverter" miras almanız size yeterli olacaktır.

# Encryption
  Url, kullanıcı şifresi v.b değerleri şifreleme alt yapısıdır.

# Encryption.Cipher
Şifreleme işlemi yaparken bir string değişken kulanırız. Ancak bununla beraber "X" değeri ile şifrele dediğimizde bizim için daha güvenilir bir şifrelme olacaktır.

> SHA256

"BaseSHA256CipherEncryption" miras almanız Decrypt,Encrypt methodlarını kullanmanıza yeterli olacaktır.






