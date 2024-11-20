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
In Visual Studio, we opened the Package Manager Console in the Other Windows option on the View menu and started the lesson. (Starting layer of application must be Presentation Layer) It's a console screen where can you write migration commands that can be used like Windows Cmd and you can also install packages. In the default project section of this console screen, data access layer must be selected because our Context class is in data access layer. In .Net projects, migrations command, After the process is finished, we need to make AutomaticMigrationsEnabled = false to true on the screen that opens. Then go back to the console and write the update-database command this time. Running seed expression we understand that our command is successfully completed without any error. If it's checked via SQL, we can see that the tables are coming. For example, you forget to add Status to Customer table. After going to the Customer class and assigning the 'public bool CustomerStatus {get; set;}' property, all we have to do is add a new migration to the Package Manager as add-migraion mig1. The migration 
command we created a new class in the data access layer. When we enter this class, we encounter 2 methods as Up and Down. If you do Update, the exporession in the UP method will work. If you give up this migration and wanna do undo the process, the expression in the DOWN method will work. We write the update-database expression again in console. The repository design pattern we will use for help us to centralise CRUD operations and instead of writing it over and over again each entity, it provides a common structure and we'll proceed through it. We created a new interface called IGenericDal in the Abstract folder in interface format and edited it as 'public interface IGenericDal<T> where T:class'. This expression will take a T value from the outside and this T value allows us to condition it to work only the classes. Then we created a method called Insert and specified it to take an entity parameter with a value type T. (void Insert(T entity)) Then we created an Update method and specified it to take an entity parameter with a value of T in the same way. Unlike others, we specified the Delete method to receive id information in the int value of this parameter. Then List<T> GetAll(); we created a method to retrieve all data and finally we defined a method in the form of T GetById(int id); and we created total of 5 methods in the IGenericDal interface. You may only want to perform operations within Categories. In this case, the structure that will come into play will be the structure where entity-specific methods are written. To do this, each entity needs to be communicate with IGenericDal one by one. For this, we created a new interface called ICategoryDal. We made this interface public and made the operations in the IGenericDal interface ready fot the Category class by adding the :IGenericDal<Category> statement at the end. We did same for the other classes. We created interfaces as IProductDal, IAdminDal, ICustomerDal, IOrderDal and completed the 3rd lession of module 301.

**Notes**

**Migration:** In programming, migration is an expression that means migration. There will be an approach where we will create database and migrate value from Visual Studio to SQL. Visual Studio will act as a bridge to create and migrate databases to SQL. In the Code First approach, our goal is to develop everything step by step form scratch.

**Introduction Abstract Folder:** Abstract folder will hold our interfaces. In the interfaces, the operations such as add, delete, update, list, get by id, which are standard in all entites, will proceed in exactly same format and only entity itself will change. Therefore, a desing pattern called Repository Design Pattern will be used in our project.

**Design Patterns:** Typical solutions to common problems in software design. They are like pre-made blueprints that you can customize to solve a recurrong design problem in your code, but you can't just find a pattern and copy it into your program, as you can do with off-the-shelf functions or libraries. A pattern is not a specific piece of code, but a general concept for solving a specific problem. Patterns are often confused with algorithms, because both concepts describe typical solutions to some known problem. An algorithm always describes a clear set of action that can achieve a goal, while a design pattern model is a higher-level description of a solution. The code for the same pattern applied to two different programmees may be different.

## 📌 Episode 14: Orm Structure: Create Model with DbFirst in Entity Framework
This time we started to learn how to use the Database First method instead of Code First on entity framework. Instead of creating all the tables and columns applied in the Code First method with code and transferring them to SQL, we first created the database and tables via SQL and then transferred them to the Visual Studio programme with entity framework. First of all, we created a new form application. Then we created a new database named EgitimKampiEFTravelDb via MSSQL. We created 3 tables in this table as Customer, Location and Guide. After we finished the database, we went to back to Visual Studio and created a new item in the project and this time we clicked on the Data option in the C# Items list and selected the ADO.NET Entity Data Model there. Then, since we will proceed as DbFirst in the window that opens, we selected the first option EF Designer from Database and proceeded. Then we clicked New Connection on the screen that appeared, pasted the Server Name field from the MSSQL app in the server section and activated the Trusted Certificate option below (if we don't activate this option, we can't resume) and in the Select or enter a database name section, we found and added the EgitimKampiEFTravelDb database we created for this project and continued. Then we selected the entity framework 6.x option and continued. Finally. we activated the Tables option on the screen that appeared and pressed Finish button. Thanks to these operations, we have created the same classes that we created with the Code First method in the previous sections with entity framework. The project will be contiuned in the next lesson.

**Notes**

**SysDiagrams Table:** It's a system table created by SQL Server Management Studio that holds database diagrams, relationships between tables (primary key, foreign key, unique key etc.) and visualtions (rows, columns, lines etc.). This table contains only metadata about the diagrams, not business data (customerId, GuideName, etc.).


