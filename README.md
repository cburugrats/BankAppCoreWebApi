## Rugrat Web Api

### Controllers

[UserController](#UserController)

[HgsController](#HgsController)

### UserController
----

[Get User By UserId](#Get-User-By-UserId)

[Get UpdateUser By TcIdentityKey](#Get-UpdateUser-By-TcIdentityKey)

[Update User and Customer Information By TcIdentityKey](#Update-User-and-Customer-Information)

**Get User By UserId**

  Returns json data about a single user.

* **URL**

  api/user/:id

* **Method:**

  `GET`
  
*  **URL Params**

   **Required:**
 
   `id=[integer]`

* **Data Params**

  None

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{"id":2,"tcIdentityKey":12345678901,"customerId":2,"userName":"12345678901","userPassword":"1","createdDate":"2019-10-23T05:49:10.587","updatedDate":"2019-10-23T05:49:10.587"}`
 
* **Error Response:**

  * **Code:** 404 NOT FOUND <br />

[Back to Top](#Controllers)



**Get UpdateUser By TcIdentityKey**

  Returns json data about a single updateUser.

* **URL**

  api/user/getUpadateUser:TcIdentityKey

* **Method:**

  `GET`
  
*  **URL Params**

   **Required:**
 
   `TcIdentityKey=[integer]`

* **Data Params**

  None

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
    "tcIdentityKey": 11111111111,
    "userName": "11111111111",
    "userPassword": "1",
    "firstname": "Muhammet",
    "surname": "KARABULAK",
    "dateOfBirth": "1997-01-01T00:00:00",
    "phoneNumber": 1111111111,
    "eMail": "sait.krblk@gmail.com"
}`
 
* **Error Response:**

  * **Content:** `null` <br />

[Back to Top](#Controllers)

### Update User and Customer Information
  Returns json data about a request.

* **URL**

  api/user/updateUser

* **Method:**

  `PUT`
  
*  **URL Params**

   **Required:**
 
   None

* **Data Params**

   UpdateUserModel

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `1`
 
* **Error Response:**

  * **Content:** `2` Veritabanına kaydedilirken hata oluştu!<br />
  * **Content:** `3` Bu TC'ye kayıtlı kullanıcı bulunamadı!<br />
  
[Back to Top](#Controllers) 


### HgsController
----

Base api url= <a href="https://bankappcorewebapirugrats.azurewebsites.net/api/">https://rugratswebapi.azurewebsites.net/api<a>

[Register Hgs User](#Register-Hgs-User)

[Get Hgs User By HgsNo](#Get-Hgs-User-By-HgsNo)

[To Deposit Money Hgs](#To-Deposit-Money-Hgs)

### Register Hgs User

  Returns json data about a request.

* **URL**

  api/hgs

* **Method:**

  `POST`
  
*  **URL Params**

   **Required:**
 
   None

* **Data Params**

   `accountNo=[string]`  //Paranın çekileceği hesap no.
   
   `balance=[decimal]`
 

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{HgsNo}= [integer]` //Eğer kayıt başarılı ise geriye kayıtta oluşan yeni HgsNo döndürülür.
    //Hgs No 1000'den başlar ve her yeni kayıtta birer birer artar. Bu işlem serviste otomatik yapılır.
 
* **Error Response:**

  * **Content:** `0` CustomerId boş bırakılamaz!<br />
  * **Content:** `2` Balance boş bırakılamaz!<br />
  * **Content:** `3` Geçersiz bir para miktarı girdiniz!<br />
  * **Content:** `4` Veritabanına kaydedilirken hata oluştu!<br />
  * **Content:** `5` Paranın çekileceği hesapta yeterli bakiye yok!<br />
  * **Content:** `6` AccountNo^ya kayıtlı bir hesap bulunamadı!<br />
  

[Back to Top](#Controllers)

### Get Hgs User By HgsNo

  Returns json data about a single user.

* **URL**

  api/hgs/user/:HgsNo

* **Method:**

  `GET`
  
*  **URL Params**

   **Required:**
 
   `HgsNo=[integer]`

* **Data Params**

    None
 

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{"id":2,"HgsNo":1003,"balance":83.0000}`
 
* **Error Response:**

  * **Content:** `null` <br />
  
  
[Back to Top](#Controllers)

### To Deposit Money Hgs

  Returns json data about a request.

* **URL**

  api/hgs/toDepositMoney

* **Method:**

  `PUT`
  
*  **URL Params**

   **Required:**
 
   None

* **Data Params**

   `accountNo=[string]` //Paranın çekileceği hesap
   
   `balance=[decimal]`
   
   `HgsNo=[integer]`

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `1`
 
* **Error Response:**

  * **Content:** `0` CustomerId boş bırakılamaz!<br />
  * **Content:** `2` Bu customerId'ye bağlı bir hgs kaydı bulunamadı!<br />
  * **Content:** `3` Geçersiz bir para miktarı girdiniz!<br />
  * **Content:** `4` Veritabanına kaydedilirken hata oluştu!<br />  
  * **Content:** `5` Paranın çekileceği hesapta yeterli bakiye yok!<br />
  * **Content:** `6` AccountNo^ya kayıtlı bir hesap bulunamadı!<br />
  
[Back to Top](#Controllers) 
