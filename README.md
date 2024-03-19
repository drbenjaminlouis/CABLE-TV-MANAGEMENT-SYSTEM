# CABLE TV MANAGEMENT SYSTEM (CTVMS)
## ABOUT THIS APPLICATION
CABLE TV MANAGEMENT SYSTEM (CTVMS) is an application for cable tv operators to manage their customers. This Application provide option for login to both operator (admin) and customers. It helps admin to manage both cable tv and broadband connection.

There are a wide range of features to increase the productivity. Through this application both admin can collect or customer can make payment. Customers don't want need to go to office for making payments. They can login to their account and pay.

Admin have an option for sending reminders to customers who have due amounts. The major advantage of this system is, both admin and customer can keep track of the payment details. Admin can generate payment report for both cable tv and broadband connection separately

Customers can edit some of their details meanwhile admin have full access to edit. Only admin is allowed to add new customers. After successful adding of new customer, a welcome email consisting the login credentials is automatically shared to the customer's email id. It also provides dashboard to both admin and customer showing the overall details.
## OBJECTIVES

- Effective management of the customers of a cable tv network.

- To keep track of the payments.

- To manage different types of connections such as, cable tv and broadband.

- To allow customers also to make payment without coming to office.

- To reduce paper works.

- To increase productivity.

- To avoid human errors.

- To keep the customer details safely.

- To make use of advanced technologies.

- To generate reports automatically.

- To keep invoices digitally

## MODULES IN THE PROJECT

- Admin dashboard

- Customer dashboard

- Customer management

- Payment collection

- Customer details

- Payment details

- Reminder sender

- Complaints

- Report generator

- Invoice viewer

- Year updater

- Status updater

- Email Templates

## MODULE DESCRIPTION

### ADMIN DASHBOARD:

This module consists of the overall details about the system. Through this module admin can view the count of customers categorized based on their connection status. This includes total active customers, inactive customers and suspended customers of both cable tv and broadband separately. It also displays the number of renewals for present day and received and pending payments of present month for both broadband and cable tv.

### CUSTOMER DASHBOARD:

This module consists of the overall details about the customer who login. Through this module customers can view their connection status, plan expiry date, due amount, current plan for both broadband and cable tv.

### CUSTOMER MANAGEMENT:

This module consists of the functionalities for adding new customer, removing existing customer and modifying customer details. Only admin is allowed to add and remove customer. Customer have partial authority for modifying their details and admin have full authority.

### PAYMENT COLLECTION:

This module is used for payment collection. Both admin and customer can make payment. Admin is allowed to collect payment as cash or via online mode. Customer is allowed to make only online payment. Reference number should be entered for online payments. After successful payment an invoice is generated automatically. This invoice can be print also. Also, after successful payment an email will be send automatically to customer’s email id stating the confirmation of payment with a copy of invoice generated. The minimum payment amount is set to 250 and maximum up to the due amount.

### CUSTOMER DETAILS:

This module is viewing customer details. Both admin and customer can use this functionality. Admin is allowed to view all customer details and customer is allowed to view only their respective details.

Admin can search for particular customer based on CRF, customer name or mobile number.

### PAYMENT DETAILS:

This module is available for both admin and customer. This consists the functionalities for viewing details of the payment done. Admin can view customer’s payment details by entering their CRF number and selecting year. For customers, they need to select only year for viewing their respective payment details. Through this module both admin and customer can view all the past payment details.

### REMINDER SENDER:

This module provides the functionalities to admin for sending a reminder to customer who have due amounts. The reminder is sent to the user via using smtp server of Gmail as an email. This email contains the name of the customer, due amount and pending months.

### COMPLAINTS:

This module allows the customers to raise complaints related to the service provided and the admin to view those complaints. When customer raise a complaint, an auto generated email is sent to their email id with complaint number. Admin can mark the complaint as resolved after fixing the issue. When the complaint is resolved, an auto generated email is send to customer’s email id stating issue is resolved.