## 📌 Episode 15: Project Implementation with Entity Framework
We started this lesson by learning how to update the changes made on the tables we created in the last lesson on Visual Studio. We added a column called Balance to the Customer table and created a new table called Admin. Then, on the diagram page named Model1.edmx in the Visual Studio app, we right-clicked on an empty area and selected Update Model From Database option and activated the Tables option in the window that opened, said finish and successfully brought the changes made on the SQL side to our application with a single click. Then we manually entered 5 new data in the Guide table. We added this data for using in the form application. By comning to the Form1.cs(Design) part of our project, we added 3 textboxes to write Guide Id, Name and Surname to our application, 5 buttons in total as List, Add, Delete, Update and Get by Id at the bottom of them and finally we added a data grid view on the right side of page where the data to be pulled from database will be displayed and we edited the names we will use on the coding of the buttons and textboxes (such as btnListele, txtName, etc.). Then we moved to the code section and wrote the codes of the buttons that will enable the desired function to be fulfilled and finished this lesson. I have specified the button codes and explanations written below. 

**Codes**

public partial class Form1 : Form
{
  public Form1()
  {
    InitializeComponent();
  }

  EgitimKampiEFTravelDbEntities db = new EgitimKampiEFTravelDbEntities(); **//In this section, we referenced the Class created in the previous lesson and assigned this information to the db variable.**
  
  private void btnList_Click(object sender, EventArgs e) **//This form is the method of the List button we created in our application.**
  {
    var values = db.Guide.ToList(); **//With this code, we created a local variable named values and called the ToList method that transfers the information in the Guide class in db to this variable. ToListMethod is a method in the Entity Framework.**
    dataGridView1.DataSource = values; **//With this code, we assigned the values variable that holds the data we added with ToList to the DataSource of the datagridview we created in the form section, that is, the part that holds the information in it.**
  }

  private void btnAdd_Click(object sender, EventArgs e) **//This form is the method of the Add button we created in our application.**
  {
   Guide guide = new Guide(); **//Here we created a small guide object from the Guide class.**
   guide.Name = txtName.Text; **//We transferred the text of the information entered in the textBox to the Name information in the guide we created here.**
   guide.Surname = txtSurname.Text; **//Here we did the same operation for Surname as we did for Name.**
   db.Guide.Add(guide); **//Here, we sent the guide object to the Guide class in the db using the Add method and created and entered the name and surname data into it.**
   db.SaveChanges(); **//This code fragment allows us to save the operations done and display them in the datagridview when we say list again.**
   MessageBox.Show(‘Contacts Added Successfully!’, ‘Warning’, MessageBoxButtons.OK, MessageBoxIcon.Warning); **//This piece of code is used to create an information message.**
  }

  private void btnDelete_Click(object sender, EventArgs e) **//This form is the method of the Delete button we created in our application.**
  {
   int id = int.Parse(txtId.Text); **//Here, it creates a local variable named id and converts the value entered in the Id textbox into int and assigns it.**
   var removeValue = db.Guide.Find(id); **//Here, a variable named removeValue is defined to hold the data to be deleted, and the id information we created in the Guide class in the db using the Find method is sent and assigned as a parameter.**
   db.Guide.Remove(removeValue); **//The code fragment here is added as a parameter to the Remove method, which deletes the removeValue variable we have assigned.**
   db.SaveChanges(); **//This code fragment ensures that the operations performed are saved and we can view them in the datagridview when we say list again.**
   MessageBox.Show(‘Contacts Deleted Successfully!’, ‘Warning’, MessageBoxButtons.OK, MessageBoxIcon.Warning); **//This piece of code is used to create an information message.**
  }

  private void btnUpdate_Click(object sender, EventArgs e) **//This form is the method of the Update button we created in our application.**
  {
   int id = int.Parse(txtId.Text); **//The same id operation is used again.**
   var updateValue = db.Guide.Find(id); **//Here, the variable that will hold the data to be updated called updateValue is defined and the id information we created in the Guide class in the db using the Find method is sent and assigned as a parameter.**
   updateValue.Name = txtName.Text; **//This piece of code transmits the name information in the data assigned in the name textbox.**
   updateValue.Surname = txtSurname.Text; **//In this code fragment, the surname information in the assigned data transmits the data entered in the surname textbox.**
   db.SaveChanges();
   MessageBox.Show(‘Directory Updated Successfully!’, ‘Warning’, MessageBoxButtons.OK, MessageBoxIcon.Warning);
  }

  private void btnGetById_Click(object sender, EventArgs e) **//This form is the method of the Get By Id button we created in our application.**
  {
   int id = int.Parse(txtId.Text); **//The same id operation is used again.**
   var values = db.Guide.Where(x => x.GuideId == id).ToList(); **//Here the lambda expression in Entity Framework is used. On each row (x), it is queried whether the GuideId value is equal to id and the ToList method is used. The information obtained from the query is                                                                     defined in the variable named values.**
   dataGridView1.DataSource = values; **//This piece of code defines the values variable to the DataSource part of the datagridview, that is, the place that holds the information in it.**
  }
  
}


## 📌 Episode 16: Entity Framework: Location Operations For The Tour Project


