# C# BOOTCAMP SECTION 301
**This repository is based on C# Bootcamp published by Murat Yücedağ on Youtube and is a continuation of the previous 101-201 repository. You can browse the details covered in each section from the bottom.**

## 📌 Episode 11: Module OOP: N Architecture Entity Layer with C#
In the beginning of the chapter 301, we started by creating a new solution. Created **Entity, DataAccess, Business and Presentation/UI** layers that we will use in our projects. We created a folder called Concrete for the EntityLayer class. This Contrete usually contains concrete, that is, classes. We created tables over C# without using SQL and this process is called **Code First**. Normally a database table created on SQL is transferred to C# via query, while in this method a class is created (the name of the class must be the name of the table) and this created class is transferred to SQL as a table. The properties created in the class represent the columns. **BusinessLayer**, **DataAccessLayer**, **EntityLayer** and **PresentationLayer** are created and a solution is prepared for the next lesson.

**Notes:**

**Access Modifiers:** 1-) Public: Accessible from all the classes under solution. 2-) Private: Accessible from only its own class. 3-) Internal: Accessible only from the layer it's in. 4-) Protected: Accessible only from classes that inherited class.

**Field - Variable - Property:** 1-) Field: Variables defined directly in the class. 2-) Variable: Variables that are defined not directly in the class but, in a method in the class. 3-) Property: When the variable structure receives get; set; values at the end, it becomes property. If you want to read the value of this variable, GET method works. If you want to assign a value of the variable, SET method works.

**Code First:** In the Code First approach, name of the class must be the same and Id must be written at the en in order to recognise that the ID value is the primary key and auto-incrementing. (CategoryId, ProductId etc.)

## 📌 Episode 12: Module OOP: Data Access Layer and Context Class
Each product must have a category. A property named CategoryId was created for the product class in the project and an object named 'Category' was created from the category class as **public virtual Category Category Category {get; set; }** in order to access the values in the category table through the product. We also need to inform the category table from the product class. **‘Public List<Product> Products {get; set; }’** property was created in category class. The reason why the products object in the category class is created as plural and the category object in the product class is created as singular is that while any product can have only one category, there can be more than one product in a category. This format is called a **one-to-many relationship** in **Code First**. In the order class, relationships were established in the same way. 

There must be a product and a customer in the order. Therefore, ProductId, Product object from product class, CustomerId and customer object from customer class are defined as properties in order class. Then the **‘Public List<Order> Orders {get; set; }’** property was created in the product class and in the same way, the **‘Public List<Order> Orders {get; set; }’** property was defined in the customer class and the one-to-many format was applied. **Entity Framework** package was installed on the data access class via Visual Studio and entered the referance section and added the **Entity** layer as a reference there. Thus, the packages in entity layer were added to the **DataAccess** layer as a reference. Then a new folder called context was created in the **Data Access** class. A new class called **CampContext** created in the context folder and we assigned the **DbContext** class in the package installed in the entity class as a reference to this class. (public class CampContext:DbContext) In the class, we defined the tables in the entity layer as **'public DbSet<Category> Categories {get; set;}'**. After defining the other classes, we added the data access class as a reference to the business layer as a reference to **Presentation** layer. 

Then we installed the **Entity Framework** package to give the database connection address in the **AppConfig** file in the **Presentation** layer. The connection address was defined by writing "<connectionStrings> <add name=’CampContext‘ connectionString=’Data Source=Server Name in MSSQL application; initial Catalog=Name of the database to be added; integrated Security=true;‘ providerName=’System.Data.SqlClient"> </connectionStrings>" and the lesson was completed. To prepare for the next lesson, folders named **Abstract**, **EntityFramework** and **Repositories** were created in the **DataAccess** layer.

**Notes:**

**Layer Ordering:** About layers and the reference relationships between them: Layers refer to each other. The entity layer is referenced to the data access layer, data access layer to the business layer and business layer to the presentation layer. At the top is the presentation layer, below that the business layer, below that the data access layer and at the bottom layer is entity layer. Thus, each layer communicates with each other, but the user doesn't have to see all the layers.

**Context:** In Code First projects, it acts as a class where the database connection address is located and the tables that will be reflected in the database are kept.

**Context and Database Relationship:** If we want to reflect a class to the database, we need to write that class in CampContext as a table. DbSet<Category> is the name of the class to be used on out C# side. Categories is the table name that will be reflected in SQL. The equivalent of this in programming is pluraise. In order for the class and the table not to be interwined with each other, it should be written as a class on C#, and the plural form should be written as Table.