### REPORT GENERATOR:

This module gives the functionalities to admin for generating report of payments for both cable tv and broadband. Admin needs to select year, month, sort type and file type before generating report. Currently report can be generated either as PDF or EXCEL file based on the selected file type. Sorting is of two types; paid and not paid.

### INVOICE VIEWER:

This module is available for customers only. Through this module, customer is allowed to view the past invoices. Customers need to select year and month, and based on the selected year and month, transactions made on that particular month is displayed. By clicking the transaction number, they can view the invoice. If file is deleted or any error occurred a custom webpage will be displayed. They are allowed to download or print the invoice they selected.

### YEAR UPDATER:

This module consists the function for updating the database for storing payment details when year changes. This function creates rows in payment details table with required data. This function is executed after checking if the month value and day value is 1 or not. If its true then function will be executed.

### STATUS UPDATER:

This module has the functions for updating the connection status of customers based on their plan expiry date. When the expiry date is over the customer status is updated to inactive state. If the expiry date is less than 60 days from current date, status will be updated to suspended. When the expiry date greater than current date, status will be updated to active state. This applicable for both services.

### EMAIL TEMPLATES:

This module consists of different email functions used for sending mail in different occasions. Different templates are credentials sender, OTP sender, invoice sender, reminder sender, complaint receive confirmation, complaint resolver. Credentials sender send welcome message to customer consist of the login credentials. OTP sender is the function to send email to customers while they trying to change password. Invoice sender is the function which send email to customer after successful payment with a copy of invoice. Reminder sender is the function used to notify customers who have due amount. Complaint raised confirmation and complaint resolver are the functions used while customer raising a complaint and admin mark complaints as resolved respectively.

## SOFTWARE REQUIREMENTS

|	|  |
|--|--|
|FRONT-END|VISUAL STUDIO 2022|
|BACK-END  | MS ACCESS 2016 |
|OPERATING SYSTEM| WINDOWS 11|

## Contact Information
For inquiries or assistance with this project, please contact:

- Email: abyjose377@gmail.com
- LinkedIn: https://www.linkedin.com/in/abyjose/

## HOW TO CONFIGURE ?

