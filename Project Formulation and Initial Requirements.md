# Project Formulation and Initial Requirements
## Introduction
For our project, we’ve decided to develop an **Online Library Management System**. This system will allow users to search for, borrow, and review books, while providing library staff with tools to manage their inventory and monitor book loans. With modern libraries embracing digital transformation, this system aims to provide a seamless user experience for both library users and administrators. Below, we will outline the basic requirements for the system, why we chose them, and how they add value.
## Basic Requirements
### 1. User Login and Role Management
   - **Description**: The system will support role-based access control with different types of users, such as **Admin**, **Librarian**, and **Library Member**. Each user type will have specific permissions.
   - **Reason for Inclusion**: A secure login system is fundamental to ensure that the right users can access appropriate features. Admins can manage users and inventory, librarians can manage loans, and library members can search and borrow books.
   - **Why It’s Cool**: Implementing roles enhances security and organization. It gives users a personalized experience based on their role and access level, making the system more user-friendly and manageable for library staff.
### 2. Book Search and Filtering
   - **Description**: Users can search for books using various criteria, including title, author, genre, and availability. Advanced filters such as publication year and language will be available.
   - **Reason for Inclusion**: Searching is a core function of any library system. Users need quick and accurate results to find the books they are interested in, especially when libraries have extensive collections.
   - **Why It’s Cool**: The advanced search filtering allows users to narrow down their choices efficiently. For large libraries, this feature adds convenience, ensuring users spend less time searching and more time reading.
### 3. Borrowing and Returning Books
   - **Description**: Users will be able to borrow books, see their borrowing history, and return books through the system. The admin will be able to view and manage all active loans.
   - **Reason for Inclusion**: The borrowing and returning of books is the most fundamental action in a library. Without this, the system wouldn’t be complete. Users need to be able to track what they’ve borrowed and return items promptly.
   - **Why It’s Cool**: This feature simplifies the borrowing process, allowing users to see what they have borrowed and their due dates, ensuring they stay on top of their loans.
### 4. Book Reviews and Ratings
   - **Description**: Users can leave reviews and ratings for books they have borrowed. These reviews will be visible to other users.
   - **Reason for Inclusion**: Reviews and ratings are key to creating a community of readers. This feature allows users to share their experiences, helping others discover new books.
   - **Why It’s Cool**: Reviews and ratings transform the library into a more social space. Users can get recommendations from others and interact with the system beyond simply borrowing books.
### 5. Admin Dashboard for Inventory Management
   - **Description**: Admins will have access to a dashboard where they can add, update, or remove books from the system. They will also manage user accounts and monitor loans.
   - **Reason for Inclusion**: To maintain an up-to-date inventory, admins need to be able to manage books efficiently. This ensures that users can access accurate information about the library’s collection.
   - **Why It’s Cool**: A well-designed admin dashboard streamlines library operations. It provides a bird's-eye view of the system, empowering admins to take control over all aspects of the library's digital operations.
### 6. Data Persistence for User and Book Records
  -	**Description**: The system will use a relational database to store user information, book data, borrowing history, and reviews.
  -	**Reason for Inclusion**: Reliable data storage is crucial to ensure that all user and book-related information is saved and can be accessed when needed. This includes everything from user accounts and book availability to borrowing records and user reviews.
  -	**Why It’s Cool**: By using a relational database, we can easily scale the system as the library grows. The database structure ensures that all relationships between users, books, and transactions are managed efficiently, providing a smooth and organized experience for both users and admins.
### 7. Communication service between users.
  -	**Description**: The system will use an internal communicator so the users can communicate with each other easily.
  -	**Reason for Inclusion**: To allow users to ask questions to other users about the books they have read.
  -	**Why It’s Cool**: By using this communicator we can easily ask people that read the book that we are interested in about their opinions and possible recommendations.
## Why We Chose This Project
We chose the **Online Library Management System** because it combines elements of user management, data handling, and UI/UX design into a cohesive system. It challenges us to think critically about data flows and user interactions, while also offering us the opportunity to implement various features like **messaging system** and **reviews**.

The library system also has real-world applicability, making it an attractive project for future use cases. Libraries today are evolving, and with more digital services being offered, such systems can be pivotal in how users interact with physical and digital books.
## What Makes It Cool
We believe the Online Library Management System stands out due to the following features:
-	Personalized User Experience: Users can log in, look through reviews and ratings when deciding on new books, and view their borrowing history, making their experience seamless and engaging.
-	Community Interaction: The messaging feature promotes user interaction and adds a social element to the library, which is often missing in traditional systems.
-	Admin Power: The admin dashboard allows for smooth, easy control over all the important aspects of the library, ensuring that librarians can focus on what matters most—helping users.
In future iterations, we are excited to potentially add advanced features like personalized book recommendations, QR code scanning, and waitlist notifications to further enhance user experience and make the system more interactive.
## Next Steps
For now, we have decided to implement the basic features listed above. However, as we continue working on the project, we will evaluate and potentially add more advanced features to create an even richer user experience.

Stay tuned for updates on how we implement these features!
