# Project Manager

The **Project Manager** system is designed to efficiently monitor projects and their associated tasks. This centralized portal simplifies the process of managing projects and tasks for a team of developers.

---

## Key Features and Functionality

### 1. **Project Management**
- **Overview**:  
  Manage projects at various stages of development, including states such as:  
  - "Frozen"  
  - "Pending"  
  - "In Progress"  
  - And more.
- **Task Association**:  
  Link multiple tasks to each project for a detailed breakdown of work requirements.

### 2. **Task Management**
- **Task Accessibility**:  
  Users can access specific projects and view all tasks assigned to them within that project.
- **Task Types**:  
  Tasks are categorized into different types:  
  - **Bugs**: Issues or errors to be resolved.  
  - **Features**: New functionalities to be implemented.  
- **Task Priorities**:  
  Assign priority levels to tasks:  
  - **Urgent**  
  - **Medium**  
  - **Low**
- **File Attachments**:  
  Users can attach images or other files to tasks to enhance understanding and improve communication within the team.

### 3. **Email Notifications**
- **User Notifications**:  
  Users are notified via email when:  
  - They are added to a project.  
  - A task is assigned to them.
- **Purpose**:  
  These notifications ensure quick and efficient communication among team members, reducing delays and improving workflow.

---

## User Roles and Permissions

The **Project Manager** system features a robust role-based permission structure to ensure proper management and control over projects and tasks. Below is a detailed description of the roles and their associated permissions:

---

### 1. **General Users**
- **Capabilities**:  
  - Can create new projects.  
  - Cannot add tasks or assign users to projects.  
  - Have limited access to project management features unless assigned additional roles.

---

### 2. **Project Administrator**
- **Role Overview**:  
  Users with the "Project Administrator" role have elevated permissions within a project.  
- **Capabilities**:  
  - Can add tasks to projects.  
  - Can assign tasks to users.  
  - Can add other users to the project.  
  - Focused on task and user management for efficient project execution.

---

### 3. **Project Creator**
- **Role Overview**:  
  This role is automatically assigned to the user who creates a project.  
- **Capabilities**:  
  - Can modify the roles of other users within the project.  
  - Can remove users from the project.  
  - Acts as the owner of the project with the highest level of control over user permissions.