Download the configuration file from [HERE](https://drive.google.com/file/d/1gpJTteHDgk-d2rXOw9ME2_xBn2aFPRdK/view?usp=sharing)  and add the correct path details of all required files. You can open it in any text editor and edit the file path accordingly. After making changes save the file inside the project folder. Don't change the file name.

You can download the ms access database file from [HERE](https://drive.google.com/file/d/1fOcdSM6v0lvyPCpxwNL1YxHggQD8ZDjQ/view?usp=sharing). Download the file and save it inside the app_data folder inside the project folder. Don't forget to add the database path to Config.vb File.

For the proper functioning of Auto Email add your own smtp userid and password in the fields smtpID and smtpPASS on config.vb file.

## FORM DESIGNS

### ADMIN LOGIN:


![ADMIN LOGIN](https://user-images.githubusercontent.com/64739511/228830496-503036d8-0a18-4185-9091-65f53665bb5a.png)


### ADMIN DASHBOARD:


![ADMIN DASHBOARD](https://user-images.githubusercontent.com/64739511/228830588-82ca0ff5-e097-49c1-80c5-f83f44a80d8c.png)


### CHANGE PASSWORD – ADMIN:


![CHANGE PASSWORD - ADMIN](https://user-images.githubusercontent.com/64739511/228830846-19ed80ad-12af-497d-83ee-1e8073386b00.png)


### COLLECT PAYMENT – ADMIN:


![ADMIN - COLLECT PAYMENT](https://user-images.githubusercontent.com/64739511/228830912-c0a27473-7577-4543-a6b7-e5c703a5d6ec.png)


### ADD CUSTOMER:


![ADD CUSTOMER](https://user-images.githubusercontent.com/64739511/228830988-d83a8bad-1d5b-4b46-bda5-7570dd99ae92.png)


### REMOVE CUSTOMER:


![REMOVE CUSTOMER](https://user-images.githubusercontent.com/64739511/228831072-4cf6955a-f714-48ce-a966-a314e178b5bf.png)


### CUSTOMER DETAILS:


![CUSTOMER DETAILS](https://user-images.githubusercontent.com/64739511/228831544-79e499fd-d6b1-41a4-8b46-134c632664c8.png)


### EDIT CUSTOMER:


![EDIT CUSTOMER](https://user-images.githubusercontent.com/64739511/228831604-b5a02669-549d-4f5f-a151-84414ef9fe49.png)


### PAYMENT DETAILS:


![PAYMENT DETAILS - ADMIN](https://user-images.githubusercontent.com/64739511/228831667-506e0580-2a24-40f6-99ec-108c97c95dbc.png)


### REMINDER SENDER:


![REMINDER SENDER](https://user-images.githubusercontent.com/64739511/228831749-0e15e9d6-78c0-44d9-b199-065a1ffb6292.png)


### COMPLAINTS VIEWER:


![COMPLAINT VIEWER](https://user-images.githubusercontent.com/64739511/228831803-39173c65-4ce1-4812-aadd-7b9a030fbf5e.png)


### TV PAYMENT REPORT:


![TV PAYMENT REPORT](https://user-images.githubusercontent.com/64739511/228831902-cb9e17ef-57a5-4354-97f6-2055189d055c.png)


### BROADBAND PAYMENT REPORT:


![BROADBAND PAYMENT REPORT](https://user-images.githubusercontent.com/64739511/228832021-5accc188-e9f3-4958-b3cc-00b2517baaa5.png)


### CUSTOMER LOGIN:


![CUSTOMER LOGIN](https://user-images.githubusercontent.com/64739511/228832110-3b417727-2a5e-4aa0-881b-364de4f1484b.png)


### CUSTOMER DASHBOARD:


![CUSTOMER DASHBOARD](https://user-images.githubusercontent.com/64739511/228832168-f76bb31d-0568-48aa-8f03-2f62db823bd8.png)


### CHANGE PASSWORD - CUSTOMER:


![CHANGE PASSWORD - CUSTOMER](https://user-images.githubusercontent.com/64739511/228832219-db947b25-41b5-478e-9377-1721f08bc9e6.png)


### MAKE PAYMENT:


![MAKE PAYMENT](https://user-images.githubusercontent.com/64739511/228832304-d0d9550f-1ba4-4149-a1c1-a1e8078698bc.png)


### COMPLAINT RAISER:


![COMPLAINT RAISER](https://user-images.githubusercontent.com/64739511/228832382-d2fc668c-ff4f-49e2-ac82-5e9ba8815f31.png)


### VIEW DETAILS – CUSTOMER


![VIEW DETAILS - CUSTOMER](https://user-images.githubusercontent.com/64739511/228832429-237557c3-782d-443e-a7fe-e38148a6a5f4.png)


### EDIT DETAILS – CUSTOMER:


![EDIT DETAILS - CUSTOMER](https://user-images.githubusercontent.com/64739511/228832506-9e19d30a-a2e2-4fa2-a22a-9d31f12a6cbb.png)


### PAYMENT DETAILS – CUSTOMER:


![PAYMENT DETAILS - CUSTOMER](https://user-images.githubusercontent.com/64739511/228832550-a0a4885b-eb61-4b07-b017-f8076f8a552e.png)


### TV PAYMENT INVOICE VIEWER:


![TV INVOICE VIEWER](https://user-images.githubusercontent.com/64739511/228832628-bcb143c9-508a-4a5c-8ecd-9abc066661e7.png)


### BROADBAND PAYMENT INVOICE VIEWER:


![BROADBAND INVOICE VIEWER](https://user-images.githubusercontent.com/64739511/228832668-65090de9-a853-452b-a64d-8603b11ae7fb.png)


