# C# BOOTCAMP SECTION 301
**This repository is based on C# Bootcamp published by Murat Yücedağ on Youtube and is a continuation of the previous 101-201 repository. You can browse the details covered in each section from the bottom.**

## 📌 Episode 11: Module OOP: N Architecture Entity Layer with C#
In the beginning of the chapter 301, we started by creating a new solution. Created Entity, DataAccess, Business and Presentation/UI layers that we will use in our projects. We created a folder called Concrete for the EntityLayer class. This Contrete usually contains concrete, that is, classes. We created tables over C# without using SQL and this process is called Code First. Normally a database table created on SQL is transferred to C# via query, while in this method a class is created (the name of the class must be the name of the table) and this created class is transferred to SQL as a table. The properties created in the class represent the columns. BusinessLayer, DataAccessLayer, EntityLayer and PresentationLayer are created and a solution is prepared for the next lesson.

**Notes**

**Access Modifiers:** 1-) Public: Accessible from all the classes under solution. 2-) Private: Accessible from only its own class. 3-) Internal: Accessible only from the layer it's in. 4-) Protected: Accessible only from classes that inherited class.

**Field - Variable - Property:** 1-) Field: Variables defined directly in the class. 2-) Variable: Variables that are defined not directly in the class but, in a method in the class. 3-) Property: When the variable structure receives get; set; values at the end, it becomes property. If you want to read the value of this variable, GET method works. If you want to assign a value of the variable, SET method works.

**Code First:** In the Code First approach, name of the class must be the same and Id must be written at the en in order to recognise that the ID value is the primary key and auto-incrementing. (CategoryId, ProductId etc.)

## 📌 Episode 12: Module OOP: Data Access Layer and Context Class
Each product must have a category. A property named CategoryId was created for the Product class in the project and an object named 'Category' was created from the Category class as public virtual Category Category Category {get; set; } in order to access the values in the Category table through the product. We also need to inform the Category table from the Product class. ‘Public List<Product> Products {get; set; }’ property was created in Category class. The reason why the Products object in the Category class is created as plural and the Category object in the Product class is created as singular is that while any product can have only one category, there can be more than one product in a category. This format is called a one-to-many relationship in Code First. In the Order class, relationships were established in the same way. There must be a product and a customer in the order. Therefore, ProductId, Product object from Product class, CustomerId and Customer object from Customer class are defined as properties in Order class. Then the ‘Public List<Order> Orders {get; set; }’ property was created in the Product class and in the same way, the ‘Public List<Order> Orders {get; set; }’ property was defined in the Customer class and the one-to-many format was applied. Entity framework package was installed on the DataAccess class via Visual Studio and entered the Referance section and added the entity layer as a reference there. Thus, the packages in entity layer were added to the DataAccess layer as a reference. Then a new folder called context was created in the data access class. A new class called CampContext created in the context folder and we assigned the DbContext class in the package installed in the entity class as a reference to this class. (public class CampContext:DbContext) In the class, we defined the tables in the entity layer as 'public DbSet<Category> Categories {get; set;}'. After defining the other classes, we added the data access class as a reference to the business layer as a reference to presentation layer. Then we installed the entity framework package to give the database connection address in the AppConfig file in the presentation layer. The connection address was defined by writing "<connectionStrings> <add name=’CampContext‘ connectionString=’Data Source=Server Name in MSSQL application; initial Catalog=Name of the database to be added; integrated Security=true;‘ providerName=’System.Data.SqlClient"> </connectionStrings>" and the lesson was completed. To prepare for the next lesson, folders named Abstract, EntityFramework and Repositories were created in the DataAccess layer.

**Notes**

**Layer Ordering:** About layers and the reference relationships between them: Layers refer to each other. The entity layer is referenced to the data access layer, data access layer to the business layer and business layer to the presentation layer. At the top is the presentation layer, below that the business layer, below that the data access layer and at the bottom layer is entity layer. Thus, each layer communicates with each other, but the user doesn't have to see all the layers.

**Context:** In Code First projects, it acts as a class where the database connection address is located and the tables that will be reflected in the database are kept.

**Context and Database Relationship:** If we want to reflect a class to the database, we need to write that class in CampContext as a table. DbSet<Category> is the name of the class to be used on out C# side. Categories is the table name that will be reflected in SQL. The equivalent of this in programming is pluraise. In order for the class and the table not to be interwined with each other, it should be written as a class on C#, and the plural form should be written as Table.

## 📌 Episode 13: Module OOP: Migration operations and Abstract Interfaces



## 📌 Episode 14: Orm Structure: Create Model with DbFirst in Entity Framework



## 📌 Episode 15: Project Implementation with Entity Framework


## 📌 Episode 16: Entity Framework: Location Operations For The Tour Project