## 📌 Episode 13: Module OOP: Migration operations and Abstract Interfaces
In Visual Studio, we opened the Package Manager Console in the Other Windows option on the View menu and started the lesson. (Starting layer of application must be Presentation Layer) It's a console screen where can you write migration commands that can be used like Windows Cmd and you can also install packages. In the default project section of this console screen, data access layer must be selected because our context class is in **Data Access** layer. 

In .Net projects, migrations command, After the process is finished, we need to make **AutomaticMigrationsEnabled = false** to true on the screen that opens. Then go back to the console and write the update-database command this time. Running seed expression we understand that our command is successfully completed without any error. If it's checked via SQL, we can see that the tables are coming. For example, you forget to add status to customer table. After going to the customer class and assigning the **'public bool CustomerStatus {get; set;}'** property, all we have to do is add a new migration to the Package Manager as add-migraion mig1. The migration 
command we created a new class in the **Data Access** layer. 

When we enter this class, we encounter 2 methods as Up and Down. If you do Update, the exporession in the Up method will work. If you give up this migration and wanna do undo the process, the expression in the Down method will work. We write the update-database expression again in console. The repository design pattern we will use for help us to centralise **CRUD** operations and instead of writing it over and over again each entity, it provides a common structure and we'll proceed through it. We created a new interface called IGenericDal in the Abstract folder in interface format and edited it as **'public interface IGenericDal<T> where T:class'**. This expression will take a T value from the outside and this T value allows us to condition it to work only the classes. Then we created a method called Insert and specified it to take an entity parameter with a value type T. (void Insert(T entity)) 

Then we created an Update method and specified it to take an entity parameter with a value of T in the same way. Unlike others, we specified the Delete method to receive id information in the int value of this parameter. Then List<T> GetAll(); we created a method to retrieve all data and finally we defined a method in the form of T GetById(int id); and we created total of 5 methods in the IGenericDal interface. You may only want to perform operations within categories. In this case, the structure that will come into play will be the structure where entity-specific methods are written. To do this, each entity needs to be communicate with IGenericDal one by one. For this, we created a new interface called ICategoryDal. We made this interface public and made the operations in the IGenericDal interface ready fot the category class by adding the :IGenericDal<Category> statement at the end. We did same for the other classes. We created interfaces as IProductDal, IAdminDal, ICustomerDal, IOrderDal and completed the 3rd lession of module 301.

**Notes:**

**Migration:** In programming, migration is an expression that means migration. There will be an approach where we will create database and migrate value from Visual Studio to SQL. Visual Studio will act as a bridge to create and migrate databases to SQL. In the Code First approach, our goal is to develop everything step by step form scratch.

**Introduction Abstract Folder:** Abstract folder will hold our interfaces. In the interfaces, the operations such as add, delete, update, list, get by id, which are standard in all entites, will proceed in exactly same format and only entity itself will change. Therefore, a desing pattern called Repository Design Pattern will be used in our project.

**Design Patterns:** Typical solutions to common problems in software design. They are like pre-made blueprints that you can customize to solve a recurrong design problem in your code, but you can't just find a pattern and copy it into your program, as you can do with off-the-shelf functions or libraries. A pattern is not a specific piece of code, but a general concept for solving a specific problem. Patterns are often confused with algorithms, because both concepts describe typical solutions to some known problem. An algorithm always describes a clear set of action that can achieve a goal, while a design pattern model is a higher-level description of a solution. The code for the same pattern applied to two different programmees may be different.

## 📌 Episode 14: Orm Structure: Create Model with DbFirst in Entity Framework
This time we started to learn how to use the **Database First** method instead of **Code First** on **Entity Framework**. Instead of creating all the tables and columns applied in the **Code First** method with code and transferring them to SQL, we first created the database and tables via SQL and then transferred them to the Visual Studio program with **Entity Framework**. First of all, we created a new form application. Then we created a new database named EgitimKampiEFTravelDb via MSSQL. We created 3 tables in this table as Customer, Location and Guide. After we finished the database, we went to back to Visual Studio and created a new item in the project and this time we clicked on the Data option in the C# Items list and selected the ADO.NET Entity Data Model there. Then, since we will proceed as DbFirst in the window that opens, we selected the first option EF Designer from Database and proceeded. 

Then we clicked New Connection on the screen that appeared, pasted the Server Name field from the MSSQL app in the server section and activated the Trusted Certificate option below (if we don't activate this option, we can't resume) and in the Select or enter a database name section, we found and added the EgitimKampiEFTravelDb database we created for this project and continued. Then we selected the entity framework 6.x option and continued. Finally. we activated the Tables option on the screen that appeared and pressed Finish button. Thanks to these operations, we have created the same classes that we created with the Code First method in the previous sections with entity framework. The project will be contiuned in the next lesson.

**Notes:**

**SysDiagrams Table:** It's a system table created by SQL Server Management Studio that holds database diagrams, relationships between tables (primary key, foreign key, unique key etc.) and visualtions (rows, columns, lines etc.). This table contains only metadata about the diagrams, not business data (customerId, GuideName, etc.).


## 📌 Episode 15: Project Implementation with Entity Framework
We started this episode by learning how to update the changes made on the tables we created in the last lesson on Visual Studio. We added a column called Balance to the customer table and created a new table called admin. Then, on the diagram page named Model1.edmx in the Visual Studio app, we right-clicked on an empty area and selected Update Model From Database option and activated the Tables option in the window that opened, said finish and successfully brought the changes made on the SQL side to our application with a single click. Then we manually entered 5 new data in the guide table. We added this data for using in the form application. By comning to the Form1.cs(Design) part of our project, we added 3 textboxes to write Guide Id, Name and Surname to our application, 5 buttons in total as List, Add, Delete, Update and Get by Id at the bottom of them and finally we added a data grid view on the right side of page where the data to be pulled from database will be displayed and we edited the names we will use on the coding of the buttons and textboxes (such as btnListele, txtName, etc.). Then we moved to the code section and wrote the codes of the buttons that will enable the desired function to be fulfilled and finished this lesson. I have specified the button codes and explanations written below. 

**Codes:**

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
   
    var values = db.Guide.Where(x => x.GuideId == id).ToList(); **//Here the lambda expression in Entity Framework is used. On each row (x), it is queried whether the GuideId value is equal to id and the ToList method is used. The information obtained from the query is defined in the variable named values.**
   
    dataGridView1.DataSource = values; **//This piece of code defines the values variable to the DataSource part of the datagridview, that is, the place that holds the information in it.**
   
    }
  
    }
 

## 📌 Episode 16: Entity Framework: Location Operations For The Tour Project
This episode is a repetition of the previous lesson and we applied the same list, add, delete and update operations performed in the Contacts form the location table.

## 📌 Episode 17: Entity Framework Methods & Linq Querries
In this episode, we have learnt to retrieve and use the data in database in different ways using **Linq** queries. For this, we created a new form in our project. We added a total of 12 sections to this form where data will be transferred. We pulled data from different tables to these sections. For example, in order to bring the name of the guide of the Lyon tour, we first created a variable by lyonGuideId and int this variable we wrote our code to select the city name is Lyon data and get the GuideId information of the selected data and assigned it to this variable. Then in the text of GuideNameLocationLyon label we first selected the data whose GuideId is equal to lyonGuideId and by taking the name and surname values of this data, we ensured that the first incoming data was successfully retrieved using this FirstOrDefault() method in the **Entity Framework**, and finally we converted it to string type with ToString() method.

In addition, i would like to explain how i prevented all locations from creating more that 2 digits after the comma in the output of the average price information requested by teacher of this bootcamp is **Murat Yücedağ**. (I got helped ChatGpt in this quest) First of all, i created a new variable named avgPrice in decimal type and averaged Price data bu using Average() method to this variable, but differently i wrote the (decimal) expression at the beginning of the code, indicating that this incoming data must be in decimal type. (decimal avgPrice = (decimal)db.Location.Average(x => x.Price);) Then, in the text part of the AvgLocationPrice label, i converted the avgPrice variable to both string format as ToString("F2") in it. In this way, incoming average information is written like 120.00 format. You can take a look at the details of this data in the picture below.

![image alt](https://github.com/berkaypehllivan/CSharpEgitimKampi301/blob/master/Ekran%20g%C3%B6r%C3%BCnt%C3%BCs%C3%BC%202024-11-23%20163357.png?raw=true)

## 📌 Episode 18 - EntityState Commands, Generic Repository Class and Ef Classes
In this episode, we started by creating a new interface in the **DataAccess** class called **Generic Repository**. This class will be take a T value when called from outside and will inherit from IGenericDal. The T value mus come from a class. Next, we used the Implement Interface method to create the methods in the IGenericDal interface for our Generic Repository interface. We created a new object named context from a previously created class called CampContext. We also created a field named _object, which us of type DbSet and takes a T value.

The purpose was to combine the _object field and the context object when the generic repository class is called. To achieve this, we wrote ctor and pressed the Tab key to create a method named Public GenericRepository. Inside this method, we assigned the value of _object as _object = context.Set<T>(); This way, when the generic repository ckass is called, it creates an instance of _object and assigns an Entity value (like Admin, Product, Category or Order) from the context class to it. After that, we filled in the necessary functions for the methods that came with the implementation and finished our work with the Generic Repository. 
These classes will create the values for **CRUD** operations using entities in the **Generic Repository**. All CRUD operations will communicate through these classes.

## 📌 Episode 19 - Business Layer and Logic Rules
In this episode , we started working on the Business Layer. Inside the Business Layer, we created a folder named Abstract. In this folder, we added a new interface called IGenericService, which takes a T value ensures that this T value must come from a class. Next, we copied the methods from IGenericDal and pasted them into the new created interface. To avoid confusion, we added the letter T at the beginning of each methods name. From the **Presentation Layer**, we will call methods that start with the letter T. This way, instead of directly accessing the **Data Access** layer, we will use **Business** layer as a bridge, ensuring compliance with the architecture. 

For each entity, we create a new interface and made these interfaces inherited from **IGenericService** for their specific classes. (For example, ICategoryService : IGenericService<Category>). We then created a new folder named **Concrete**. In this folder, we cereated classes for the each entities by adding **Manager** to their names (CategoryManager) and made them inherit their corresponding interfaces. To perform CRUD operations in these classes, we needed to call the realted structure from the **Data Access** layer, which is directly connected. This method is called **Dependency Injection** (topic of the next episode). In the **Business** layer, we will be use these classes to define the validation rules for the entities.

**Example Method:**


    public void TInsert(Customer entity)
    {
	    if (entity.CustomerName != "" && entity.CustomerName.Length >= 3 && entity.CustomerCity != null)
	    {
		    Ekleme işlemini gerçekleştir
	    }
	    else
	    {
		    Hata mesajı ver	
	    }
    }

For practical purposes, we created a simple form application named FrmCategory based on the category entity. The form has content like the following codes. This is how we finished the lesson.

**Codes:**

    public partial class FrmCategory : Form
    {
        private readonly ICategoryService _categoryService;

        public FrmCategory()
        {
            _categoryService = new CategoryManager(new EfCategoryDal());
            InitializeComponent();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            var categoryValues = _categoryService.TGetAll();
            dgvCategory.DataSource = categoryValues;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Category category = new Category();
            category.CategoryName = txtCategoryName.Text;
            category.CategoryStatus = true;
            _categoryService.TInsert(category);
            MessageBox.Show("Ekleme işlemi başarılı");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCategoryId.Text);
            var deletedValue = _categoryService.TGetById(id);
            _categoryService.TDelete(deletedValue);
            MessageBox.Show("Silme işlemi başarılı");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int updatedId = int.Parse(txtCategoryId.Text);
            var updatedValue = _categoryService.TGetById(updatedId);
            updatedValue.CategoryName = txtCategoryName.Text;
            updatedValue.CategoryStatus = true;
            _categoryService.TUpdate(updatedValue);
        }

        private void btnGetById_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtCategoryId.Text);
            var values = _categoryService.TGetById(id);
            dgvCategory.DataSource = values;
        }
    }

## 📌 Episode 20 - Dependency Injection
**Dependency Injection** is based on the principle of providing (injecting) the objects that an object needs from the outside instead of creating them inside itself. This reduces dependencies between projects, increases testability and makes the code more modular.

**Constructor Methods** are special methods that are initiated when an object is created. They are typically used to set the initial state of the object. When used with **Dependency Injection**, constructor methods take rhe dependencies of the object as parameters. In this way, dependencies are injected when the object is created.

In summary, **Dependency Injection** and **Constructor Methods** are important concepts in modern software development that improve code quality. They help in creating applications that are easier to maintain, test and extend.

We finished the theoretical part of the episode and started applying the topics of constructors and dependency injection. In the OrderManager class, we created a field like **private readonly IOrderDal _orderDal**. Then we pressed Alt + Enter and selected the generate constructor option from the list. Visual Studio automatically prepared the constructor method we needed. 

Afterward, we coded the required functions for the methods we implemented. We repeated these steps for the category, customer and product entities by creating their respective Manager classes. The main goal is this part of the episode was to avoid direct access from the **Presentation(UI)** layer to **DataAccess** layer. Instead, we used the **Business** layer as a bridge to ensure that we followed the architecture properly.

    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void TDelete(Order entity)
        {
            _orderDal.Delete(entity);
        }

        public List<Order> TGetAll()
        {
            return _orderDal.GetAll();
        }

        public Order TGetById(int Id)
        {
            return _orderDal.GetById(Id);
        }

        public void TInsert(Order entity)
        {
            _orderDal.Insert(entity);
        }

        public void TUpdate(Order entity)
        {
            _orderDal.Update(entity);
        }

 
In the second part of the episode, we added about 10 data to the category table in our database using SQL. We will use these entries in the from application to reinforce the topic. After adding the data, we double clicked the list button in the form application to go form's code section. Inside the class, we created a field named **private readonly ICategoryService _categoryService** and generated the constructor method. In this constructor, we added InilializeComponent(); function. Then we deleted the empty public FrmCategory() method that was left.

At this point, we cannot run the code directly because our FrmCategory class has now been converted to a format that requires a parameter. To fix this problem we deleted the parameter in the constructor method and manually created the objects for CategoryManager and EfCategoryDal in the _categoryService field. We changed the code to: **_categoryService = new CategoryManager(new EfCategoryDal());**

Next we navigated to EfCategoryDal, pressed Alt + Enter and added a referance to the **DataAccess** layer. This resolved the referance problem in EfCategoryDal and we completed this part of task. After completing the important steps for this part of episode, we moved on to adding other functionalities to the form and completed this episode.

## 📌 Episode 21 - Writing Entity Specific Method
In this episode we worked on writing methods specific to an entity. First we designed a new form for the product table and added about 10 data to the product table using SQL to set up the base. After that we went to code section of the form by clicking the list button an object with the following code: **ProductManager productManager = new ProductManager(new EfProductDal());** 

We then wrote the necessary code for listing and open the application. However, we encountered a problem: since the category table is realted to the product table, it appeared as a separate column in the data grid. To solve this problem we wrote a method specific to the entity. In the IProductDal interface located in the **Abstract** folder of the **DataAccess** layer, we added this code: **List<Product> GetProductsWithCategory();** This method is designed to retrieve products along with their categories. Next we opened the EfProductDal class in the EntityFramework folder of the same layer and included our interface.

Similar to the **Generic Repository** we needed a context here, but this context would be specific to entity. We started by creating an object from the CampContext class: **var context = new CampContext();** After this we wrote the necessary code for the method. Since we needed an extra column for category name, we used a method named [NotMapped].

**Codes:**

            var context = new CampContext();
            var values = context.Products.Select
                (x => new
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductStock = x.ProductStock,
                    ProductPrice = x.ProductPrice,
                    ProductDescription = x.ProductDescription,
                    CategoryName = x.Category.CategoryName
                }).ToList();

            return values;

We went to the product class and added the [NotMapped] attribute. Below it, we created a CategoryName property. After this we copied the **List<Product> GetProductsWithCategory()** method from EfProductDal and pasted it into the IProductService interface in the **Business** layer, adding a T to the beggining of the method name. We then implemented in the ProductManager class and wrote the following code: **return _productDal.GetProductsWithCategory();** Since this method is specific to the product entity, it can't be used for other entities in this project. To test the new system, we added a new list button to the form application. When we tried listing we encountered a another error. To try a different approach, we commented out the [NotMapped] attribute and the CategoryName property in the product class. Then we replaced **List<Product> to List<Object>** in all related areas, including IProductDal, ProductManager and IProductService. Because we wanted the method to work with an anonymous value, whe changed it to object type value. Finally we updated the return values line as following code: **return values.Cast<Object>().ToList();** This allowed us to successfully complete the listing process and we finished here this episode.

## 📌 Episode 22 - Using Dapper in C#
In this episode we completed form project that we started in previous episode, finished the form project. Then we moved on to the new part of this course, the 501 module. **You can access the 501 module from my Github profile.**
